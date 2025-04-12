using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Linq;

namespace StairsAreaPlugin
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
                              ref string message,
                              ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            if (uidoc.Selection.GetElementIds().Count != 1)
            {
                TaskDialog.Show("Error", "Please select one element: stairs or ramp.");
                return Result.Succeeded;
            }

            ElementId selectedId = uidoc.Selection.GetElementIds().First();
            Element element = doc.GetElement(selectedId);

            TaskDialog dialog = new TaskDialog("What did you select?");
            dialog.MainInstruction = "Select the type of the selected element";
            dialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Stairs");
            dialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "Ramp");
            dialog.CommonButtons = TaskDialogCommonButtons.Cancel;
            dialog.DefaultButton = TaskDialogResult.CommandLink1;

            TaskDialogResult result = dialog.Show();

            if (result == TaskDialogResult.CommandLink1)
            {
                return HandleStairs(doc, element);
            }
            else if (result == TaskDialogResult.CommandLink2)
            {
                return HandleRamp(doc, element);
            }

            return Result.Cancelled;
        }

        private Result HandleStairs(Document doc, Element stairsElement)
        {
            if (stairsElement.Category.Id.IntegerValue != (int)BuiltInCategory.OST_Stairs)
            {
                TaskDialog.Show("Error", "The selected element is not a stair.");
                return Result.Failed;
            }

            double totalTreadAreaM2 = 0;
            double totalRiserAreaM2 = 0;

            var stairsRuns = new FilteredElementCollector(doc)
                .OfClass(typeof(StairsRun))
                .Cast<StairsRun>()
                .Where(run => run.GetStairs() != null && run.GetStairs().Id == stairsElement.Id)
                .ToList();

            foreach (var run in stairsRuns)
            {
                int numRisers = run.ActualRisersNumber;
                int numTreads = run.ActualTreadsNumber;
                double width = UnitUtils.ConvertFromInternalUnits(run.ActualRunWidth, UnitTypeId.Meters);

                Parameter treadDepthParam = run.get_Parameter(BuiltInParameter.STAIRS_RUN_ACTUAL_TREAD_DEPTH);
                double treadDepth = treadDepthParam != null
                    ? UnitUtils.ConvertFromInternalUnits(treadDepthParam.AsDouble(), UnitTypeId.Meters)
                    : 0;

                Parameter riserHeightParam = run.get_Parameter(BuiltInParameter.STAIRS_RUN_ACTUAL_RISER_HEIGHT);
                double riserHeight = riserHeightParam != null
                    ? UnitUtils.ConvertFromInternalUnits(riserHeightParam.AsDouble(), UnitTypeId.Meters)
                    : 0;

                double runTreadArea = numTreads * treadDepth * width;
                totalTreadAreaM2 += runTreadArea;

                double runRiserArea = numRisers * riserHeight * width;
                totalRiserAreaM2 += runRiserArea;
            }

            Parameter horizontalParam = stairsElement.LookupParameter("DI_Area_Riser");
            Parameter verticalParam = stairsElement.LookupParameter("DI_Area_Tread");

            using (Transaction t = new Transaction(doc, "Write stair surface areas"))
            {
                t.Start();

                if (horizontalParam != null && horizontalParam.StorageType == StorageType.Double)
                {
                    double internalVal = UnitUtils.ConvertToInternalUnits(totalTreadAreaM2, UnitTypeId.SquareMeters);
                    horizontalParam.Set(internalVal);
                }
                else
                {
                    TaskDialog.Show("Warning", "Missing parameter: DI_Area_Riser");
                }

                if (verticalParam != null && verticalParam.StorageType == StorageType.Double)
                {
                    double internalVal = UnitUtils.ConvertToInternalUnits(totalRiserAreaM2, UnitTypeId.SquareMeters);
                    verticalParam.Set(internalVal);
                }
                else
                {
                    TaskDialog.Show("Warning", "Missing parameter: DI_Area_Tread");
                }

                t.Commit();
            }

            TaskDialog.Show("Result", $"Stairs:\nHorizontal: {totalTreadAreaM2:F2} m²\nVertical: {totalRiserAreaM2:F2} m²");
            return Result.Succeeded;
        }

        private Result HandleRamp(Document doc, Element rampElement)
        {
            if (rampElement.Category.Id.IntegerValue != (int)BuiltInCategory.OST_Ramps)
            {
                TaskDialog.Show("Error", "The selected element is not a ramp.");
                return Result.Failed;
            }

            double totalSurfaceAreaM2 = 0;

            Options options = new Options();
            GeometryElement geomElem = rampElement.get_Geometry(options);
            if (geomElem == null)
            {
                TaskDialog.Show("Error", "Failed to retrieve ramp geometry.");
                return Result.Failed;
            }

            foreach (GeometryObject obj in geomElem)
            {
                GeometryInstance geomInst = obj as GeometryInstance;
                if (geomInst == null) continue;

                GeometryElement instGeom = geomInst.GetInstanceGeometry();
                foreach (GeometryObject instObj in instGeom)
                {
                    Solid solid = instObj as Solid;
                    if (solid == null || solid.Faces.Size == 0) continue;

                    foreach (Face face in solid.Faces)
                    {
                        PlanarFace pf = face as PlanarFace;
                        if (pf == null) continue;

                        XYZ normal = pf.FaceNormal.Normalize();

                        if (normal.Z > 0.3)
                        {
                            double area = UnitUtils.ConvertFromInternalUnits(pf.Area, UnitTypeId.SquareMeters);
                            totalSurfaceAreaM2 += area;
                        }
                    }
                }
            }

            Parameter areaParam = rampElement.LookupParameter("DI_Area");

            using (Transaction t = new Transaction(doc, "Write ramp area"))
            {
                t.Start();

                if (areaParam != null && areaParam.StorageType == StorageType.Double)
                {
                    double internalVal = UnitUtils.ConvertToInternalUnits(totalSurfaceAreaM2, UnitTypeId.SquareMeters);
                    areaParam.Set(internalVal);
                }
                else
                {
                    TaskDialog.Show("Warning", "Missing instance parameter: DI_Area");
                }

                t.Commit();
            }

            TaskDialog.Show("Result", $"Ramp surface area: {totalSurfaceAreaM2:F2} m²");
            return Result.Succeeded;
        }
    }
}

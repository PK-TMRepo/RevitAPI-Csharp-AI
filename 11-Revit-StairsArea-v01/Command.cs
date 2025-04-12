
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
                TaskDialog.Show("Error", "Please select one stair element.");
                return Result.Succeeded;
            }

            ElementId selectedId = uidoc.Selection.GetElementIds().First();
            Element stairElement = doc.GetElement(selectedId);

            if (stairElement.Category.Id.IntegerValue != (int)BuiltInCategory.OST_Stairs)
            {
                TaskDialog.Show("Error", "The selected element is not a stair.");
                return Result.Succeeded;
            }

            double totalTreadAreaM2 = 0;
            double totalRiserAreaM2 = 0;

            var stairRuns = new FilteredElementCollector(doc)
                .OfClass(typeof(StairsRun))
                .Cast<StairsRun>()
                .Where(run => run.GetStairs() != null && run.GetStairs().Id == stairElement.Id)
                .ToList();

            foreach (var run in stairRuns)
            {
                int numberOfRisers = run.ActualRisersNumber;
                int numberOfTreads = run.ActualTreadsNumber;
                double width = UnitUtils.ConvertFromInternalUnits(run.ActualRunWidth, UnitTypeId.Meters);

                Parameter treadDepthParam = run.get_Parameter(BuiltInParameter.STAIRS_RUN_ACTUAL_TREAD_DEPTH);
                double treadDepth = treadDepthParam != null
                    ? UnitUtils.ConvertFromInternalUnits(treadDepthParam.AsDouble(), UnitTypeId.Meters)
                    : 0;

                Parameter riserHeightParam = run.get_Parameter(BuiltInParameter.STAIRS_RUN_ACTUAL_RISER_HEIGHT);
                double riserHeight = riserHeightParam != null
                    ? UnitUtils.ConvertFromInternalUnits(riserHeightParam.AsDouble(), UnitTypeId.Meters)
                    : 0;

                double treadArea = numberOfTreads * treadDepth * width;
                totalTreadAreaM2 += treadArea;

                double riserArea = numberOfRisers * riserHeight * width;
                totalRiserAreaM2 += riserArea;
            }

            TaskDialog.Show("Stair Surface Area",
                $"Horizontal surface (treads): {totalTreadAreaM2:F2} m²\n" +
                $"Vertical surface (risers): {totalRiserAreaM2:F2} m²");

            Parameter horizontalParam = stairElement.LookupParameter("DI_Area_Riser");
            Parameter verticalParam = stairElement.LookupParameter("DI_Area_Tread");

            using (Transaction t = new Transaction(doc, "Update stair area parameters"))
            {
                t.Start();

                if (horizontalParam != null && horizontalParam.StorageType == StorageType.Double)
                {
                    double internalVal = UnitUtils.ConvertToInternalUnits(totalTreadAreaM2, UnitTypeId.SquareMeters);
                    horizontalParam.Set(internalVal);
                }
                else
                {
                    TaskDialog.Show("Warning", "Instance parameter 'DI_Area_Riser' not found.");
                }

                if (verticalParam != null && verticalParam.StorageType == StorageType.Double)
                {
                    double internalVal = UnitUtils.ConvertToInternalUnits(totalRiserAreaM2, UnitTypeId.SquareMeters);
                    verticalParam.Set(internalVal);
                }
                else
                {
                    TaskDialog.Show("Warning", "Instance parameter 'DI_Area_Tread' not found.");
                }

                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}

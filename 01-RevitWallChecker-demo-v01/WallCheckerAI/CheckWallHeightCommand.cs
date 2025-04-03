using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;

namespace WallCheckerAI
{
    [Transaction(TransactionMode.Manual)]
    public class CheckWallHeightCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Document doc = uidoc.Document;

                Reference pickedRef = uidoc.Selection.PickObject(ObjectType.Element, new WallSelectionFilter(), "Pick Wall");
                if (pickedRef == null) return Result.Cancelled;

                Wall wall = doc.GetElement(pickedRef) as Wall;
                if (wall == null)
                {
                    TaskDialog.Show("ERROR", "The selected element is not a wall");
                    return Result.Failed;
                }

                Parameter heightParam = wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM);
                double heightFeet = heightParam.AsDouble();
                double heightMeters = heightFeet * 0.3048;

                double limit = 3.0;

                if (heightMeters > limit)
                {
                    double excess = heightMeters - limit;
                    TaskDialog.Show("⚠️ Violation",
                        $"Wall has {Math.Round(heightMeters, 2)} m.\n" +
                        $"It is {Math.Round(excess, 2)} m too much.\n\n" +
                        $"Please decrease height by {Math.Round(excess, 2)} m.");
                }
                else
                {
                    TaskDialog.Show("✅ Compliance OK",
                        $"Wall has {Math.Round(heightMeters, 2)} m and the wall meets building code requirements");
                }

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("ERROR", $"An error occurred: {ex.Message}");
                return Result.Failed;
            }
        }

        private class WallSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element element) => element is Wall;
            public bool AllowReference(Reference reference, XYZ position) => true;
        }
    }
}

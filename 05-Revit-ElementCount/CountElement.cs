using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Linq;

[Transaction(TransactionMode.Manual)]
public class CountElementsCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        UIApplication uiApp = commandData.Application;
        Document doc = uiApp.ActiveUIDocument.Document;

        try
        {
            // Zliczanie wybranych elementów
            int wallCount = CountElementsOfCategory(doc, BuiltInCategory.OST_Walls);
            int doorCount = CountElementsOfCategory(doc, BuiltInCategory.OST_Doors);
            int windowCount = CountElementsOfCategory(doc, BuiltInCategory.OST_Windows);

            // Wyświetlanie wyników
            TaskDialog.Show("Element Count", $"Walls: {wallCount}\nDoors: {doorCount}\nWindows: {windowCount}");
            return Result.Succeeded;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            TaskDialog.Show("Error", ex.Message);
            return Result.Failed;
        }
    }

    private int CountElementsOfCategory(Document doc, BuiltInCategory category)
    {
        // Zbieranie elementów danej kategorii
        FilteredElementCollector collector = new FilteredElementCollector(doc);
        collector.OfCategory(category).WhereElementIsNotElementType();

        return collector.Count();
    }
}

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Linq;
using Autodesk.Revit.DB.Architecture;

namespace KoconAI.RoomAreaCalculator
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class RoomAreaCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Document doc = uiDoc.Document;

                // Pobierz wszystkie pomieszczenia
                var rooms = new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_Rooms)
                    .WhereElementIsNotElementType()
                    .Cast<Room>()
                    .Where(r => r.Area > 0);

                if (!rooms.Any())
                {
                    TaskDialog.Show("Info", "No rooms found in the project.");
                    return Result.Succeeded;
                }

                // Oblicz całkowitą powierzchnię
                double totalArea = rooms.Sum(room => room.Area);
                double areaInMeters = UnitUtils.ConvertFromInternalUnits(totalArea, UnitTypeId.SquareMeters);

                // Wyświetl wynik
                TaskDialog.Show("Room Area Calculator",
                    $"Total rooms area: {areaInMeters:0.00} m²\n" +
                    $"Number of rooms: {rooms.Count()}");

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
                return Result.Failed;
            }
        }
    }

    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            try
            {
                // Utwórz panel na wstążce
                RibbonPanel panel = app.CreateRibbonPanel("KOCOŃ AI Tools");

                // Ścieżka do bieżącego assembly
                string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

                // Utwórz przycisk
                PushButtonData buttonData = new PushButtonData(
                    "RoomAreaCalculator",
                    "Calculate Area",
                    assemblyPath,
                    typeof(RoomAreaCommand).FullName);

                PushButton button = panel.AddItem(buttonData) as PushButton;

                // Konfiguracja przycisku
                button.ToolTip = "Calculates total area of all rooms";
                button.LongDescription = "This tool calculates the sum of areas for all rooms in the project.";

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Startup Error", ex.Message);
                return Result.Failed;
            }
        }

        public Result OnShutdown(UIControlledApplication app)
        {
            return Result.Succeeded;
        }
    }
}
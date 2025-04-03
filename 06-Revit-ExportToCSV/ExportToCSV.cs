using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[Transaction(TransactionMode.Manual)]
[Regeneration(RegenerationOption.Manual)]
public class ExportToCSV : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        UIApplication uiApp = commandData.Application;
        Document doc = uiApp.ActiveUIDocument.Document;

        try
        {
            // Lista do przechowywania danych
            var roomData = new List<string>();
            var doorData = new List<string>();
            var windowData = new List<string>();

            // Eksport danych o pomieszczeniach
            var rooms = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Rooms)
                .WhereElementIsNotElementType()
                .Cast<Room>();

            foreach (Room room in rooms)
            {
                string roomDetails = $"{room.Number},{room.Name},{room.Area}";
                roomData.Add(roomDetails);
            }

            // Eksport danych o drzwiach
            var doors = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Doors)
                .WhereElementIsNotElementType()
                .Cast<FamilyInstance>();

            foreach (FamilyInstance door in doors)
            {
                string doorDetails = $"{door.Name},{door.Symbol.FamilyName},{door.Symbol.Name}";
                doorData.Add(doorDetails);
            }

            // Eksport danych o oknach
            var windows = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Windows)
                .WhereElementIsNotElementType()
                .Cast<FamilyInstance>();

            foreach (FamilyInstance window in windows)
            {
                string windowDetails = $"{window.Name},{window.Symbol.FamilyName},{window.Symbol.Name}";
                windowData.Add(windowDetails);
            }

            // Ścieżka do pliku CSV
            string filePath = @"C:\Temp\RevitExport.csv";

            // Zapisz dane do CSV
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Nagłówki CSV
                writer.WriteLine("Room Number,Room Name,Room Area");
                foreach (var room in roomData)
                {
                    writer.WriteLine(room);
                }

                writer.WriteLine(); // Pusty wiersz pomiędzy sekcjami

                writer.WriteLine("Door Name,Family Name,Symbol Name");
                foreach (var door in doorData)
                {
                    writer.WriteLine(door);
                }

                writer.WriteLine(); // Pusty wiersz pomiędzy sekcjami

                writer.WriteLine("Window Name,Family Name,Symbol Name");
                foreach (var window in windowData)
                {
                    writer.WriteLine(window);
                }
            }

            TaskDialog.Show("Export Complete", "Data has been exported successfully to C:\\Temp\\RevitExport.csv");

            return Result.Succeeded;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            TaskDialog.Show("Error", $"Failed to export data: {ex.Message}");
            return Result.Failed;
        }
    }
}

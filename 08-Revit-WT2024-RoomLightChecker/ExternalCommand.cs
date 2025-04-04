using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using System;
using System.Collections.Generic;

[Transaction(TransactionMode.Manual)]
public class ExternalCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        UIDocument uidoc = commandData.Application.ActiveUIDocument;
        Document doc = uidoc.Document;

        // 1. Wybierz ROOM
        Room room;
        try
        {
            Reference pickedRoom = uidoc.Selection.PickObject(ObjectType.Element, new RoomSelectionFilter(), "Wybierz pomieszczenie");
            room = doc.GetElement(pickedRoom) as Room;
            if (room == null) return Result.Failed;
        }
        catch
        {
            return Result.Cancelled;
        }

        // 2. Wybierz OKNA (wielokrotny wybór)
        IList<Reference> pickedWindows;
        try
        {
            pickedWindows = uidoc.Selection.PickObjects(ObjectType.Element, new WindowSelectionFilter(), "Wybierz okna przypisane do tego pokoju");
        }
        catch
        {
            return Result.Cancelled;
        }

        // 3. Obliczenia
        double floorArea = UnitUtils.ConvertFromInternalUnits(room.Area, UnitTypeId.SquareMeters);
        double totalWindowArea = 0;
        int windowCount = 0;

        foreach (Reference r in pickedWindows)
        {
            Element win = doc.GetElement(r);

            // Szukaj parametru Width i Height — najpierw z instancji, potem z typu (Symbol)
            Parameter widthParam = win.LookupParameter("Width") ?? doc.GetElement(win.GetTypeId()).LookupParameter("Width");
            Parameter heightParam = win.LookupParameter("Height") ?? doc.GetElement(win.GetTypeId()).LookupParameter("Height");

            // Upewnij się, że to liczby (double)
            if (widthParam != null && heightParam != null &&
                widthParam.StorageType == StorageType.Double &&
                heightParam.StorageType == StorageType.Double)
            {
                double width = UnitUtils.ConvertFromInternalUnits(widthParam.AsDouble(), UnitTypeId.Meters);
                double height = UnitUtils.ConvertFromInternalUnits(heightParam.AsDouble(), UnitTypeId.Meters);

                totalWindowArea += width * height;
                windowCount++;
            }
        }

        // 4. Formularz wyników
        var form = new RoomCheckForm(totalWindowArea, floorArea, windowCount);
        form.ShowDialog();

        return Result.Succeeded;
    }
}

// Filtr do wyboru tylko Room
public class RoomSelectionFilter : ISelectionFilter
{
    public bool AllowElement(Element elem) => elem is Room;
    public bool AllowReference(Reference reference, XYZ position) => true;
}

// Filtr do wyboru tylko Windows
public class WindowSelectionFilter : ISelectionFilter
{
    public bool AllowElement(Element elem) => elem.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Windows;
    public bool AllowReference(Reference reference, XYZ position) => true;
}

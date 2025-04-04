using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using System;
using System.Linq;

namespace WindowAreaCalculator
{
    [Transaction(TransactionMode.ReadOnly)]
    public class WindowAreaCommand : IExternalCommand
    {
        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Pobierz wszystkie okna
            FilteredElementCollector collector = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Windows)
                .WhereElementIsNotElementType();

            double totalWindowArea = 0;
            int oknoCount = 0;

            foreach (Element win in collector)
            {
                Parameter widthParam = win.LookupParameter("Width") ?? doc.GetElement(win.GetTypeId()).LookupParameter("Width");
                Parameter heightParam = win.LookupParameter("Height") ?? doc.GetElement(win.GetTypeId()).LookupParameter("Height");

                if (widthParam != null && heightParam != null &&
                    widthParam.StorageType == StorageType.Double &&
                    heightParam.StorageType == StorageType.Double)
                {
                    double width = UnitUtils.ConvertFromInternalUnits(widthParam.AsDouble(), UnitTypeId.Meters);
                    double height = UnitUtils.ConvertFromInternalUnits(heightParam.AsDouble(), UnitTypeId.Meters);

                    totalWindowArea += width * height;
                    oknoCount++;
                }
            }

            TaskDialog.Show("Powierzchnia okien",
                $"Znaleziono {oknoCount} okien\n" +
                $"Łączna powierzchnia: {totalWindowArea:F2} m²");

            return Result.Succeeded;
        }
    }
}

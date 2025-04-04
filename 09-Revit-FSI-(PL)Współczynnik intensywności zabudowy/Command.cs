using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System.Linq;
using System.Windows.Forms;

namespace FSICalculator
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Pobierz wszystkie pomieszczenia (Rooms)
            var rooms = new FilteredElementCollector(doc)
                            .OfCategory(BuiltInCategory.OST_Rooms)
                            .WhereElementIsNotElementType()
                            .OfType<Room>()
                            .ToList();

            // Sumuj powierzchnie pomieszczeń (m²)
            double totalRoomArea = rooms
                .Where(r => r.Area > 0)
                .Sum(r => r.Area); // Revit API zwraca już w jednostkach projektu (m² jeśli tak ustawiono)

            // Pobierz toposolid (działkę)
            var toposolid = new FilteredElementCollector(doc)
                                .OfCategory(BuiltInCategory.OST_Toposolid)
                                .WhereElementIsNotElementType()
                                .FirstOrDefault();

            double siteArea = 0;
            if (toposolid != null)
            {
                Parameter areaParam = toposolid.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED);
                if (areaParam != null && areaParam.HasValue)
                {
                    siteArea = UnitUtils.ConvertFromInternalUnits(
                        areaParam.AsDouble(),
                        UnitTypeId.SquareMeters
                    );
                }
            }

            // Oblicz współczynnik intensywności zabudowy (FSI)
            double fsi = siteArea > 0 ? totalRoomArea / siteArea : 0;

            // Pokaż wynik w okienku
            MessageBox.Show(
                $"Powierzchnia kondygnacji (Rooms): {totalRoomArea:F2} m²\n" +
                $"Powierzchnia działki (Toposolid): {siteArea:F2} m²\n\n" +
                $"FSI (intensywność zabudowy): {fsi:F2}",
                "FSI Kalkulator",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            return Result.Succeeded;
        }
    }
}

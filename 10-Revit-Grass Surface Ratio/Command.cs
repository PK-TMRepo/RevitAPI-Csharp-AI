using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using System;
using System.Collections.Generic;

namespace BiologicznaTrawaSimple
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            double plotArea = 0.0;
            double greenArea = 0.0;

            // Zbieramy wszystkie Toposolidy
            FilteredElementCollector collector = new FilteredElementCollector(doc)
                .OfClass(typeof(Toposolid))
                .WhereElementIsNotElementType();

            foreach (Toposolid ts in collector)
            {
                string name = ts.Name?.ToLower() ?? "";

                Parameter areaParam = ts.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED);
                if (areaParam != null && areaParam.HasValue)
                {
                    double area = areaParam.AsDouble(); // ft²

                    if (name.Contains("działka"))
                    {
                        plotArea += area;
                    }
                    else if (name.Contains("trawa"))
                    {
                        greenArea += area;
                    }
                }
            }

            double plotM2 = plotArea * 0.092903;
            double greenM2 = greenArea * 0.092903;
            double percent = (plotM2 > 0) ? (greenM2 / plotM2 * 100.0) : 0.0;

            string result = $"📐 Powierzchnia działki: {Math.Round(plotM2, 2)} m²\n" +
                            $"🌿 Powierzchnia 'trawa': {Math.Round(greenM2, 2)} m²\n" +
                            $"✅ Udział biologicznie czynny: {Math.Round(percent, 1)}%";

            TaskDialog.Show("Powierzchnia biologicznie czynna", result);
            return Result.Succeeded;
        }
    }
}

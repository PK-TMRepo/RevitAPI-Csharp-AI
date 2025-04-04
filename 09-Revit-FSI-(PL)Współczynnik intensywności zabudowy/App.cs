using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Reflection;

namespace FSICalculator
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // Zakładka (tab) – może już istnieć
            string tabName = "FSI Tools";
            try { application.CreateRibbonTab(tabName); } catch { }

            // Panel
            RibbonPanel panel = application.CreateRibbonPanel(tabName, "Zabudowa");

            // Ścieżka do aktualnego DLL
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            // Przycisk
            PushButtonData buttonData = new PushButtonData("FSICalcBtn", "FSI Kalkulator", thisAssemblyPath, "FSICalculator.Command");

            PushButton button = panel.AddItem(buttonData) as PushButton;
            button.ToolTip = "Oblicza współczynnik intensywności zabudowy (FSI).";

            // (Opcjonalnie) Ikony – możesz dodać .png lub .ico później

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}


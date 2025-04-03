using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Windows.Media.Imaging;

namespace WallCheckerAI
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            TaskDialog.Show("WallCheckerAI", "Plugin launched!");

            string tabName = "AI Tools";
            string panelName = "Compliance";

            try { application.CreateRibbonTab(tabName); } catch { }

            RibbonPanel panel = application.CreateRibbonPanel(tabName, panelName);

            PushButtonData buttonData = new PushButtonData(
                "WallCheckBtn",
                "Wall Checker",
                typeof(App).Assembly.Location,
                "WallCheckerAI.CheckWallHeightCommand"
            );

            PushButton button = panel.AddItem(buttonData) as PushButton;
            if (button != null)
            {
                button.ToolTip = "Check the wall height in accordance with local building regulations.";
                button.LargeImage = new BitmapImage(new Uri("YOUR PATH FOR ICON1.png"));
                button.Image = new BitmapImage(new Uri("YOUR PATH FOR ICON1.png"));
            }

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}

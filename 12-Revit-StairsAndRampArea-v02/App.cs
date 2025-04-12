using Autodesk.Revit.UI;

namespace StairsAreaPlugin
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application) => Result.Succeeded;
        public Result OnShutdown(UIControlledApplication application) => Result.Succeeded;
    }
}

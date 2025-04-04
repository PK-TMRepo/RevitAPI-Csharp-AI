# 🧱 WallChecker-demo-v01 – Revit Plugin (this is demo without AI integration)

WallChecker is a simple and smart plugin for Autodesk Revit 2025 that:

- Adds a custom tab and button to the Revit ribbon interface
- Lets the user select a wall and checks its height
- Displays a warning if the wall height is **3.0 meters or more**
- Displays a confirmation message if the wall height is below the legal threshold

### ⚖️ Assumed Legal Regulation:
According to an exemplary regulation, **walls must be shorter than 3.0 meters**.
Any wall **3.0 meters or higher is considered non-compliant**.

## 🔧 Requirements
- Autodesk Revit 2025
- .NET Framework 4.8
- Visual Studio 2022 or newer
- References:
  - RevitAPI.dll
  - RevitAPIUI.dll
  - PresentationCore.dll
  - WindowsBase.dll

## 🚀 Installation

1. Build the project in Visual Studio
2. Copy `WallCheckerAI.dll` to any location
3. Rename `WallCheckerAI.addin.example` to `WallCheckerAI.addin`
4. Update the `.addin` file with your local DLL path
5. Copy the `.addin` file to:
```
C:\Users\YourName\AppData\Roaming\Autodesk\Revit\Addins\2025
```

## 🧪 Usage

1. Open Revit 2025
2. Go to the **AI Tools** ribbon tab
3. Click **Wall Checker**
4. Select a wall
5. View compliance results based on the 3-meter rule

## 📜 License
MIT – free to use and modify

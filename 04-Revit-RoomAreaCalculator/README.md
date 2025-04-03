# RoomAreaCalculator

**RoomAreaCalculator** is a simple yet effective Revit plugin that calculates the total area of all rooms in the current Revit project and displays the result in a TaskDialog window.

> 📦 Version: `v1.0` – Initial release with core functionality for area calculation.

---

## ⚙️ Features

- Collects all room elements from the active Revit document.
- Calculates the total room area in square meters.
- Displays the total area and number of rooms in a native Revit TaskDialog.
- Integrates with Revit UI through a custom Ribbon Panel ("KOCOŃ AI Tools").

---

## 🧩 Installation

### 1. Build the Plugin

Open the solution `RoomAreaCalculator.sln` in Visual Studio 2022:

- Make sure Autodesk Revit 2025 API DLLs are referenced.
- Set configuration to `Release`.
- Build the solution to generate the `.dll`.

### 2. Create `.addin` File

Place a `.addin` file inside your Revit 2025 Addins folder:

```
C:\ProgramData\Autodesk\Revit\Addins\2025\
```

Sample `RoomAreaCalculator.addin`:

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<RevitAddIns>
  <AddIn Type="Command">
    <Name>RoomAreaCalculator</Name>
    <Assembly>C:\Path\To\RoomAreaCalculator.dll</Assembly>
    <AddInId>1e2e3e3e-4e4e-5e5e-6e6e-7e7e7e7e7e7e</AddInId>
    <FullClassName>KoconAI.RoomAreaCalculator.App</FullClassName>
    <VendorId>KOCON</VendorId>
    <VendorDescription>KOCOŃ AI Tools</VendorDescription>
  </AddIn>
</RevitAddIns>
```

> ⚠️ Remember to replace the `Assembly` path with your actual DLL path.

---

## 💻 Usage

1. Open Revit 2025.
2. Load any project.
3. Go to the “KOCOŃ AI Tools” ribbon panel.
4. Click on **Calculate Area**.
5. A TaskDialog will show the **total room area** and **number of rooms**.

---

## 🧼 .gitignore Suggestion

```gitignore
bin/
obj/
*.user
*.addin
.vs/
```

---

## 📜 License

This project is not open-source. Copying, modifying or distributing the plugin or its components without permission is strictly prohibited.

---

## 👨‍💻 Author

Developed by **Piotr Kocoń**  
Custom Revit tools powered by KOCOŃ AI.
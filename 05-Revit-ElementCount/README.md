# CountElement

**CountElement** is a simple Revit plugin that counts the number of walls, doors, and windows in your Revit project and displays the result in a TaskDialog window.

> 📦 Version: `v1.0` – Basic element counting functionality for Revit projects.

---

## ⚙️ Features

- Counts:
  - Walls
  - Doors
  - Windows
- Uses Revit API and `FilteredElementCollector`.
- Displays results using native Revit `TaskDialog`.

---

## 🧩 Installation

### 1. Build the Plugin

Open the solution `CountElement.sln` in Visual Studio 2022:

- Make sure Autodesk Revit 2025 API DLLs are referenced.
- Set configuration to `Release`.
- Build the solution to generate the `.dll`.

### 2. Create `.addin` File

Place the provided `.addin` file in your Revit Addins folder:

```
C:\ProgramData\Autodesk\Revit\Addins\2025\
```

Sample `CountElement.addin`:

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<RevitAddIns>
  <AddIn Type="Command">
    <Name>CountElement</Name>
    <Assembly>C:\Path\To\CountElement.dll</Assembly>
    <AddInId>22334455-6677-8899-aabb-ccddeeff0011</AddInId>
    <FullClassName>CountElementsCommand</FullClassName>
    <VendorId>KOCON</VendorId>
    <VendorDescription>KOCOŃ AI Tools</VendorDescription>
  </AddIn>
</RevitAddIns>
```

> ⚠️ Don’t forget to update the `Assembly` path with your actual `.dll` location.

---

## 💻 Usage

1. Open Revit 2025.
2. Load any project.
3. Use the plugin from the Add-Ins tab.
4. A dialog will show how many walls, doors, and windows are in the project.

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
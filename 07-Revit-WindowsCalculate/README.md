# WindowsArea

**WindowsArea** is a Revit plugin that calculates the total surface area of all windows in the active Revit project. It also counts the number of windows and displays the result in a simple TaskDialog window.

> 📦 Version: `v1.0` – Lightweight Revit plugin for window surface calculation.

---

## ⚙️ Features

- Collects all elements in the `OST_Windows` category.
- Extracts `Width` and `Height` parameters from instances or types.
- Calculates total area in **square meters (m²)**.
- Displays:
  - Total number of windows
  - Combined surface area
- Uses Revit `TaskDialog` for output.

---

## 🧩 Installation

### 1. Build the Plugin

Open the solution `WindowsArea.sln` in Visual Studio 2022:

- Make sure Autodesk Revit 2025 API DLLs are referenced.
- Set configuration to `Release`.
- Build the solution to generate the `.dll`.

### 2. Create `.addin` File

Place a `.addin` file in your Revit Addins folder:

```
C:\ProgramData\Autodesk\Revit\Addins\2025\
```

Sample `WindowsArea.addin`:

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<RevitAddIns>
  <AddIn Type="Command">
    <Name>WindowsArea</Name>
    <Assembly>C:\Path\To\WindowsArea.dll</Assembly>
    <AddInId>aaddeeff-1122-3344-5566-77889900aabb</AddInId>
    <FullClassName>WindowAreaCalculator.WindowAreaCommand</FullClassName>
    <VendorId>KOCON</VendorId>
    <VendorDescription>KOCOŃ AI Tools</VendorDescription>
  </AddIn>
</RevitAddIns>
```

> ⚠️ Update the `Assembly` path to match your DLL location.

---

## 💻 Usage

1. Open any Revit 2025 project.
2. Go to the **Add-Ins** tab and run the plugin.
3. A dialog will show:
   - Number of windows
   - Total window surface area in m²

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
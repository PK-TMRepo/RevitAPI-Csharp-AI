# Grass Surface Ratio

**Grass Surface Ratio** is a Revit plugin that calculates the percentage of biologically active area (in polish: "powierzchnia biologicznie czynna") based on Toposolid elements in your model. It is designed to assist in evaluating compliance with Polish urban planning standards for green areas.

> 📦 Version: `v1.0` – Simple and effective biologically active area calculator.

---

## ⚙️ Features

- Scans all `Toposolid` elements in the Revit model.
- Identifies:
  - Plot area by name containing `"działka"`
  - Green/grass area by name containing `"trawa"`
- Converts area from ft² to m².
- Calculates:
  - Total plot area (m²)
  - Total green area (m²)
  - Biologically active ratio (%)
- Displays result in a native Revit `TaskDialog`.

---

## 🧩 Installation

### 1. Build the Plugin

Open `BiologicznaTrawaSimple.csproj` or `.sln` in Visual Studio 2022:

- Make sure Autodesk Revit 2025 API DLLs are referenced.
- Set configuration to `Release`.
- Build the project to generate the `.dll`.

### 2. Add `.addin` File

Place the `.addin` file in:

```
C:\ProgramData\Autodesk\Revit\Addins\2025\
```

Sample `BiologicznaTrawaSimple.addin`:

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<RevitAddIns>
  <AddIn Type="Command">
    <Name>BiologicznaTrawaSimple</Name>
    <Assembly>C:\Path\To\BiologicznaTrawaSimple.dll</Assembly>
    <AddInId>44445555-6666-7777-8888-9999aaaabbbb</AddInId>
    <FullClassName>BiologicznaTrawaSimple.Command</FullClassName>
    <VendorId>KOCON</VendorId>
    <VendorDescription>KOCOŃ AI Tools</VendorDescription>
  </AddIn>
</RevitAddIns>
```

---

## 💻 Usage

1. Open a project in Revit 2025.
2. Run the plugin from the Add-Ins tab.
3. Make sure at least one Toposolid is named `"działka"` and another `"trawa"`.
4. A TaskDialog will show:
   - Plot area (m²)
   - Green area (m²)
   - Biologically active percentage

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

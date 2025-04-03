# ExportToCSV

**ExportToCSV** is a Revit plugin that exports data from rooms, doors, and windows to a `.csv` file. This tool is useful for quickly generating schedules and exporting BIM data for further analysis in Excel or other applications.

> 📦 Version: `v1.0` – CSV export functionality for key Revit categories.

---

## ⚙️ Features

- Exports the following to a CSV file:
  - **Rooms**: Number, Name, Area
  - **Doors**: Name, Family Name, Type Name
  - **Windows**: Name, Family Name, Type Name
- CSV is saved automatically to:
  ```
  C:\Temp\RevitExport.csv
  ```
- Uses `FilteredElementCollector` and `StreamWriter`.

---

## 🧩 Installation

### 1. Build the Plugin

Open the solution `ExportToCSV.sln` in Visual Studio 2022:

- Make sure Autodesk Revit 2025 API DLLs are referenced.
- Set configuration to `Release`.
- Build the solution to generate the `.dll`.

### 2. Create `.addin` File

Place the provided `.addin` file in your Revit Addins folder:

```
C:\ProgramData\Autodesk\Revit\Addins\2025\
```

Sample `ExportToCSV.addin`:

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<RevitAddIns>
  <AddIn Type="Command">
    <Name>ExportToCSV</Name>
    <Assembly>C:\Path\To\ExportToCSV.dll</Assembly>
    <AddInId>aa1111bb-cc22-dd33-ee44-ff5566778899</AddInId>
    <FullClassName>ExportToCSV</FullClassName>
    <VendorId>KOCON</VendorId>
    <VendorDescription>KOCOŃ AI Tools</VendorDescription>
  </AddIn>
</RevitAddIns>
```

> ⚠️ Make sure the `C:\Temp\` folder exists before running the plugin.

---

## 💻 Usage

1. Open Revit 2025 and any project.
2. Run the command via Add-Ins tab.
3. Data will be saved to `C:\Temp\RevitExport.csv`.
4. Open the file in Excel or another spreadsheet tool.

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
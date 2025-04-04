# FSICalculator

**FSICalculator** is a Revit plugin that calculates the Floor Space Index (FSI) for your building model. FSI is computed as the ratio between the total room area (floor space) and the plot area (toposolid). The plugin outputs results in a simple Windows message box.

> 📦 Version: `v1.0` – Basic FSI calculator with Toposolid support (Revit 2025).

---

## ⚙️ Features

- Collects all `Room` elements and sums their area.
- Reads the total site area from the `Toposolid`.
- Calculates the FSI = Total Room Area / Site Area.
- Displays:
  - Total floor area (m²)
  - Total site area (m²)
  - FSI value
- Runs from a ribbon tab and panel:
  - Tab: **"FSI Tools"**
  - Panel: **"Zabudowa"**

---

## 🧩 Installation

### 1. Build the Plugin

Open `FSICalculator.sln` in Visual Studio 2022:

- Make sure Autodesk Revit 2025 API DLLs are referenced.
- Set build configuration to `Release`.
- Build the solution to generate the `.dll`.

### 2. Create `.addin` File

Place the generated `.addin` file into:

```
C:\ProgramData\Autodesk\Revit\Addins\2025\
```

Sample `FSICalculator.addin`:

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<RevitAddIns>
  <AddIn Type="Application">
    <Name>FSICalculator</Name>
    <Assembly>C:\Path\To\FSICalculator.dll</Assembly>
    <AddInId>f5f5a3a3-bbbb-cccc-dddd-eeeeeeeeeeee</AddInId>
    <FullClassName>FSICalculator.App</FullClassName>
    <VendorId>KOCON</VendorId>
    <VendorDescription>KOCOŃ AI Tools</VendorDescription>
  </AddIn>
</RevitAddIns>
```

---

## 💻 Usage

1. Launch Revit 2025 and open a project.
2. Go to the **FSI Tools** tab and click **FSI Kalkulator**.
3. A dialog box will display:
   - Room area sum
   - Toposolid site area
   - Calculated FSI value

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
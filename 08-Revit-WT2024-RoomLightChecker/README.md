# WTRoom

**WTRoom** is a Revit plugin that checks whether a selected room meets the daylight requirement based on window-to-floor area ratio. It supports Polish technical conditions (WT 2024) for rooms designated for permanent or temporary occupancy.

> 📦 Version: `v1.0` – Interactive WinForms tool for visual daylight ratio verification.

---

## ⚙️ Features

- User selects:
  - One **Room**
  - One or more **Windows** assigned to that room
- Calculates:
  - Floor area (m²)
  - Total window area (m²)
  - Number of windows
  - Ratio of window-to-floor area
- Verifies compliance with **1/8 requirement** (12.5%) for permanent occupancy
- Offers option to check room type: `Permanent` vs `Temporary`
- Displays result in a custom WinForms dialog

---

## 🧩 Installation

### 1. Build the Plugin

Open the solution `WTRoom.sln` in Visual Studio 2022:

- Ensure Autodesk Revit 2025 API DLLs are referenced.
- Set configuration to `Release`.
- Build the solution to generate the `.dll`.

### 2. Create `.addin` File

Place a `.addin` file in your Revit Addins folder:

```
C:\ProgramData\Autodesk\Revit\Addins\2025\
```

Sample `WTRoom.addin`:

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<RevitAddIns>
  <AddIn Type="Command">
    <Name>WTRoom</Name>
    <Assembly>C:\Path\To\WTRoom.dll</Assembly>
    <AddInId>dd112233-4455-6677-8899-aabbccddeeff</AddInId>
    <FullClassName>ExternalCommand</FullClassName>
    <VendorId>KOCON</VendorId>
    <VendorDescription>KOCOŃ AI Tools</VendorDescription>
  </AddIn>
</RevitAddIns>
```

> ⚠️ Replace `Assembly` with the actual DLL path after building.

---

## 💻 Usage

1. Open a project in Revit 2025.
2. Run the plugin from the Add-Ins tab.
3. Select a **Room**, then select **Windows**.
4. A form will show:
   - Floor area
   - Window area
   - Ratio (%)
   - Whether it complies with 1/8 daylight requirement

---

## 🧼 .gitignore Suggestion

```gitignore
bin/
obj/
*.user
*.addin
.vs/
*.resx
```

---

## 📜 License

This project is not open-source. Copying, modifying or distributing the plugin or its components without permission is strictly prohibited.

---

## 👨‍💻 Author

Developed by **Piotr Kocoń**  
Custom Revit tools powered by KOCOŃ AI.
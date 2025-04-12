# StairsAreaPlugin

A lightweight Revit plugin that calculates the total horizontal and vertical surface area of stair elements in a Revit model. These calculated values are written to instance parameters in the selected stair element.

## 🧠 Features
- Calculates:
  - Total horizontal tread surface area (e.g. top of the steps)
  - Total vertical riser surface area (e.g. front faces of the steps)
- Automatically writes values to instance parameters:
  - `DI_Area_Riser` — Horizontal surface area (treads) in **m²**
  - `DI_Area_Tread` — Vertical surface area (risers) in **m²**
- Designed for use with Revit 2025

## 🚀 Installation
1. Build the project in Visual Studio with `.NET Framework 4.8`
2. Place the generated `StairsAreaPlugin.dll` into your Revit `Addins` folder:
   ```
   %AppData%\Autodesk\Revit\Addins\2025
   ```
3. Add the included `StairsAreaPlugin.addin` file to the same folder

## ⚠️ Requirements
Before using the plugin, you **must** manually create two instance parameters in your Revit project:

| Parameter Name     | Type   | Group             | Instance | Category |
|--------------------|--------|------------------|----------|----------|
| `DI_Area_Riser`    | Number | Other or Custom  | ✅ Yes   | Stairs   |
| `DI_Area_Tread`    | Number | Other or Custom  | ✅ Yes   | Stairs   |

You can create them via **Manage → Project Parameters → Add**.
Make sure both are marked as **Instance parameters** and are assigned to the **Stairs** category.

## 🛠️ Usage
1. In Revit, select a stair element.
2. Run the plugin from the Add-Ins tab.
3. The plugin:
   - Computes horizontal and vertical surface areas
   - Writes the values into `DI_Area_Riser` and `DI_Area_Tread`
   - Displays the result in a message dialog

## 📂 Files
- `Command.cs` – Plugin logic for calculations and parameter updates
- `App.cs` – Entry point for Revit (no ribbon UI in this version)
- `StairsAreaPlugin.csproj` – Visual Studio project file
- `StairsAreaPlugin.addin` – Revit manifest for registering the plugin

## 📦 License
MIT License – Free to use, modify, and share.

## 🤝 Credits
Created by Piotr Kocoń 🇵🇱

---
*Need help building or expanding this plugin? Reach out or fork it on GitHub!*

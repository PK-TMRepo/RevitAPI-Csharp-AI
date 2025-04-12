# StairsAreaPlugin

A lightweight Revit plugin that calculates the surface area of **stairs** and **ramps** and writes the results to custom instance parameters.

---

## 🚀 Features

- **Stairs:**
  - Calculates total **horizontal** area (treads)
  - Calculates total **vertical** area (risers)
  - Writes results to:
    - `DI_Area_Riser` — horizontal surface area (in m²)
    - `DI_Area_Tread` — vertical surface area (in m²)

- **Ramp:**
  - Calculates top surface area
  - Writes result to:
    - `DI_Area` — total surface area (in m²)

---

## 🧰 Requirements

You must manually add the following **Instance Parameters** to your Revit project:

| Parameter Name     | Applies To | Type     | Is Instance? |
|--------------------|------------|----------|--------------|
| `DI_Area_Riser`    | Stairs     | Number   | ✅ Yes       |
| `DI_Area_Tread`    | Stairs     | Number   | ✅ Yes       |
| `DI_Area`          | Ramp       | Number   | ✅ Yes       |

To add them:
- Go to **Manage > Project Parameters > Add**
- Set them as **Instance**
- Assign to category **Stairs** or **Ramps** respectively

---

## 🛠 Installation

1. Open the `.csproj` in **Visual Studio**
2. Build using **.NET Framework 4.8**
3. Copy the output `.dll` to:

   ```
   %AppData%\Autodesk\Revit\Addins\2025
   ```

4. Place the `.addin` file in the same folder

---

## ▶️ Usage

1. Open Revit and select **one** element (stair or ramp)
2. Run the plugin from the **Add-Ins tab**
3. Choose the correct type in the dialog box
4. The plugin calculates surface areas and writes them to the instance parameters

---

## 📁 Files

- `Command.cs` – Plugin logic (calculations, parameter writing)
- `App.cs` – Entry point
- `StairsAreaPlugin.csproj` – Visual Studio project
- `StairsAreaPlugin.addin` – Revit Add-In manifest
- `README.md` – This documentation

---

## 📖 License

MIT License – Free to use, modify and distribute.

---

## 👤 Author

Created by **Piotr Kocoń** 🇵🇱  
For support or contributions, feel free to fork the repository or open an issue.

# WallLayerAnalyzer (v0.2 BETA)

**WallLayerAnalyzer** is a Revit plugin that analyzes wall layers and uses OpenAI to generate professional material descriptions based on real product names and manufacturers used in your project. The goal is to enhance project documentation with meaningful AI-generated insights.

> 🔬 Version: `v0.2` – Early development version with functional AI-based descriptions. U-value calculations are still experimental.

---

## 🧠 Main Features

- Reads compound wall layers directly from selected walls in Autodesk Revit.
- Detects material names and prompts OpenAI to:
  - Generate a **technical description** of the material (application, properties, manufacturers, standards).
  - Include real product references when available.
- (Experimental) Calculates **U-value estimates** based on layer thickness and material.
- (Experimental) Checks compliance with **Polish WT 2024** building standards.
- Displays all data in a simple **Windows Forms interface** embedded into Revit.

---

## ⚙️ Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/WallLayerAnalyzer.git
   ```

2. Open the solution in Visual Studio 2022.

3. Make sure you have:
   - Autodesk Revit 2025 API DLLs referenced.
   - .NET Framework 4.8
   - An active OpenAI API key.

4. Replace your API key in `OpenAIService.cs`:
   ```csharp
   private readonly string _apiKey = "YOUR OPEN AI key";
   ```

5. Build the solution. Load the compiled `.dll` file into Revit as an external plugin.

---

## 🧩 Revit Plugin Setup (.addin file)

To run the plugin in Autodesk Revit, you need to place a `.addin` file in your Revit Addins folder, typically:

```
C:\ProgramData\Autodesk\Revit\Addins\2025\
```

Here’s a sample content for your `WallLayerAnalyzer.addin` file:

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<RevitAddIns>
  <AddIn Type="Command">
    <Name>WallLayerAnalyzer</Name>
    <Assembly>C:\Path\To\Your\WallLayerAnalyzer.dll</Assembly>
    <AddInId>12ef03d3-8510-4d0a-a6c6-ff1f62e4d5c3</AddInId>
    <FullClassName>WallLayerAnalyzer.ExternalCommand</FullClassName>
    <VendorId>YOUR_ID</VendorId>
    <VendorDescription>YOUR_COMPANY_OR_NAME</VendorDescription>
  </AddIn>
</RevitAddIns>
```

> ⚠️ **Important**: Update the path in `<Assembly>` to point to the actual location of your compiled `.dll` file. This path is unique to your machine.

---

## 📝 Notes for Best Results

- Use **real material names and manufacturer names** in your Revit project.
  - Example: _"Knauf Insulation TP115 Mineral Wool"_ or _"Wienerberger Porotherm 25 P+W"_.
- The more specific the material name, the more accurate the AI-generated description will be.

---

## 🧪 Development Roadmap

- ✅ Material description via AI
- 🔄 U-value estimation (W/m²K) – *in progress*
- 🔄 WT 2024 compliance feedback – *in progress*
- 🧾 PDF/Word export – *planned*
- 🌐 Online reference links – *planned*

---

## 📁 Project Structure

```
WallLayerAnalyzer/
│
├── ExternalCommand.cs       # Connects the plugin to Revit UI
├── MainForm.cs              # Windows Form interface
├── OpenAIService.cs         # AI prompt and response handler
├── WallLayerAnalyzer.csproj # C# project file
├── WallLayerAnalyzer.sln    # Visual Studio solution file
├── packages.config          # NuGet dependencies
└── README.md                # This file
```

---

## 📜 License

This project is open-source and free to use and modify. Please give credit when publishing derivative versions.

---

## 👨‍💻 Author

Developed by **Piotr Kocoń**  
Feedback and contributions are welcome.
# MyRevitCommands

**MyRevitCommands** is a lightweight Revit plugin that allows you to select any element in your Revit model and instantly display its `ElementId`. This is useful for debugging, automation, or working with custom selection logic in your BIM workflows.

> 📦 Version: `v0.1` – Simple working command to get started with custom Revit plugins.

---

## ⚙️ Features

- Allows user to click an element in Revit.
- Displays the selected element's `ElementId` in a dialog box.
- Lightweight and fast – ideal for testing Revit API setup.

---

## 🧩 Installation

### 1. Build the Plugin

Open the solution `MyRevitCommands.sln` in Visual Studio 2022:

- Make sure you have Autodesk Revit 2025 API DLLs referenced.
- Set configuration to `Release`.
- Build the solution to generate the `.dll`.

### 2. Create `.addin` File

Place the provided `MyRevitCommands.addin` file in:

```
C:\ProgramData\Autodesk\Revit\Addins\2025\
```

Make sure to update the `<Assembly>` path in the `.addin` file to point to your local DLL location.

```xml
<Assembly>C:\Path\To\Your\MyRevitCommands.dll</Assembly>
```

---

## 💻 Usage

1. Open Revit 2025.
2. Load a project and select the plugin from the "Add-Ins" tab.
3. Click an element in the model.
4. A dialog box will display its `ElementId`.

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

This project is for personal or internal use only. Copying, distributing, or using this plugin without permission is prohibited.
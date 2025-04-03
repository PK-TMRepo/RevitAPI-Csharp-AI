using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallLayerAnalyzer
{
    public partial class MainForm : System.Windows.Forms.Form, IDisposable
    {
        private readonly UIDocument _uidoc;
        private readonly Document _doc;
        private System.Windows.Forms.DataGridView dataGridViewLayers;

        public MainForm(UIDocument uidoc)
        {
            _uidoc = uidoc;
            _doc = uidoc.Document;
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            this.dataGridViewLayers = new System.Windows.Forms.DataGridView
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            };

            this.Controls.Add(this.dataGridViewLayers);

            // Add columns
            this.dataGridViewLayers.Columns.Add("Function", "Function");
            this.dataGridViewLayers.Columns.Add("Material", "Material");
            this.dataGridViewLayers.Columns.Add("Thickness", "Thickness [mm]");
            this.dataGridViewLayers.Columns.Add("Description", "AI Description");
            this.dataGridViewLayers.Columns.Add("UValue", "U-value [W/m²K]");
            this.dataGridViewLayers.Columns.Add("WTComment", "WT 2024 Comment");
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await AnalyzeWallLayersAsync();
        }

        private async Task AnalyzeWallLayersAsync()
        {
            try
            {
                var wall = GetSelectedWall();
                if (wall == null) return;

                var cs = wall.WallType.GetCompoundStructure();
                if (cs == null)
                {
                    System.Windows.Forms.MessageBox.Show("Selected wall has no compound structure.", "Error",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                this.dataGridViewLayers.Rows.Clear();
                OpenAIService aiService = new OpenAIService();

                foreach (CompoundStructureLayer layer in cs.GetLayers())
                {
                    Material material = _doc.GetElement(layer.MaterialId) as Material;
                    string materialName = material?.Name ?? "(no material)";
                    double thicknessMm = layer.Width * 1000;

                    var descriptionTask = aiService.GetMaterialDescriptionAsync(materialName, wall.WallType.Name);
                    var uValueTask = aiService.GetUValueAnalysisAsync(materialName, thicknessMm);
                    await Task.WhenAll(descriptionTask, uValueTask);

                    this.dataGridViewLayers.Rows.Add(
                        layer.Function.ToString(),
                        materialName,
                        thicknessMm.ToString("F1"),
                        descriptionTask.Result,
                        uValueTask.Result.Item1?.ToString("F3"),
                        uValueTask.Result.Item2
                    );
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error: {ex.Message}", "Error",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private Wall GetSelectedWall()
        {
            try
            {
                var selectedIds = _uidoc.Selection.GetElementIds();
                if (selectedIds.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show("Please select a wall first.", "No Selection",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return null;
                }

                Element element = _doc.GetElement(selectedIds.First());
                if (element is Wall wall)
                {
                    return wall;
                }

                System.Windows.Forms.MessageBox.Show("The selected element is not a wall.", "Invalid Selection",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return null;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error selecting wall: {ex.Message}", "Error",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
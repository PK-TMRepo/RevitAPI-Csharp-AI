using System;
using System.Windows.Forms;

public partial class RoomCheckForm : Form
{
    private double windowArea;
    private double floorArea;
    private int windowCount;

    public RoomCheckForm(double windowArea, double floorArea, int windowCount)
    {
        InitializeComponent();
        this.windowArea = windowArea;
        this.floorArea = floorArea;
        this.windowCount = windowCount;
    }

    private void RoomCheckForm_Load(object sender, EventArgs e)
    {
        rbStaly.Checked = true;
    }

    private void btnSprawdz_Click(object sender, EventArgs e)
    {
        double ratio = floorArea > 0 ? windowArea / floorArea : 0;
        string status = rbStaly.Checked
            ? (ratio >= 0.125 ? "✅ SPEŁNIA 1/8 dla stałego pobytu" : "❌ NIE SPEŁNIA 1/8!")
            : "ℹ️ Sprawdzenie informacyjne (czasowy pobyt)";

        lblWynik.Text =
            $"Powierzchnia podłogi: {floorArea:F2} m²\n" +
            $"Powierzchnia okien: {windowArea:F2} m²\n" +
            $"Liczba okien: {windowCount}\n" +
            $"Stosunek: {(ratio * 100):F1}%\n" +
            status;
    }
}

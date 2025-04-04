using System.Windows.Forms;

partial class RoomCheckForm
{
    private System.ComponentModel.IContainer components = null;
    private RadioButton rbStaly;
    private RadioButton rbCzasowy;
    private Button btnSprawdz;
    private Label lblWynik;

    private void InitializeComponent()
    {
        this.rbStaly = new System.Windows.Forms.RadioButton();
        this.rbCzasowy = new System.Windows.Forms.RadioButton();
        this.btnSprawdz = new System.Windows.Forms.Button();
        this.lblWynik = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // rbStaly
        // 
        this.rbStaly.AutoSize = true;
        this.rbStaly.Location = new System.Drawing.Point(20, 20);
        this.rbStaly.Name = "rbStaly";
        this.rbStaly.Size = new System.Drawing.Size(180, 17);
        this.rbStaly.TabIndex = 0;
        this.rbStaly.TabStop = true;
        this.rbStaly.Text = "Pomieszczenie na stały pobyt";
        this.rbStaly.UseVisualStyleBackColor = true;
        // 
        // rbCzasowy
        // 
        this.rbCzasowy.AutoSize = true;
        this.rbCzasowy.Location = new System.Drawing.Point(20, 50);
        this.rbCzasowy.Name = "rbCzasowy";
        this.rbCzasowy.Size = new System.Drawing.Size(205, 17);
        this.rbCzasowy.TabIndex = 1;
        this.rbCzasowy.Text = "Pomieszczenie na czasowy pobyt";
        this.rbCzasowy.UseVisualStyleBackColor = true;
        // 
        // btnSprawdz
        // 
        this.btnSprawdz.Location = new System.Drawing.Point(20, 80);
        this.btnSprawdz.Name = "btnSprawdz";
        this.btnSprawdz.Size = new System.Drawing.Size(100, 23);
        this.btnSprawdz.TabIndex = 2;
        this.btnSprawdz.Text = "Sprawdź";
        this.btnSprawdz.UseVisualStyleBackColor = true;
        this.btnSprawdz.Click += new System.EventHandler(this.btnSprawdz_Click);
        // 
        // lblWynik
        // 
        this.lblWynik.AutoSize = true;
        this.lblWynik.Location = new System.Drawing.Point(20, 120);
        this.lblWynik.Name = "lblWynik";
        this.lblWynik.Size = new System.Drawing.Size(0, 13);
        this.lblWynik.TabIndex = 3;
        // 
        // RoomCheckForm
        // 
        this.ClientSize = new System.Drawing.Size(380, 200);
        this.Controls.Add(this.rbStaly);
        this.Controls.Add(this.rbCzasowy);
        this.Controls.Add(this.btnSprawdz);
        this.Controls.Add(this.lblWynik);
        this.Name = "RoomCheckForm";
        this.Load += new System.EventHandler(this.RoomCheckForm_Load);
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}

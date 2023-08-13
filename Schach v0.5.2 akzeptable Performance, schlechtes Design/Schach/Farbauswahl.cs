using System.Windows.Forms;

namespace Schach
{
    public partial class FarbauswahlForm : Form
    {
        Button btnWeiss;
        Button btnSchwarz;
        Button btnSpieler;
        Button btnBeide;
        TextBox txtFarbauswahl;

        public FarbauswahlForm()
        {
            InitializeComponent();
        }

        private void btnWeiss_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnSchwarz_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void btnSpieler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void btnBeide_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void InitializeComponent()
        {
            btnWeiss = new Button();
            btnSchwarz = new Button();
            btnSpieler = new Button();
            btnBeide = new Button();
            txtFarbauswahl = new TextBox();
            SuspendLayout();
            // 
            // btnWeiss
            // 
            btnWeiss.Location = new Point(61, 29);
            btnWeiss.Name = "btnWeiss";
            btnWeiss.Size = new Size(75, 23);
            btnWeiss.TabIndex = 0;
            btnWeiss.Text = "Weiß";
            btnWeiss.Click += btnWeiss_Click;
            // 
            // btnSchwarz
            // 
            btnSchwarz.Location = new Point(61, 58);
            btnSchwarz.Name = "btnSchwarz";
            btnSchwarz.Size = new Size(75, 23);
            btnSchwarz.TabIndex = 1;
            btnSchwarz.Text = "Schwarz";
            btnSchwarz.Click += btnSchwarz_Click;
            // 
            // btnSpieler
            // 
            btnSpieler.Location = new Point(61, 87);
            btnSpieler.Name = "btnSpieler";
            btnSpieler.Size = new Size(75, 23);
            btnSpieler.TabIndex = 2;
            btnSpieler.Text = "Keine";
            btnSpieler.Click += btnSpieler_Click;
            // 
            // btnBeide
            // 
            btnBeide.Location = new Point(61, 116);
            btnBeide.Name = "btnBeide";
            btnBeide.Size = new Size(75, 23);
            btnBeide.TabIndex = 3;
            btnBeide.Text = "Beide";
            btnBeide.Click += btnBeide_Click;
            // 
            // txtFarbauswahl
            // 
            txtFarbauswahl.Location = new Point(0, 0);
            txtFarbauswahl.Name = "txtFarbauswahl";
            txtFarbauswahl.Size = new Size(200, 23);
            txtFarbauswahl.TabIndex = 4;
            txtFarbauswahl.Text = "Welche Farbe soll der Bot spielen?";
            // 
            // FarbauswahlForm
            // 
            ClientSize = new Size(199, 145);
            Controls.Add(btnWeiss);
            Controls.Add(btnSchwarz);
            Controls.Add(btnSpieler);
            Controls.Add(btnBeide);
            Controls.Add(txtFarbauswahl);
            Name = "FarbauswahlForm";
            Text = "Farbauswahl für den Bot";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

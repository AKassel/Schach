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
            this.btnWeiss = new System.Windows.Forms.Button();
            this.btnSchwarz = new System.Windows.Forms.Button();
            this.btnSpieler = new System.Windows.Forms.Button();
            this.btnBeide = new System.Windows.Forms.Button();
            txtFarbauswahl = new TextBox();
            this.txtFarbauswahl.Size = new System.Drawing.Size(200, 40);
            txtFarbauswahl.Text = "Welche Farbe soll der Bot spielen?";
            Text = "Farbauswahl für den Bot";
            // Konfiguration der Buttons
            this.btnWeiss.Text = "Weiß";
            this.btnWeiss.Location = new System.Drawing.Point(10, 40);
            this.btnWeiss.Click += new System.EventHandler(this.btnWeiss_Click);

            this.btnSchwarz.Text = "Schwarz";
            this.btnSchwarz.Location = new System.Drawing.Point(10, 70);
            this.btnSchwarz.Click += new System.EventHandler(this.btnSchwarz_Click);

            this.btnSpieler.Text = "Keine";
            this.btnSpieler.Location = new System.Drawing.Point(10, 100);
            this.btnSpieler.Click += new System.EventHandler(this.btnSpieler_Click);

            this.btnBeide.Text = "Beide";
            this.btnBeide.Location = new System.Drawing.Point(10, 130);
            this.btnBeide.Click += new System.EventHandler(this.btnBeide_Click);

            // Weitere Komponenten hinzufügen und konfigurieren

            this.SuspendLayout();
            // Anordnung der Komponenten im Formular
            this.Controls.Add(btnWeiss);
            this.Controls.Add(btnSchwarz);
            this.Controls.Add(btnSpieler);
            this.Controls.Add(btnBeide);
            this.Controls.Add(txtFarbauswahl);
            this.ResumeLayout(false);

            // Weitere Konfiguration und Initialisierungen hier ...
        }
    }
}

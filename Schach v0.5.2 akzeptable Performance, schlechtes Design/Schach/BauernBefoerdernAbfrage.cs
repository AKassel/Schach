using System.Windows.Forms;

namespace Schach
{
    public partial class BauerBefoerdernAbfrage : Form
    {
        Button Dame;
        Button Laeufer;
        Button Springer;
        Button Turm;
        TextBox txtFigurenAuswahl;

        public BauerBefoerdernAbfrage()
        {
            InitializeComponent();
        }

        private void Dame_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void Laeufer_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void Springer_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void Turm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void InitializeComponent()
        {
            this.Dame = new System.Windows.Forms.Button();
            this.Laeufer = new System.Windows.Forms.Button();
            this.Springer = new System.Windows.Forms.Button();
            this.Turm = new System.Windows.Forms.Button();
            txtFigurenAuswahl = new TextBox();
            this.txtFigurenAuswahl.Size = new System.Drawing.Size(200, 40);
            txtFigurenAuswahl.Text = "Wozu soll befördert werden?";
            Text = "Bauern befördern";
            // Konfiguration der Buttons
            this.Dame.Text = "Dame";
            this.Dame.Location = new System.Drawing.Point(10, 40);
            this.Dame.Click += new System.EventHandler(this.Dame_Click);

            this.Laeufer.Text = "Laeufer";
            this.Laeufer.Location = new System.Drawing.Point(10, 70);
            this.Laeufer.Click += new System.EventHandler(this.Laeufer_Click);

            this.Springer.Text = "Springer";
            this.Springer.Location = new System.Drawing.Point(10, 100);
            this.Springer.Click += new System.EventHandler(this.Springer_Click);

            this.Turm.Text = "Turm";
            this.Turm.Location = new System.Drawing.Point(10, 130);
            this.Turm.Click += new System.EventHandler(this.Turm_Click);

            // Weitere Komponenten hinzufügen und konfigurieren

            this.SuspendLayout();
            // Anordnung der Komponenten im Formular
            this.Controls.Add(Dame);
            this.Controls.Add(Laeufer);
            this.Controls.Add(Springer);
            this.Controls.Add(Turm);
            this.Controls.Add(txtFigurenAuswahl);
            this.ResumeLayout(false);

            // Weitere Konfiguration und Initialisierungen hier ...
        }
    }
}

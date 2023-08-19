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
            Dame = new Button();
            Laeufer = new Button();
            Springer = new Button();
            Turm = new Button();
            txtFigurenAuswahl = new TextBox();
            SuspendLayout();
            // 
            // Dame
            // 
            Dame.Location = new Point(48, 28);
            Dame.Name = "Dame";
            Dame.Size = new Size(75, 23);
            Dame.TabIndex = 0;
            Dame.Text = "Dame";
            Dame.Click += Dame_Click;
            // 
            // Laeufer
            // 
            Laeufer.Location = new Point(48, 57);
            Laeufer.Name = "Laeufer";
            Laeufer.Size = new Size(75, 23);
            Laeufer.TabIndex = 1;
            Laeufer.Text = "Laeufer";
            Laeufer.Click += Laeufer_Click;
            // 
            // Springer
            // 
            Springer.Location = new Point(48, 86);
            Springer.Name = "Springer";
            Springer.Size = new Size(75, 23);
            Springer.TabIndex = 2;
            Springer.Text = "Springer";
            Springer.Click += Springer_Click;
            // 
            // Turm
            // 
            Turm.Location = new Point(48, 115);
            Turm.Name = "Turm";
            Turm.Size = new Size(75, 23);
            Turm.TabIndex = 3;
            Turm.Text = "Turm";
            Turm.Click += Turm_Click;
            // 
            // txtFigurenAuswahl
            // 
            txtFigurenAuswahl.Location = new Point(0, -1);
            txtFigurenAuswahl.Name = "txtFigurenAuswahl";
            txtFigurenAuswahl.Size = new Size(200, 23);
            txtFigurenAuswahl.TabIndex = 4;
            txtFigurenAuswahl.Text = "Wozu soll befördert werden?";
            txtFigurenAuswahl.TextChanged += txtFigurenAuswahl_TextChanged;
            // 
            // BauerBefoerdernAbfrage
            // 
            ClientSize = new Size(202, 158);
            Controls.Add(Dame);
            Controls.Add(Laeufer);
            Controls.Add(Springer);
            Controls.Add(Turm);
            Controls.Add(txtFigurenAuswahl);
            Name = "BauerBefoerdernAbfrage";
            Text = "Bauern befördern";
            ResumeLayout(false);
            PerformLayout();
        }

        private void txtFigurenAuswahl_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System.ComponentModel.Design;
using System.Diagnostics;
using System.Windows.Forms;

namespace Schach
{
    partial class Schach
    {
        string basePath = null;
        string imagesPath = null;

        Image weisserKoenig = null;
        Image weisseDame = null;
        Image weisserLaeufer = null;
        Image weisserSpringer = null;
        Image weisserTurm = null;
        Image weisserBauer = null;

        Image schwarzerKoenig = null;
        Image schwarzeDame = null;
        Image schwarzerLaeufer = null;
        Image schwarzerSpringer = null;
        Image schwarzerTurm = null;
        Image schwarzerBauer = null;
        //PlaceFigureOnField(0, 0, kingImage);

        Bot bot;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        public Panel[,] chessBoardPanels; 
        private Figur selectedFigur = null;
        List<Zug> moeglicheZuege;
        Panel sourcePanel = null;
        Panel targetPanel = null;
        private bool isDragging;
        
        //Mit true bedeutet der Bot spielt weiss, false bedeutet der Bot spielt schwarz.

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 8;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 8;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel.Size = new Size(800, 800);
            tableLayoutPanel.TabIndex = 0;
            // 
            // Schach
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 700);
            Controls.Add(tableLayoutPanel);
            Name = "Schach";
            Text = "Schachfeld";
            ResumeLayout(false);
        }
        private void Schach_Load(object sender, EventArgs e)
        {
            
        }
        #endregion
        public void CreateChessBoard()
        {
            FarbauswahlForm farbForm = new FarbauswahlForm();
            DialogResult result = farbForm.ShowDialog();

            if (result == DialogResult.Yes)
            {
                bot = new Bot(true, new Schachfeld(chessBoardPanels), true);
            }
            else if (result == DialogResult.No)
            {
                bot = new Bot(false, new Schachfeld(chessBoardPanels), true);
            }
            else if (result == DialogResult.Cancel)
            {
                bot = new Bot(false, new Schachfeld(chessBoardPanels), false);
            }
            else if (result == DialogResult.OK)
            {
                bot = new Bot(true, new Schachfeld(chessBoardPanels), false);
            }
            this.SuspendLayout();
            
            // Erstellen Sie das Schachfeld (TableLayoutPanel)
            chessBoardPanels = new Panel[8, 8];
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Panel panel = new Panel();
                    panel.Dock = DockStyle.Fill;
                    panel.BackColor = (row + col) % 2 == 0 ? Color.Silver : Color.Black;
                    this.tableLayoutPanel.Controls.Add(panel, col, row);
                    this.chessBoardPanels[row, col] = panel;
                }
            }

            //Figuren platzieren
            PlaceFigureOnField(new Turm(true, 7, 7, weisserTurm));
            PlaceFigureOnField(new Springer(true, 7, 6, weisserSpringer));
            PlaceFigureOnField(new Laeufer(true, 7, 5, weisserLaeufer));
            PlaceFigureOnField(new Koenig(true, 7, 4, weisserKoenig));
            PlaceFigureOnField(new Dame(true, 7, 3, weisseDame));
            PlaceFigureOnField(new Laeufer(true, 7, 2, weisserLaeufer));
            PlaceFigureOnField(new Springer(true, 7, 1, weisserSpringer));
            PlaceFigureOnField(new Turm(true, 7, 0, weisserTurm));
            for (int i = 0; i < 8; i++)
            {
                PlaceFigureOnField(new Bauer(true, 6, i, weisserBauer));
            }


            PlaceFigureOnField(new Turm(false, 0, 7, schwarzerTurm));
            PlaceFigureOnField(new Springer(false, 0, 6, schwarzerSpringer));
            PlaceFigureOnField(new Laeufer(false, 0, 5, schwarzerLaeufer));
            PlaceFigureOnField(new Koenig(false, 0, 4, schwarzerKoenig));
            PlaceFigureOnField(new Dame(false, 0, 3, schwarzeDame));
            PlaceFigureOnField(new Laeufer(false, 0, 2, schwarzerLaeufer));
            PlaceFigureOnField(new Springer(false, 0, 1, schwarzerSpringer));
            PlaceFigureOnField(new Turm(false, 0, 0, schwarzerTurm));
            for (int i = 0; i < 8; i++)
            {
                PlaceFigureOnField(new Bauer(false, 1, i, schwarzerBauer));
            }

            this.ResumeLayout(false);
            
        }
        public void Ziehen()
        {
            if (bot.weiss == bot.schachfeld.weissAmZug)
            {
                    bot.Ziehen();
            }
        }
        private void PlaceFigureOnField(Figur figur)
        {
            bot.schachfeld.schachfeld = chessBoardPanels;
            // Erstellen Sie eine neue PictureBox
            figur.SizeMode = PictureBoxSizeMode.StretchImage;
            figur.Dock = DockStyle.Fill;
            figur.MouseDown += new MouseEventHandler(ChessBoard_MouseDown);
            figur.MouseUp += new MouseEventHandler(ChessBoard_MouseUp);
            figur.MouseMove += new MouseEventHandler(ChessBoard_MouseMove);

            // Fügen Sie die PictureBox zum Panel hinzu
            chessBoardPanels[figur.row,figur.col].Controls.Add(figur);
            if (figur.weiss)
            {
                bot.schachfeld.WeisseFiguren.Add(figur);
            }
            else
            {
                bot.schachfeld.SchwarzeFiguren.Add(figur);
            }
        }
        private void RemoveFigureFromField(Figur figur1)
        {
            Panel panel = chessBoardPanels[figur1.row, figur1.col];
            if (panel != null)
            {
                panel.Controls.Remove(figur1);
                figur1.Dispose();
            }
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            await Task.Delay(50);
                Ziehen();
            
        }

        public void ChessBoard_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (sender is Panel panel1)
            {
                isDragging = false;
            }
            if (sender is Figur figur && !isDragging && figur.weiss == bot.schachfeld.weissAmZug)
            {
                selectedFigur = figur;
                isDragging = true;

                sourcePanel = bot.schachfeld.schachfeld[figur.row, figur.col];
                if(bot.schachfeld.schachfeld != chessBoardPanels)
                {

                }
                moeglicheZuege = selectedFigur.ZuegetrotzSchachVerbieten(bot.schachfeld);

            }
        }
        
        public void ChessBoard_MouseMove(object sender, MouseEventArgs e)
        {
            //Wenn MouseDown geklickt wurde, wird die Figur, auf die geklickt wurde, auf das Pannel geaddet, auf dem die Maus ist.

            if (isDragging)
            {
                //Ueberfluessige Bedingung
                if (sender == selectedFigur)
                {
                    
                    Point mousePosition = tableLayoutPanel.PointToClient(Cursor.Position);
                    Control control = tableLayoutPanel.GetChildAtPoint(mousePosition);
                    if (control is Panel)
                    {
                        targetPanel = (Panel)control;
                    }
                    if (targetPanel != null)
                    {
                        targetPanel.Controls.Add(selectedFigur);
                    }
                }
            }
            else
            {
                selectedFigur = null;
                targetPanel = null;
            }
        }

        public async void ChessBoard_MouseUp(object receiver, MouseEventArgs e)
        {
            isDragging = false;
            //targetPanel suchen
            //Das target Panel Conatained die Figur schon, da die Fiugr in Mouse move bewegt wird
            if (selectedFigur != null && selectedFigur.weiss == bot.schachfeld.weissAmZug)
            {
                if (targetPanel != null)
                {
                    int row = tableLayoutPanel.GetRow(targetPanel);
                    int col = tableLayoutPanel.GetColumn(targetPanel);
                    if (row != -1 && col != -1)
                    {
                        Zug zug = new Zug(selectedFigur,row,col);
                        foreach(Zug MZug in moeglicheZuege)
                        {
                            if(MZug.row == row && MZug.col == col)
                            {
                                zug = MZug;
                            }
                        }
                        //Pruefung ob der Zug legal ist, row und col sind von dem Feld auf das gezogen wird 
                        if (moeglicheZuege.Contains(zug))
                        {
                            if (selectedFigur is Bauer bauer && (row == 7 || row == 0))
                            {
                                //Hier muesste man irgendwie waehlen kann wozu man befoerdern will
                                BauerBefoerdernAbfrage befoerdernAbfrage = new BauerBefoerdernAbfrage();
                                DialogResult result = befoerdernAbfrage.ShowDialog();

                                if (result == DialogResult.Yes)
                                {
                                    if (bauer.weiss)
                                    {
                                        zug = new Zug(selectedFigur, row, col, new Dame(bauer.weiss, row, col, weisseDame));
                                    }
                                    else
                                    {
                                        zug = new Zug(selectedFigur, row, col, new Dame(bauer.weiss, row, col, schwarzeDame));
                                    }
                                }
                                else if (result == DialogResult.No)
                                {
                                    if (bauer.weiss)
                                    {
                                        zug = new Zug(selectedFigur, row, col, new Laeufer(bauer.weiss, row, col, weisserLaeufer));
                                    }
                                    else
                                    {
                                        zug = new Zug(selectedFigur, row, col, new Laeufer(bauer.weiss, row, col, schwarzerLaeufer));
                                    }
                                }
                                else if (result == DialogResult.Cancel)
                                {
                                    if (bauer.weiss)
                                    {
                                        zug = new Zug(selectedFigur, row, col, new Springer(bauer.weiss, row, col, weisserSpringer));
                                    }
                                    else
                                    {
                                        zug = new Zug(selectedFigur, row, col, new Springer(bauer.weiss, row, col, schwarzerSpringer));
                                    }
                                }
                                else if(result == DialogResult.OK)
                                {
                                    if (bauer.weiss)
                                    {
                                        zug = new Zug(selectedFigur, row, col, new Turm(bauer.weiss, row, col, weisserTurm));
                                    }
                                    else
                                    {
                                        zug = new Zug(selectedFigur, row, col, new Turm(bauer.weiss, row, col, schwarzerTurm));
                                    }
                                }
                                
                            }
                            targetPanel.Controls.Remove(selectedFigur);
                            bot.ZugMachen(zug, bot.schachfeld);
                            if (zug.befoerdert != null)
                            {
                                RemoveFigureFromField(zug.figur);
                                PlaceFigureOnField(zug.befoerdert);
                                
                            }
                        }
                        else
                        {
                            //Wenn der Zug nicht erlaubt ist, wird die Figur zurueck aufs sourcePanel gesetzt
                            //und von dem nicht erlaubtem Feld entfernt
                            //Esseiden sie wurde garnicht bewegt
                            //Stimmt aber scheinbar alles garnicht und das ist nutzlos geworden
                            if (sourcePanel != targetPanel)
                            {
                                //RemoveFigureFromField(selectedFigur);
                            }

                            selectedFigur.row = tableLayoutPanel.GetRow(sourcePanel);
                            selectedFigur.col = tableLayoutPanel.GetColumn(sourcePanel);
                            PlaceFigureOnField(selectedFigur);
                        }
                    }
                }
            }
            targetPanel = null;
            selectedFigur = null;
            sourcePanel = null;
        }

        // Beispiel für das Anfügen der Ereignishandler an die PictureBox-Objekte
        public void AttachEventHandlersToPanels()
        {
            foreach (Panel panel in chessBoardPanels)
            {
                panel.AllowDrop = true; // Erlauben des Drag & Drop für das Panel
                panel.MouseDown += new MouseEventHandler(ChessBoard_MouseDown);
                panel.MouseUp += new MouseEventHandler(ChessBoard_MouseUp);
                panel.MouseMove += new MouseEventHandler(ChessBoard_MouseMove);
            }
        }
    }
}
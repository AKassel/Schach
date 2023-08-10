using System.ComponentModel.Design;
using System.Diagnostics;
using System.Windows.Forms;

namespace Schach
{
    partial class Schach
    {
        // Beispielaufruf, um den König auf das Feld (0, 0) zu platzieren
        Image weisserKoenig = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\king.png"); // Bild der Königsfigur
        Image weisseDame = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\Dame.png");
        Image weisserLaeufer = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\Laeufer.png");
        Image weisserSpringer = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\Springer.png");
        Image weisserTurm = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\Turm.png");
        Image weisserBauer = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\Bauer.png");

        Image schwarzerKoenig = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\SKingL.png"); // Bild der Königsfigur
        Image schwarzeDame = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\SDame.png");
        Image schwarzerLaeufer = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\SLaeufer.png");
        Image schwarzerSpringer = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\SSpringer.png");
        Image schwarzerTurm = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\STurm.png");
        Image schwarzerBauer = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\SBauer.png");
        //PlaceFigureOnField(0, 0, kingImage);

        Bot bot;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private Panel[,] chessBoardPanels; 
        private Figur selectedFigur;
        List<Zug> moeglicheZuege;
        Panel sourcePanel = null;
        Panel targetPanel = null;
        private bool isDragging;
        
        //Mit true bedeutet der Bot spielt weiss, false bedeutet der Bot spielt schwarz.

        public void Ziehen()
        {
            bot.BessererZug(bot.schachfeld,2);
        }
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

        #endregion
        public void CreateChessBoard()
        {
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

            //Hier koennte etwas eingebaut werden, dass abfragt welche farbe der Bott spielen sollte
            bot = new Bot(false, new Schachfeld(chessBoardPanels));


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
            if (bot.weiss==bot.schachfeld.weissAmZug)
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
            bot.schachfeld.schachfeld[figur.row,figur.col].Controls.Add(figur);
            if(chessBoardPanels[figur.row, figur.col] != bot.schachfeld.schachfeld[figur.row, figur.col])
            {

            }
            if (figur.weiss)
            {
                bot.schachfeld.WeisseFiguren.Add(figur);
            }
            else
            {
                bot.schachfeld.SchwarzeFiguren.Add(figur);
            }
        }
        private void RemoveFigureFromField(int row, int col)
        {
            Panel panel = chessBoardPanels[row, col];

            // Überprüfen, ob das Panel eine PictureBox enthält
            if (panel.Controls.Count > 0 && panel.Controls[0] is Figur figur)
            {
                // PictureBox vom Panel entfernen und freigeben
                panel.Controls.Remove(figur);
                figur.Dispose();
            }
        }
        private void RemoveFigureFromField(Figur figur1, Panel[,] Schachfeld)
        {
            Panel panel = Schachfeld[figur1.row, figur1.col];

            // Überprüfen, ob das Panel eine PictureBox enthält
            if (panel.Contains(figur1))
            {
                panel.Controls.Remove(figur1);
                figur1.Dispose();
            }
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
            if(bot.schachfeld.schachfeld != chessBoardPanels)
            {

            }
        }

        public void ChessBoard_MouseUp(object receiver, MouseEventArgs e)
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
                            //Zug zug;
                            if (selectedFigur is Bauer bauer && (row == 7 || row == 0))
                            {
                                //Hier muesste man irgendwie waehlen kann wozu man befoerdern will
                                if (bauer.weiss) {
                                     zug = new Zug(selectedFigur, row, col, new Dame(bauer.weiss, row, col, weisseDame));
                                }
                                else
                                {
                                     zug = new Zug(selectedFigur, row, col, new Dame(bauer.weiss, row, col, schwarzeDame));
                                }
                            }
                            else
                            {
                                 
                            }
                            //Eigentlich sollte bot.schachfeld() == chessBoardPanels sein, wenn das nicht so ist wäre das... Blöd
                            if (bot.schachfeld.schachfeld != chessBoardPanels)
                            {

                            }

                            bot.ZugMachen(zug, bot.schachfeld);
                            if (zug.befoerdert != null)
                            {
                                RemoveFigureFromField(zug.figur,chessBoardPanels);
                                PlaceFigureOnField(zug.befoerdert);
                            }

                            if ( bot.schachfeld.schachfeld == chessBoardPanels)
                            {

                            }
                           

                        }
                        else
                        {
                            //Wenn der Zug nicht erlaubt ist, wird die Figur zurueck aufs sourcePanel gesetzt
                            //und von dem nicht erlaubtem Feld entfernt
                            //Esseiden sie wurde garnicht bewegt
                            if (sourcePanel != targetPanel)
                            {
                                RemoveFigureFromField(selectedFigur.row, selectedFigur.col);
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schach
{
    abstract internal class Figur : PictureBox
    {
        public bool weiss;
        public int row;
        public int col;
        public int Wert;
        public Figur original;
        Bot bot;

        public Figur(bool weiss, int row, int colomn, Image image) : base() {
            this.weiss = weiss;
            this.row = row;
            this.col = colomn;
            Image = image;
            original = this;
             bot = new Bot();
        }

        abstract public List<Zug> LegaleZuege(Panel[,] Schachfeld);


        public abstract Figur klonen();

        public bool KoenigStehtSchach(Schachfeld Schachfeld)
        {

            foreach (Figur figur in Schachfeld.ListDerFiugrenAmZug())
            {
                //Pruefen ob der koenig schach steht
                foreach (Zug zug in figur.LegaleZuege(Schachfeld.schachfeld))
                {
                    if (Schachfeld.schachfeld[zug.row, zug.col].Controls.OfType<Koenig>().FirstOrDefault() is Koenig koenig)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public bool RochadeGeht(Schachfeld Schachfeld, Zug zug, int AltCol)
        {
            if (!weiss)
            {
                if (zug.col > AltCol)
                {
                    for (int i = AltCol; i < zug.col; i++)
                    {
                        foreach (Figur figur in Schachfeld.WeisseFiguren)
                        {
                            foreach (Zug zug1 in figur.LegaleZuege(Schachfeld.schachfeld))
                            {
                                if (zug1.row == row && zug1.col == i)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = AltCol; i < zug.col; i--)
                    {
                        foreach (Figur figur in Schachfeld.WeisseFiguren)
                        {
                            foreach (Zug zug1 in figur.LegaleZuege(Schachfeld.schachfeld))
                            {
                                if (zug1.row == row && zug1.col == i)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (zug.col > AltCol)
                {
                    for (int i = AltCol; i < zug.col; i++)
                    {
                        foreach (Figur figur in Schachfeld.SchwarzeFiguren)
                        {
                            foreach (Zug zug1 in figur.LegaleZuege(Schachfeld.schachfeld))
                            {
                                if (zug1.row == row && zug1.col == i)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = AltCol; i < zug.col; i--)
                    {
                        foreach (Figur figur in Schachfeld.SchwarzeFiguren)
                        {
                            foreach (Zug zug1 in figur.LegaleZuege(Schachfeld.schachfeld))
                            {
                                if (zug1.row == row && zug1.col == i)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
        public List<Zug> ZuegetrotzSchachVerbieten(Schachfeld schachfeld)
        {
            List<Zug> legaleZuege = LegaleZuege(schachfeld.schachfeld);

            List<Zug> zuegeToRemove = new List<Zug>(); // Züge, die entfernt werden sollen

            foreach (Zug zug in legaleZuege)
            {
                int AltRow = row;
                int AltCol = col;

                bot.ZugMachen(zug, schachfeld);

                if (KoenigStehtSchach(schachfeld))
                {
                    zuegeToRemove.Add(zug);
                }

                if (zug.Rochade)
                {
                    if (!RochadeGeht(schachfeld, zug, AltCol))
                    {
                        zuegeToRemove.Add(zug);
                    }
                }

                bot.ZugRueckgaengigMachen(zug, schachfeld, AltRow, AltCol);
            }

            // Entferne die Züge, die den König bedrohen oder die Rochade unmöglich machen
            foreach (Zug zugToRemove in zuegeToRemove)
            {
                legaleZuege.Remove(zugToRemove);
            }

            return legaleZuege;
        }


    }
}

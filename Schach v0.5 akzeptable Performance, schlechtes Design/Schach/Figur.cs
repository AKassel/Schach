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

            //Schachfeld Schachfeld1 = new Schachfeld(schachfeld);
            //legale Zuege muss mit dem Original Schachfeld aufgerufen werden, da die Rochade und enpassent Panels auf ein Feld gesetzt werden, dass der legaleZuege Methode uebergeben wird
            List<Zug> legaleZuege = LegaleZuege(schachfeld.schachfeld);

            //Pruefen welche Zuege moeglich sind

            List<Zug> Rzuege = new List<Zug>();

            foreach (Zug zug in LegaleZuege(schachfeld.schachfeld))
            {
                int AltRow = row;
                int AltCol = col;
                bot.ZugMachen(zug, schachfeld);

                //Pruefen ob der Koenig dadurch in ein Schach kommt bzw. in einem bleibt
                if (KoenigStehtSchach(schachfeld))
                {
                    foreach(Zug zug1 in legaleZuege)
                    {
                        if(zug.row == zug1.row &&  zug.col == zug1.col && zug.figur == zug1.figur)
                        {
                            Rzuege.Add(zug1);
                        }
                    }
                }
                if (zug.Rochade)
                {
                    if (!RochadeGeht(schachfeld,zug,AltCol))
                    {
                        foreach (Zug zug1 in legaleZuege)
                        {
                            if (zug.row == zug1.row && zug.col == zug1.col && zug.figur == zug1.figur)
                            {
                                Rzuege.Add(zug1);
                            }
                        }
                    }
                }
                bot.ZugRueckgaengigMachen(zug, schachfeld, AltRow, AltCol);
            }
            foreach (Zug zug in Rzuege)
            {
                legaleZuege.Remove(zug);
            }
            return legaleZuege;
        }

    }
}

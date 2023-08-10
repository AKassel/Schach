using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    internal class Turm : Figur
    {
        //public Turm original;
        public bool Bewegt = false;
        public Turm(bool weiss, int row, int colomn, Image image) : base(weiss, row, colomn, image)
        {
            original = this;
            Wert = 5;
        }
        public override Figur klonen()
        {
            Turm Turm = new Turm(weiss, row, col, Image);
            Turm.original = original;
            Turm.Bewegt = Bewegt;
            return Turm;
        }
        override public List<Zug> LegaleZuege(Panel[,] Schachfeld)
        {
            List<Zug> legaleZuege = new List<Zug>();
            //Der Turm kann nach unten ziehen
            for (int i = 1; i < 8 - row; i++)
            {
                if (Schachfeld[row + i, col].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row + i, col].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row + i, col));
                        break;
                    }


                    if (Schachfeld[row + i, col].Controls.OfType<Figur>().FirstOrDefault().weiss == weiss)
                    {
                        break;
                    }
                }
                legaleZuege.Add(new Zug(this, row + i, col));
            }

            // Der Turm kann nach oben ziehen
            for (int i = 1; i <= row; i++)
            {
                if (Schachfeld[row - i, col].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row - i, col].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row - i, col));
                        break;
                    }

                    if (Schachfeld[row - i, col].Controls.OfType<Figur>().FirstOrDefault().weiss == weiss)
                    {
                        break;
                    }
                }
                legaleZuege.Add(new Zug(this, row - i, col));
            }

            // Der Turm kann nach rechts ziehen
            for(int i = 1; i < 8 - col; i++)
            {
                if (Schachfeld[row, col + i].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row, col + i].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row, col + i));
                        break;
                    }
                    if (Schachfeld[row, col + i].Controls.OfType<Figur>().FirstOrDefault().weiss == weiss)
                    {
                        break;
                    }
                }
                legaleZuege.Add(new Zug(this, row, col + i));

            }
            //Der Turm kann nach links ziehen
            for (int i = 1; i <= col; i++)
            {
                if (Schachfeld[row, col - i].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row, col - i].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row, col - i));
                        break;
                    }
                    if (Schachfeld[row, col - i].Controls.OfType<Figur>().FirstOrDefault().weiss == weiss)
                    {
                        break;
                    }
                }
                legaleZuege.Add(new Zug(this, row, col - i));

            }
            return legaleZuege;
        }
        }
}

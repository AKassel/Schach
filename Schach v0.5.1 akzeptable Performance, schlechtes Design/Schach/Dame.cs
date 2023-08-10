using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    internal class Dame : Figur
    {
        //public Dame original;
        public Dame(bool weiss, int row, int colomn, Image image) : base(weiss, row, colomn, image)
        {
            original = this;
            Wert = 9;
        }        
        public override Figur klonen()
        {
            Dame dame = new Dame(weiss, row, col, Image);
            dame.original = original;

            return dame;
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
            for (int i = 1; i < 8 - col; i++)
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
            //Der Laeufer kann nach unten rechts ziehen

            for (int i = 1; i + row < 8 && i + col < 8; i++)
            {
                if (Schachfeld[row + i, col + i].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row + i, col + i].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row + i, col + i));
                        break;
                    }


                    if (Schachfeld[row + i, col + i].Controls.OfType<Figur>().FirstOrDefault().weiss == weiss)
                    {
                        break;
                    }
                }
                legaleZuege.Add(new Zug(this, row + i, col + i));
            }
            //Der Laeufer kann nach unten links ziehen

            for (int i = 1; i + row < 8 && col - i >= 0; i++)
            {
                if (Schachfeld[row + i, col - i].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row + i, col - i].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row + i, col - i));
                        break;
                    }


                    if (Schachfeld[row + i, col - i].Controls.OfType<Figur>().FirstOrDefault().weiss == weiss)
                    {
                        break;
                    }
                }
                legaleZuege.Add(new Zug(this, row + i, col - i));
            }
            //Der Laeufer kann nach oben rechts ziehen

            for (int i = 1; row - i >= 0 && i + col < 8; i++)
            {
                if (Schachfeld[row - i, col + i].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row - i, col + i].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row - i, col + i));
                        break;
                    }


                    if (Schachfeld[row - i, col + i].Controls.OfType<Figur>().FirstOrDefault().weiss == weiss)
                    {
                        break;
                    }
                }
                legaleZuege.Add(new Zug(this, row - i, col + i));
            }
            //Der Laeufer kann nach oben links ziehen

            for (int i = 1; row - i >= 0 && col - i >= 0; i++)
            {
                if (Schachfeld[row - i, col - i].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row - i, col - i].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row - i, col - i));
                        break;
                    }


                    if (Schachfeld[row - i, col - i].Controls.OfType<Figur>().FirstOrDefault().weiss == weiss)
                    {
                        break;
                    }
                }
                legaleZuege.Add(new Zug(this, row - i, col - i));
            }

            return legaleZuege;
        }
    }
}

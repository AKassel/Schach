using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    internal class Laeufer : Figur
    {
        //public Laeufer original;
        public Laeufer(bool weiss, int row, int colomn, Image image) : base(weiss, row, colomn, image)
        {
            original = this;
        }
        public override Figur klonen()
        {
            Laeufer Laeufer = new Laeufer(weiss, row, col, Image);
            Laeufer.original = original;
            return Laeufer;
        }
        override public List<Zug> LegaleZuege(Panel[,] Schachfeld)
        {
            List<Zug> legaleZuege = new List<Zug>();
            //Der Laeufer kann nach unten rechts ziehen

            for (int i = 1; i + row < 8 && i + col < 8;i++)
            {
                if (Schachfeld[row + i, col+i].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row + i, col+i].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row + i, col+i));
                        break;
                    }


                    if (Schachfeld[row + i, col+i].Controls.OfType<Figur>().FirstOrDefault().weiss == weiss)
                    {
                        break;
                    }
                }
                legaleZuege.Add(new Zug(this, row + i, col + i));
            }
            //Der Laeufer kann nach unten links ziehen

            for (int i = 1; i + row < 8 &&  col - i >= 0; i++)
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

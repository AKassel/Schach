using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    internal class Springer : Figur
    {
       // public Springer original;
        public Springer(bool weiss, int row, int colomn, Image image) : base(weiss, row, colomn, image)
        {
            original = this;
            Wert = 3;
        }
        public override Figur klonen()
        {
            Springer springer = new Springer(weiss, row, col, Image);
            springer.original = this.original;
            
            return springer;
        }
        override public List<Zug> LegaleZuege(Panel[,] Schachfeld)
        {
            List<Zug> legaleZuege = new List<Zug>();
            //2 nach unten 1 nach rechts
            if (row + 2 < 8 && col+1<8)
            {
                if (Schachfeld[row + 2, col + 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row + 2, col + 1].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row + 2, col + 1));
                    }
                }
                else
                {
                    legaleZuege.Add(new Zug(this,row + 2, col + 1));
                }
                   
            }
            //2 nach unten 1 nach links
            if (row + 2 < 8 && col - 1 >=0)
            {
                if (Schachfeld[row + 2, col - 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row + 2, col - 1].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this,row + 2, col - 1));
                    }
                    
                }
                else
                {
                    legaleZuege.Add(new Zug(this, row + 2, col - 1));
                }

            }

            //2 nach oben 1 nach rechts
            if (row - 2 >= 0 && col + 1 < 8)
            {
                if (Schachfeld[row - 2, col + 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row - 2, col + 1].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row - 2, col + 1));
                    }
                    
                }
                else
                {
                    legaleZuege.Add(new Zug(this, row - 2, col + 1));
                }
            }
            //2 nach oben 1 nach links
            if (row - 2 >=0 && col - 1 >= 0)
            {
                if (Schachfeld[row - 2, col - 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row - 2, col - 1].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row - 2, col - 1));
                    }
                }
                else
                {
                    legaleZuege.Add(new Zug(this, row - 2, col - 1));
                }
            }


            //2 nach rechts 1 nach unten
            if (row + 1 < 8 && col + 2 < 8)
            {
                if (Schachfeld[row + 1, col + 2].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row + 1, col + 2].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row + 1, col + 2));
                    }
                    
                }
                else
                {
                    legaleZuege.Add(new Zug(this, row + 1, col + 2));
                }
            }
            //2 nach links 1 nach unten
            if (row + 1 < 8 && col - 2 >= 0)
            {
                if (Schachfeld[row + 1, col - 2].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row + 1, col - 2].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row + 1, col - 2));
                    }
                    
                }
                else
                {
                    legaleZuege.Add(new Zug(this, row + 1, col - 2));
                }
            }
            //2 nach rechts 1 nach oben
            if (row - 1 >= 0 && col + 2 < 8)
            {
                if (Schachfeld[row - 1, col + 2].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row - 1, col + 2].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row - 1, col + 2));
                    }
                    
                }
                else
                {
                    legaleZuege.Add(new Zug(this, row - 1, col + 2));
                }
            }
            //2 nach links 1 nach oben
            if (row - 1 >= 0 && col - 2 >= 0)
            {
                if (Schachfeld[row - 1, col - 2].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                    if (Schachfeld[row - 1, col - 2].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row - 1, col - 2));
                    }
                    
                }
                else {
                    legaleZuege.Add(new Zug(this, row - 1, col - 2));
                }
            }



            return legaleZuege;
        }
      }
}

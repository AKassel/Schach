using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    internal class Koenig : Figur
    {
        public bool Bewegt = false;
        public Turm rochadeTurm;
        public Turm langerrochadeTurm;
        public Panel rochadepanel;
        public Panel langesRochadepanel;

        //public Koenig original;
        public Koenig(bool weiss, int row, int colomn, Image image) : base(weiss, row, colomn, image)
        {
            original = this;
            Wert = 100;
        }
        public override Figur klonen()
        {
           Koenig koenig = new Koenig(weiss, row, col, Image);
            koenig.original = original;
            /*
            koenig.rochadeTurm = rochadeTurm;
            koenig.langerrochadeTurm=langerrochadeTurm;
            koenig.rochadepanel = rochadepanel;
            koenig.langesRochadepanel = langesRochadepanel;
            */
            koenig.Bewegt = Bewegt;
            return koenig;
        }
        override public List<Zug> LegaleZuege(Panel[,] Schachfeld)
        {
            List<Zug> legaleZuege = new List<Zug>();

            //kurze Rochade fuer weiss
            if (weiss && !Bewegt)
            {
                if (Schachfeld[7, 5].Controls.OfType<Figur>().FirstOrDefault() == null &&
                    Schachfeld[7, 6].Controls.OfType<Figur>().FirstOrDefault() == null &&
                    Schachfeld[7, 7].Controls.OfType<Figur>().FirstOrDefault() is Turm)
                {
                    Turm turm = Schachfeld[7, 7].Controls.OfType<Turm>().FirstOrDefault();
                    if ((!turm.Bewegt) && turm.weiss)
                    {
                        Zug zug = new Zug(this, 7, 6);
                        zug.Rochade = true;
                        legaleZuege.Add(zug);
                        rochadepanel = Schachfeld[7, 6];
                        rochadeTurm = turm;
                    }

                }
                //lange Rochade fuer weiss
                if (Schachfeld[7, 3].Controls.OfType<Figur>().FirstOrDefault() == null &&
                    Schachfeld[7, 2].Controls.OfType<Figur>().FirstOrDefault() == null &&
                    Schachfeld[7, 1].Controls.OfType<Figur>().FirstOrDefault() == null &&
                    Schachfeld[7, 0].Controls.OfType<Figur>().FirstOrDefault() is Turm)
                {
                    Turm turm = Schachfeld[7, 0].Controls.OfType<Turm>().FirstOrDefault();
                    if ((!turm.Bewegt) && turm.weiss)
                    {
                        Zug zug = new Zug(this, 7, 2);
                        zug.Rochade = true;
                        legaleZuege.Add(zug);
                        langesRochadepanel = Schachfeld[7, 2];
                        langerrochadeTurm = turm;
                    }
                }
            }

            //kurze Rochade fuer schwarz
            if (!weiss && !Bewegt)
            {
                if (Schachfeld[0, 5].Controls.OfType<Figur>().FirstOrDefault() == null &&
                    Schachfeld[0, 6].Controls.OfType<Figur>().FirstOrDefault() == null &&
                    Schachfeld[0, 7].Controls.OfType<Figur>().FirstOrDefault() is Turm)
                {
                    Turm turm = Schachfeld[0, 7].Controls.OfType<Turm>().FirstOrDefault();
                    if ((!turm.Bewegt) && !turm.weiss)
                    {
                        Zug zug = new Zug(this, 0, 6);
                        zug.Rochade = true;
                        legaleZuege.Add(zug);
                        rochadepanel = Schachfeld[0, 6];
                        rochadeTurm = turm;
                    }

                }
                //lange Rochade fuer schwarz
                if (Schachfeld[0, 3].Controls.OfType<Figur>().FirstOrDefault() == null &&
                    Schachfeld[0, 2].Controls.OfType<Figur>().FirstOrDefault() == null &&
                    Schachfeld[0, 1].Controls.OfType<Figur>().FirstOrDefault() == null &&
                    Schachfeld[0, 0].Controls.OfType<Figur>().FirstOrDefault() is Turm)
                {
                    Turm turm = Schachfeld[0, 0].Controls.OfType<Turm>().FirstOrDefault();
                    if ((!turm.Bewegt) && !turm.weiss)
                    {
                        Zug zug = new Zug(this, 0, 2);
                        zug.Rochade = true;
                        legaleZuege.Add(zug);
                        langesRochadepanel = Schachfeld[0, 2];
                        langerrochadeTurm = turm;
                    }
                }
            }

            //Der Koenig kann auf die drei Felder über ihm ziehen
            for (int i = 0; i < 3; i++)
            {
                if (row - 1 >= 0 && col - 1 + i >= 0 && col - 1 + i < 8)
                {
                    if (Schachfeld[row - 1, col - 1 + i].Controls.OfType<Figur>().FirstOrDefault() != null)
                    {
                       
                        if (Schachfeld[row - 1, col - 1 + i].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                        {
                            legaleZuege.Add(new Zug(this, row - 1, col - 1 + i));
                        }

                    }
                    else
                    {
                        legaleZuege.Add(new Zug(this, row - 1, col - 1 + i));
                    }
                }
            }
            //Der Koenig kann auf die drei Felder unter ihm ziehen
            for (int i = 0; i < 3; i++)
            {
                if (row + 1 < 8 && col + 1 - i >= 0 && col + 1 - i < 8)
                {
                    if (Schachfeld[row + 1, col + 1 - i].Controls.OfType<Figur>().FirstOrDefault() != null)
                    {
                       
                        if (Schachfeld[row + 1, col + 1 - i].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                        {
                            legaleZuege.Add(new Zug(this, row + 1, col + 1 - i));
                        }

                    }
                    else
                    {
                        legaleZuege.Add(new Zug(this, row + 1, col + 1 - i));
                    }
                }
            }
            //Der Koenig kann nach links ziehen
            if (col - 1 >= 0)
            {
                if (Schachfeld[row, col - 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                   
                    if (Schachfeld[row, col - 1].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row, col - 1));
                    }

                }
                else
                {
                    legaleZuege.Add(new Zug(this, row, col - 1));
                }
            }
            //Der Koenig kann nach rechts ziehen
                if (col +1<8)
                {
                if (Schachfeld[row, col + 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                {
                   
                    if (Schachfeld[row, col + 1].Controls.OfType<Figur>().FirstOrDefault().weiss != weiss)
                    {
                        legaleZuege.Add(new Zug(this, row, col + 1));
                    }

                }
                else
                {
                    legaleZuege.Add(new Zug(this, row, col + 1));
                }
            }

            return legaleZuege;
        }
    }
}

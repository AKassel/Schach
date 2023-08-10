namespace Schach
{
    internal class Bauer : Figur
    {
        public bool enpassant = false;
        public bool bewegt = false;
        public Panel enpassantpanel;
        public Panel enpassantSchlagenPanel;
        public Bauer passant;
        //public Bauer original;

        public Bauer(bool weiss, int row, int colomn, Image image) : base(weiss, row, colomn, image)
        {
        }
        public override Figur klonen()
        {
            Bauer bauer = new Bauer(weiss, row, col, Image);
           
            bauer.enpassant = enpassant;
            /*
            bauer.enpassantpanel = enpassantpanel;
            bauer.enpassantSchlagenPanel = enpassantSchlagenPanel;
            bauer.passant = passant;
            */
            bauer.original = this.original;
            return bauer;
        }
        public bool Befoerdern()
        {
            //Zuege der Dame zurueckgeben wenn der Bauer am durchs Feld gelaufen ist und zur Dame wird
            if (weiss && row == 0)
            {
                return true;
            }
            if (!weiss && row == 7)
            {
                return true;
            }
            return false;
        }

        override public List<Zug> LegaleZuege(Panel[,] Schachfeld)
        {
            List<Zug> legaleZuege = new List<Zug>();
            
            //Schwarze Bauern auf reihe 8-1=7, also die, die sich noch nicht bewegt haben, können wenn auf den 2 Feldern vor ihnen keine andere
            //Figur steht, auf beide Felder gehen
            if (row == 1 && !weiss &&
                Schachfeld[row+2, col].Controls.OfType<Figur>().FirstOrDefault() == null &&
                    Schachfeld[row+1, col].Controls.OfType<Figur>().FirstOrDefault() == null)
            {
                    legaleZuege.Add(new Zug(this, row + 2, col));
                enpassantpanel = Schachfeld[row + 2, col];
            }
            if (row + 1 < 8)
            {
                if (!weiss && Schachfeld[row + 1, col].Controls.OfType<Figur>().FirstOrDefault() == null)
                {
                    legaleZuege.Add(new Zug(this, row + 1, col));
                }
            }
            //Schwarze Bauern können diagonal schlagen
            if (!weiss && row < 7)
            {
                if (col < 7)
                {
                    if (Schachfeld[row + 1, col + 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                    {
                        if (Schachfeld[row + 1, col + 1].Controls.OfType<Figur>().FirstOrDefault().weiss)
                        {
                            legaleZuege.Add(new Zug(this, row + 1, col + 1));
                        }
                    }
                    //enpassant nach rechts
                    if (Schachfeld[row, col + 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                    {
                        if (Schachfeld[row, col + 1].Controls.OfType<Figur>().FirstOrDefault() is Bauer)
                        {
                            if (Schachfeld[row, col + 1].Controls.OfType<Bauer>().FirstOrDefault().enpassant&& Schachfeld[row, col + 1].Controls.OfType<Bauer>().FirstOrDefault().weiss)
                            {
                                legaleZuege.Add(new Zug(this, row + 1, col + 1));
                                enpassantSchlagenPanel = Schachfeld[row + 1, col + 1];
                                passant = Schachfeld[row, col + 1].Controls.OfType<Bauer>().FirstOrDefault();
                            }
                        }
                    }
                }
                if (col > 0)
                {
                    if (Schachfeld[row + 1, col - 1].Controls.OfType<Figur>().FirstOrDefault() != null )
                    {
                        if (!weiss && Schachfeld[row + 1, col - 1].Controls.OfType<Figur>().FirstOrDefault().weiss)
                        {
                            legaleZuege.Add(new Zug(this, row + 1, col - 1));
                        }
                    }
                    //enpassent nach links
                    if(Schachfeld[row, col - 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                    {
                        if(Schachfeld[row, col - 1].Controls.OfType<Figur>().FirstOrDefault() is Bauer)
                        {
                            if(Schachfeld[row, col - 1].Controls.OfType<Bauer>().FirstOrDefault().enpassant && Schachfeld[row, col - 1].Controls.OfType<Bauer>().FirstOrDefault().weiss)
                            {
                                legaleZuege.Add(new Zug(this, row + 1, col - 1));
                                enpassantSchlagenPanel = Schachfeld[row + 1, col - 1];
                                passant = Schachfeld[row, col - 1].Controls.OfType<Bauer>().FirstOrDefault();
                            }
                        }
                    }
                }
            }

            //Weisse Bauern auf Reihe 8-6=2, können genauso wie die schwarzen Bauern 2 Felder nach vorne
            if (row > 0 && weiss)
            {
                if (row == 6 && weiss && Schachfeld[row-2, col].Controls.OfType<Figur>().FirstOrDefault() == null
                && Schachfeld[row-1, col].Controls.OfType<Figur>().FirstOrDefault() == null)
            {
                legaleZuege.Add(new Zug(this, row - 2, col));
                enpassantpanel = Schachfeld[row - 2, col];
            }
            //Oder auch nur eins nach vorne
            if (weiss && Schachfeld[row-1, col].Controls.OfType<Figur>().FirstOrDefault() == null)
            {
                legaleZuege.Add(new Zug(this, row - 1, col));
            }
            //Weisse Bauern können die Figuren mit der anderen Farbe diagonal Schlagen
            
                if (col > 0&&weiss)
                {
                    if (Schachfeld[row - 1, col - 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                    {
                        if (weiss && !Schachfeld[row - 1, col - 1].Controls.OfType<Figur>().FirstOrDefault().weiss)
                        {
                            legaleZuege.Add(new Zug(this, row - 1, col - 1));
                        }
                    }
                    //enpassant nach links
                    if (Schachfeld[row, col - 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                    {
                        if (Schachfeld[row, col - 1].Controls.OfType<Figur>().FirstOrDefault() is Bauer)
                        {
                            if (Schachfeld[row, col - 1].Controls.OfType<Bauer>().FirstOrDefault().enpassant&& !Schachfeld[row, col - 1].Controls.OfType<Bauer>().FirstOrDefault().weiss)
                            {
                                legaleZuege.Add(new Zug(this, row - 1, col - 1));
                                enpassantSchlagenPanel = Schachfeld[row - 1, col - 1];
                                passant = Schachfeld[row, col - 1].Controls.OfType<Bauer>().FirstOrDefault();
                            }
                        }
                    }
                }
                if (col < 7 && weiss)
                {
                    if (Schachfeld[row - 1, col + 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                    {
                        if (weiss && !Schachfeld[row - 1, col + 1].Controls.OfType<Figur>().FirstOrDefault().weiss)
                        {
                            legaleZuege.Add(new Zug(this, row - 1, col + 1));
                        }
                    }
                    // enpassent nach rechts
                    if (Schachfeld[row, col + 1].Controls.OfType<Figur>().FirstOrDefault() != null)
                    {
                        if (Schachfeld[row, col + 1].Controls.OfType<Figur>().FirstOrDefault() is Bauer)
                        {
                            if (Schachfeld[row, col + 1].Controls.OfType<Bauer>().FirstOrDefault().enpassant&& !Schachfeld[row, col + 1].Controls.OfType<Bauer>().FirstOrDefault().weiss)
                            {
                                legaleZuege.Add(new Zug(this, row - 1, col + 1));
                                enpassantSchlagenPanel = Schachfeld[row - 1, col + 1];
                                passant = Schachfeld[row, col + 1].Controls.OfType<Bauer>().FirstOrDefault();
                            }
                        }
                    }
                }
            }
            return legaleZuege;
        }

    }
}

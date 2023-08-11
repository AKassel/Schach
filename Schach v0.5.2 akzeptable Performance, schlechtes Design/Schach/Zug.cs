using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    internal class Zug
    {
        //die Figur die gezogen wird
        public Figur figur;
        //Die Ziel Koordinate Im Schachbrett
        public int row;
        public int col;
        public int bewertung;
        public List<Zug> folgeZuege = new List<Zug>();
        public Figur befoerdert;
        public Figur geschlageneFigur;
        public bool FigurWurdeSchonVorherGezogen;
        public bool Rochade = false;
        public Zug(Figur figur, int row, int col)
        {

            this.figur = figur;
            this.row = row;
            this.col = col;
            if (figur is Koenig koenig)
            {
                FigurWurdeSchonVorherGezogen = koenig.Bewegt;
            }
            if (figur is Turm turm)
            {
                FigurWurdeSchonVorherGezogen = turm.Bewegt;
            }
        }
        public Zug(Figur figur, int row, int col, Figur befoerdet)
        {
            this.befoerdert = befoerdet;
            this.figur = figur;
            this.row = row;
            this.col = col;
        }
        public int Bewertung()
        {
            if (folgeZuege.Count == 0)
            {
                return bewertung;
            }

            if (figur.weiss)
            {
                int maxBewertung = folgeZuege.Min(z => z.Bewertung());
                bewertung = maxBewertung;
                return maxBewertung;
            }
            else
            {
                int minBewertung = folgeZuege.Max(z => z.Bewertung());
                bewertung = minBewertung;
                return minBewertung;
            }

        }

        //gute Stellung fuer weiss ist positiv
        public int FigurenZaehlen(Schachfeld Schachfeld1)
        {
            Koenig SchwarzerKoenig = null;
            Koenig WeisserKoenig = null;
            List<Zug> Zuege = new List<Zug>();
            int AnzahlZuege = 0;
            List<Zug> WZuege = new List<Zug>();
            int AnzahlWZuege = 0;
            List<Figur> WeisseFiguren = new List<Figur>();
            List<Figur> SchwarzeFiguren = new List<Figur>();   
            WeisseFiguren.AddRange(Schachfeld1.WeisseFiguren);
            SchwarzeFiguren.AddRange(Schachfeld1.SchwarzeFiguren);

            //Den Zug, bzw. die Stellung nach dem Zug, bewerten
            foreach (Figur figur in SchwarzeFiguren)
            {
                Zuege = figur.ZuegetrotzSchachVerbieten(Schachfeld1);
                AnzahlZuege += Zuege.Count;
                if (figur is Bauer bauer)
                {
                    bewertung -= 1;
                }
                if (figur is Dame dame)
                {
                    bewertung -= 9;
                }
                if (figur is Laeufer laeufer)
                {
                    bewertung -= 3;
                }
                if (figur is Springer springer)
                {
                    bewertung -= 3;
                }
                if (figur is Turm turm)
                {
                    bewertung -= 5;
                }

                if (figur is Koenig koenig)
                {
                    SchwarzerKoenig = koenig;
                }

            }
           
            if (AnzahlZuege == 0)
            {
                if (SchwarzerKoenig.KoenigStehtSchach(Schachfeld1))
                {
                    return bewertung += 100;
                }
                else
                {
                    return 0;
                }
            }
            //Wenn weisse Figuren auf dem Brett sind, ist die Position gut fuer weiss also Positiv
            foreach (Figur figur in WeisseFiguren)
            {
                WZuege = figur.ZuegetrotzSchachVerbieten(Schachfeld1);
                AnzahlWZuege += WZuege.Count;

                if (figur is Bauer bauer)
                {
                    bewertung += 1;
                }
                if (figur is Dame dame)
                {
                    bewertung += 9;
                }
                if (figur is Laeufer laeufer)
                {
                    bewertung += 3;
                }
                if (figur is Springer springer)
                {
                    bewertung += 3;
                }
                if (figur is Turm turm)
                {
                    bewertung += 5;
                }

                if (figur is Koenig koenig)
                {
                    WeisserKoenig = koenig;
                }

            }
           
            if (AnzahlZuege == 0)
            {
                if (WeisserKoenig.KoenigStehtSchach(Schachfeld1))
                {
                    return bewertung -= 100;
                }
                else
                {
                    return 0;
                }
            }
            return bewertung;
        }


    }
}


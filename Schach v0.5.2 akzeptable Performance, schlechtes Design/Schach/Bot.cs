using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;

namespace Schach
{
    internal class Bot
    {
        public bool weiss;
        public Schachfeld schachfeld;
        public bool Test = false;

        // Bilder werden gebraucht um Bauern zu befoerdern

        Image weisseDame;
        Image weisserLaeufer;
        Image weisserSpringer;
        Image weisserTurm;

        Image schwarzeDame;
        Image schwarzerLaeufer;
        Image schwarzerSpringer;
        Image schwarzerTurm;
        public Bot(bool weiss, Schachfeld schachfeld)
        {
            this.weiss = weiss;
            this.schachfeld = schachfeld;
            weisseDame = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\Dame.png");
            weisserLaeufer = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\Laeufer.png");
            weisserSpringer = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\Springer.png");
            weisserTurm = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\Turm.png");

            schwarzeDame = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\SDame.png");
            schwarzerLaeufer = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\SLaeufer.png");
            schwarzerSpringer = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\SSpringer.png");
            schwarzerTurm = Image.FromFile(@"D:\Arne\Schach Programm\Bilder\STurm.png");

        }
        public Bot()
        {

        }
        public void Ziehen()
        {
            if (Test)
            {
                weiss = !weiss;
                Schachfeld schachfeldKopie = new Schachfeld(schachfeld);
                Stopwatch stopwatch = Stopwatch.StartNew();
                int Test = MoeglicheZuegeTest(1, schachfeldKopie);
                stopwatch.Stop();
                Debug.WriteLine(stopwatch.ElapsedMilliseconds + "ms R1: " +Test);
                
                Schachfeld schachfeldKopie1 = new Schachfeld(schachfeld);
                Stopwatch stopwatch1 = Stopwatch.StartNew();
                int Test1 = MoeglicheZuegeTest(2, schachfeldKopie1);
                stopwatch1.Stop();
                Debug.WriteLine(stopwatch1.ElapsedMilliseconds + "ms R2: " + Test1);

                Schachfeld schachfeldKopie2 = new Schachfeld(schachfeld);
                Stopwatch stopwatch2 = Stopwatch.StartNew();
                int Test2 = MoeglicheZuegeTest(3, schachfeldKopie2);
                stopwatch2.Stop();
                Debug.WriteLine(stopwatch2.ElapsedMilliseconds + "ms R3: " + Test2);

                Schachfeld schachfeldKopie3 = new Schachfeld(schachfeld);
                Stopwatch stopwatch3 = Stopwatch.StartNew();
                int Test3 = MoeglicheZuegeTest(4, schachfeldKopie3);
                stopwatch3.Stop();
                Debug.WriteLine(stopwatch3.ElapsedMilliseconds + "ms R4: " + Test3);


                Stopwatch stopwatch32 = Stopwatch.StartNew();
                int Test32 = MoeglicheZuegeTest(4);
                stopwatch32.Stop();
                Debug.WriteLine(stopwatch32.ElapsedMilliseconds + "ms R4 single Threaded: " + Test32);


                Schachfeld schachfeldKopie4 = new Schachfeld(schachfeld);
                Stopwatch stopwatch4 = Stopwatch.StartNew();
                int Test4 = MoeglicheZuegeTest(5, schachfeldKopie4);
                stopwatch4.Stop();
                Debug.WriteLine(stopwatch4.ElapsedMilliseconds + "ms R5: " + Test4);

            }
            else
            {
                BessererZug(schachfeld,3);
            }
           //SchlechterZug(schachfeld);
           //ZufallsZug(schachfeld);



        }
        public int MoeglicheZuegeTest(int Tiefe, Schachfeld schachfeldOrg)
        {
            List<Zug> zuege = MoeglicheZuegeFuerWeissOderSchwarzSuchen(schachfeldOrg);

            if (Tiefe == 1)
            {
                return zuege.Count;
            }

            int Positionen = 0;

            object lockObject = new object(); // Objekt zum Sperren der inkrementellen Operation
            if (Tiefe > 2)
            {
                Parallel.ForEach(zuege, zug =>
                {
                    int AltRow = zug.figur.row;
                    int AltCol = zug.figur.col;

                    Schachfeld Kopie = new Schachfeld(schachfeldOrg);
                    zug.figur = Kopie.figurBei(zug.figur.row, zug.figur.col);

                    ZugMachen(zug, Kopie);

                    int positionsInThisThread = MoeglicheZuegeTest(Tiefe - 1, new Schachfeld(Kopie));

                    ZugRueckgaengigMachen(zug, Kopie, AltRow, AltCol);

                    // Inkrementiere Positionen unter Verwendung von Interlocked.Increment
                    lock (lockObject)
                    {
                        Positionen += positionsInThisThread;
                    }
                });
            }
            else
            {
                foreach(Zug zug in zuege)
                {
                    int AltRow = zug.figur.row;
                    int AltCol = zug.figur.col;
                    ZugMachen(zug, schachfeldOrg);

                     Positionen += MoeglicheZuegeTest(Tiefe - 1, schachfeldOrg);

                    ZugRueckgaengigMachen(zug, schachfeldOrg, AltRow, AltCol);
                }
            }
            return Positionen;
        }
        public int MoeglicheZuegeTest(int Tiefe)
        {
            //Schelchte Performance
            List<Zug> zuege = MoeglicheZuegeFuerWeissOderSchwarzSuchen(schachfeld);

            if (Tiefe == 1)
            {
                return zuege.Count;
            }
            int Positionen = 0;

            foreach (Zug zug in zuege)
            {
                int AltRow = zug.figur.row;
                int AltCol = zug.figur.col;

                ZugMachen(zug, schachfeld);
                Positionen += MoeglicheZuegeTest(Tiefe - 1);
                ZugRueckgaengigMachen(zug, schachfeld, AltRow, AltCol);
            }
            return Positionen;
        }
        
        public List<Zug> FolgeZuegeHinzufuegen(int Tiefe, List<Zug> zuege, Schachfeld schachfeld1)
        {
            if (Tiefe == 1)
            {
                foreach (Zug zug in zuege)
                {
                    int AltRow = zug.figur.row;
                    int AltCol = zug.figur.col;

                    ZugMachen(zug, schachfeld1);

                    //int zugbewertung = schachfeld1.bewertung;
                    int gezaehlt = schachfeld1.bewertung;//zug.FigurenZaehlen(schachfeld1);
                    zug.bewertung = gezaehlt;

                    ZugRueckgaengigMachen(zug, schachfeld1, AltRow, AltCol);


                }
                return zuege;
            }
            object lockobject = new object();
            if (false)
            {
                Parallel.ForEach(zuege, zug =>
                {
                    int AltRow = zug.figur.row;
                    int AltCol = zug.figur.col;
                    Schachfeld Kopie;
                    lock (schachfeld1)
                    {
                        Kopie = new Schachfeld(schachfeld1);

                        zug.figur = Kopie.figurBei(zug.figur.row, zug.figur.col);
                    }
                    ZugMachen(zug, Kopie);
                    //Schachfeld Kopie2 = new Schachfeld(Kopie);
                    ZugRueckgaengigMachen(zug, Kopie, AltRow, AltCol);

                    zug.folgeZuege = FolgeZuegeHinzufuegen(Tiefe - 1, MoeglicheZuegeFuerWeissOderSchwarzSuchen(Kopie), Kopie);

                    
                    
                });
            }
            else
            {
                foreach (Zug zug in zuege)
                {
                    int AltRow = zug.figur.row;
                    int AltCol = zug.figur.col;

                    ZugMachen(zug, schachfeld1);
                    zug.folgeZuege = FolgeZuegeHinzufuegen(Tiefe - 1, MoeglicheZuegeFuerWeissOderSchwarzSuchen(schachfeld1), schachfeld1);
                    ZugRueckgaengigMachen(zug, schachfeld1, AltRow, AltCol);
                    if (zug.folgeZuege.Count == 0)
                    {
                        if (zug.figur.KoenigStehtSchach(schachfeld1))
                        {
                            if (zug.figur.weiss)
                            {
                                zug.bewertung -= 100;
                            }
                            else
                            {
                                zug.bewertung += 100;
                            }
                        }
                        else
                        {
                            zug.bewertung = 0;
                        }
                    }
                }
               
            }
            return zuege;
        }
        public void BessererZug(Schachfeld Schachfeld1, int rekursion)
        {
            //Durch die Deep Coypy verschwinden die Figuren nicht mehr und Ziehen wird durch ZugMachen auch nicht rekursiv aufgerufen
            Schachfeld Schachfeld = new Schachfeld(Schachfeld1);

            List<Zug> Mz = MoeglicheZuegeFuerWeissOderSchwarzSuchen(Schachfeld);
            //Debug.WriteLine("A: " + ZuegeAufDemErstenFeld.Count);
            int z = 0;
            FolgeZuegeHinzufuegen(rekursion, Mz, Schachfeld);
            //Hier werden die gemachten Zuege gezaehlt um zu pruefen ob ob der Bot nur legale Zuege macht
            foreach (Zug zug in Mz)
            {
                if (zug.folgeZuege.Count != 0)
                {
                    foreach (Zug folgezug in zug.folgeZuege)
                    {

                        if (folgezug.folgeZuege.Count != 0)
                        {
                            foreach (Zug folgefolgezug in folgezug.folgeZuege)
                            {
                                if (folgefolgezug.folgeZuege.Count != 0)
                                {

                                    foreach (Zug vierterrekursionszug in folgefolgezug.folgeZuege)
                                    {
                                        if (vierterrekursionszug.folgeZuege.Count != 0)
                                        {
                                            foreach (Zug fuenfterrekursionszug in vierterrekursionszug.folgeZuege) {

                                                z++;
                                            }
                                        }
                                        else
                                        {
                                            z++;
                                        }
                                    }
                                }
                                else
                                {
                                    z++;
                                }
                            }
                        }
                        else
                        {
                            z++;
                        }
                    }
                }
                else
                {
                    z++;
                }
            }
            Debug.WriteLine("Bewertete Züge: " + z);

            //Wenn alle Züge die gleicher Bewertung haben wird ein Zufälliger Zug gemacht
            Random random = new Random();
            Zug ZufallsZug = null;
            if (Mz.Count > 0)
            {
                int randomZug = random.Next(0, Mz.Count);
                ZufallsZug = Mz[randomZug];
                ZufallsZug.figur = ZufallsZug.figur.original;
                // Verarbeite den ZufallsZug
            }
            else
            {
                // Zeige eine Benachrichtigung an, dass der Spieler gewonnen hat
                MessageBox.Show("Herzlichen Glückwunsch! Du hast gewonnen!", "Spielende");
                return;
                // Hier kannst du zusätzlichen Code hinzufügen, um das Spiel zurückzusetzen oder zu beenden
            }
            Zug besterZug;

            foreach (Zug zug in Mz)
            {
                 zug.bewertung = zug.Bewertung();
            }
            if (!weiss)
            {
                besterZug = Mz.FirstOrDefault(z => z.bewertung == Mz.Min(x => x.bewertung));

                if (besterZug.bewertung == ZufallsZug.bewertung)
                {
                    besterZug = ZufallsZug;
                }
                besterZug.figur = besterZug.figur.original;
                ZugMachen(besterZug, Schachfeld1);
            }
            if (weiss)
            {
                besterZug = Mz.FirstOrDefault(z => z.bewertung == Mz.Max(x => x.bewertung));

                if (besterZug.bewertung == ZufallsZug.bewertung)
                {
                    besterZug = ZufallsZug;
                }
                besterZug.figur = besterZug.figur.original;
                ZugMachen(besterZug, Schachfeld1);
            }
            
                
            
            
        }

        public async void ZugMachen(Zug zug, Schachfeld spielfeld)
        {
            Panel[,] Schachfeld = spielfeld.schachfeld;

            //Methoden die Zuege nur pruefen sollten hier ein Deep Copy Schachfeld uebergeben, da die Figuren sonst kurz vom Spielfeld verschwinden, auch wenn sie man später wieder zurücksetzt
            int row = zug.row;
            int col = zug.col;
            Figur figur = zug.figur;
            Panel targetPanel = Schachfeld[row, col];

            //Rochade scheiss Anfang
            //Beim ersten Koenigszug oder Turmzug wird bewegt auf true gesetzt, solange bewegt auf false ist, kann LegaleZuege()
            //Die Rochade als Legal zurueck geben

            if (figur is Koenig koenig)
            {

                if (!koenig.Bewegt)
                {
                    zug.FigurWurdeSchonVorherGezogen = false;
                    //Wenn noch keine Rochade gemacht wurde
                    if (koenig.rochadepanel != null || koenig.langesRochadepanel != null)
                    {
                        //rochadepanel kann nur == targetpanel sein, wenn bewegt vorher false war und der Koenig 
                        //prueft ob der Turm sich bewegt hat

                        if (targetPanel == koenig.rochadepanel)
                        {
                            //kurze Rochade
                            koenig.rochadeTurm.col = koenig.col + 1;
                            koenig.rochadeTurm.row = koenig.row;
                            Schachfeld[koenig.rochadeTurm.row, koenig.rochadeTurm.col].Controls.Add(koenig.rochadeTurm);
                            zug.Rochade = true;
                        }
                        else if (targetPanel == koenig.langesRochadepanel)
                        {
                            //lange Rochade
                            koenig.langerrochadeTurm.col = koenig.col - 1;
                            koenig.langerrochadeTurm.row = koenig.row;
                            Schachfeld[koenig.langerrochadeTurm.row, koenig.langerrochadeTurm.col].Controls.Add(koenig.langerrochadeTurm);
                            zug.Rochade = true;
                        }
                    }
                }
                koenig.Bewegt = true;
            }
            if (figur is Turm turm)
            {
                if (!turm.Bewegt) {
                    zug.FigurWurdeSchonVorherGezogen = false;
                }
                turm.Bewegt = true;
            }
            //Rochade scheiss Ende

            //Enpassant scheiss Anfang

            //spielfeld.vorherigerEnpassantBauer = spielfeld.enpassantBauer;
            if (spielfeld.enpassantBauer != null)
            {
                if (spielfeld.enpassantBauer.weiss == spielfeld.weissAmZug)
                {
                    spielfeld.enpassantBauer.enpassant = false;
                    spielfeld.enpassantBauer = null;
                }
                else
                {
                    spielfeld.enpassantBauer.enpassant = true;
                }
            }
            if (figur is Bauer bauer)
            {
                if (!bauer.bewegt)
                {
                    zug.FigurWurdeSchonVorherGezogen = false;
                }
                else
                {
                    zug.FigurWurdeSchonVorherGezogen = true;
                }
                if (targetPanel == bauer.enpassantpanel && !bauer.bewegt)
                {
                    spielfeld.enpassantBauer = bauer;
                    bauer.enpassant = true;
                }
                else
                {
                    bauer.enpassant = false;
                }
                bauer.bewegt = true;

                //RemoveFiugreFrom field Methode, aber das Panel auf dem die Figur geschlagen wird, ist das enpassantSchlagenPanel und nicht das wo hin gezogen wird
                //Nur wenn auf dem Panel auf das der Bauer zieht keine Figur steht, kann enpassant geschlagen werden
                if (bauer.enpassantSchlagenPanel == targetPanel && bauer.passant.enpassant)
                {
                    Panel panel = spielfeld.schachfeld[bauer.passant.row, bauer.passant.col];
                    if (panel.Controls.Count > 0 && panel.Controls[0] is Figur figur1)
                    {
                        // PictureBox vom Panel entfernen und freigeben
                        zug.geschlageneFigur = figur1;
                        if(zug.geschlageneFigur  == null)
                        {

                        }
                        if (figur1.weiss)
                        {
                            spielfeld.WeisseFiguren.Remove(figur1);
                            spielfeld.bewertung -= figur.Wert;
                        }
                        else
                        {
                            spielfeld.SchwarzeFiguren.Remove(figur1);
                            spielfeld.bewertung += figur.Wert;
                        }

                        panel.Controls.Remove(figur1);
                        //figur1.Dispose();

                    }
                }
            }

            //Enpassant scheiss Ende

            //Bauer befoerdern
            if (zug.befoerdert != null)
            {
                RemoveFigureFromField(zug, spielfeld);
                //Schachfeld[zug.row, zug.col].Controls.Add(zug.befoerdert);
                figur = zug.befoerdert;
                if(zug.befoerdert.weiss)
                {
                    spielfeld.WeisseFiguren.Add(zug.befoerdert);
                    spielfeld.bewertung += zug.befoerdert.Wert;
                }
                else
                {
                    spielfeld.WeisseFiguren.Add(zug.befoerdert);
                    spielfeld.bewertung -= zug.befoerdert.Wert;
                }
            }
            //Das eigentliche Zug machen

            //Entferne ggf. vorhandene Figuren auf dem Feld, die auf das gezogen werden soll
            if (Schachfeld[row, col].Controls.OfType<Figur>().FirstOrDefault() != null && Schachfeld[row, col].Controls.OfType<Figur>().FirstOrDefault() != figur)
            {
                //RemoveFigureFromField entfernt die Figuren auch aus den Listen und speichert die geschlageneFigur
                RemoveFigureFromField(zug, spielfeld);

            }
            //Das figur null ist, sollte eigentlich nur durch die Deep Copy Felder passieren und wuerde andere Fehler ausloesen
            if (figur != null)
            {
                figur.row = row;
                figur.col = col;
            }
            else
            {
                //Debug
            }
            if(Schachfeld[row, col].Controls.OfType<Figur>().FirstOrDefault() != null && Schachfeld[row, col].Controls.OfType<Figur>().FirstOrDefault() != figur)
            {
                //sollte eigentlich nicht passieren
            }
            spielfeld.schachfeld[row, col].Controls.Add(figur);

            spielfeld.weissAmZug = !spielfeld.weissAmZug;
            
        }

        void RemoveFigureFromField(Zug zug, Schachfeld schachfeld1)
        {
            Panel panel = schachfeld1.schachfeld[zug.row, zug.col];

            if (panel.Controls.Count > 0 && panel.Controls[0] is Figur figur1 && figur1 != zug.figur)
            {
                // PictureBox vom Panel entfernen und freigeben
                zug.geschlageneFigur = figur1;
                
                if (figur1.weiss)
                {
                    schachfeld1.WeisseFiguren.Remove(figur1);
                    schachfeld1.bewertung -= figur1.Wert;
                }
                else
                {
                    schachfeld1.SchwarzeFiguren.Remove(figur1);
                    schachfeld1.bewertung += figur1.Wert;
                }
                panel.Controls.Remove(figur1);
                //figur1.Dispose();

            }
        }

        public void ZugRueckgaengigMachen(Zug zug, Schachfeld Schachfeld, int AltRow, int AltCol)
        {
            //Zug wieder rückgängig machen
            zug.figur.row = AltRow;
            zug.figur.col = AltCol;
            if (Schachfeld.schachfeld[AltRow, AltCol].Controls.OfType<Figur>().FirstOrDefault() == null)
            {
                Schachfeld.schachfeld[AltRow, AltCol].Controls.Add(zug.figur);
            }
            else
            {
                //sollte eigentlich nicht passieren
            }
            //Rochade scheiss Anfang

            if (zug.figur is Koenig koenig)
            {

                koenig.Bewegt = zug.FigurWurdeSchonVorherGezogen;
                if (zug.Rochade)
                {
                    if (koenig.rochadepanel != null)
                    {
                        if (Schachfeld.schachfeld[zug.row, zug.col] == koenig.rochadepanel)
                        {
                            //kurze Rochade
                            if (koenig.weiss)
                            {
                                koenig.rochadeTurm.col = 7;
                                koenig.rochadeTurm.row = 7;
                            }
                            else
                            {
                                koenig.rochadeTurm.col = 7;
                                koenig.rochadeTurm.row = 0;
                            }
                            Schachfeld.schachfeld[koenig.rochadeTurm.row, koenig.rochadeTurm.col].Controls.Add(koenig.rochadeTurm);
                            koenig.rochadeTurm.Bewegt = false;
                        }
                    }
                    if (koenig.langesRochadepanel != null)
                    {
                        if (Schachfeld.schachfeld[zug.row, zug.col] == koenig.langesRochadepanel)
                        {
                            //lange Rochade
                            if (koenig.weiss)
                            {
                                koenig.langerrochadeTurm.col = 0;
                                koenig.langerrochadeTurm.row = 7;
                            }
                            else
                            {
                                koenig.langerrochadeTurm.col = 0;
                                koenig.langerrochadeTurm.row = 0;
                            }
                            Schachfeld.schachfeld[koenig.langerrochadeTurm.row, koenig.langerrochadeTurm.col].Controls.Add(koenig.langerrochadeTurm);
                            koenig.langerrochadeTurm.Bewegt = false;
                        }
                    }
                }
            }

            if (zug.figur is Turm turm)
            {
                turm.Bewegt = false;
            }
            //Rochade scheiss Ende

            //Enpassant scheiss Anfang
            
            if (zug.figur is Bauer bauer)
            {
                if(!zug.FigurWurdeSchonVorherGezogen) {
                    bauer.bewegt = false;
                }
            }

            /*
            if (Schachfeld.vorherigerEnpassantBauer != null)
                {

                Schachfeld.enpassantBauer = Schachfeld.vorherigerEnpassantBauer;
                Schachfeld.enpassantBauer.enpassant = true;
                }
          */

            //Enpassant scheiss Ende
            //Bauer befoerdern
            if (zug.befoerdert != null)
            {
                RemoveFigureFromField(zug, Schachfeld);
                Schachfeld.schachfeld[zug.row, zug.col].Controls.Add(zug.figur);
                if (zug.figur.weiss)
                {
                    Schachfeld.WeisseFiguren.Add(zug.figur);
                    Schachfeld.bewertung += zug.figur.Wert;
                }
                else
                {
                    Schachfeld.SchwarzeFiguren.Add(zug.figur);
                    Schachfeld.bewertung -= zug.figur.Wert;
                }
            }
                //geschlagene Figur wieder hinstellen
                if (zug.geschlageneFigur != null && zug.geschlageneFigur != zug.befoerdert)
                {
                    Schachfeld.schachfeld[zug.geschlageneFigur.row, zug.geschlageneFigur.col].Controls.Add(zug.geschlageneFigur);

                if (zug.geschlageneFigur.weiss)
                {
                    Schachfeld.WeisseFiguren.Add(zug.geschlageneFigur);
                    Schachfeld.bewertung += zug.geschlageneFigur.Wert;
                }
                else
                {
                    Schachfeld.SchwarzeFiguren.Add(zug.geschlageneFigur);
                    Schachfeld.bewertung -= zug.geschlageneFigur.Wert;
                }

                }
                //eigentlich ueberfluessig
                zug.geschlageneFigur = null;

                Schachfeld.weissAmZug = !Schachfeld.weissAmZug;
            
        }
        List<Zug> MoeglicheZuegeHinzufuegen(Figur figur, Schachfeld schachfeld1)
        {
            //HilfsMethode von Moegliche Zuege suchen

            List<Zug> MoeglicheZuege = figur.ZuegetrotzSchachVerbieten(schachfeld1);
            List<Zug> Rzuege = new List<Zug>();
            List<Zug> Azuege = new List<Zug>();
            if (figur is Bauer bauer)
            {
                foreach (Zug zug in MoeglicheZuege)
                {
                    if (zug.row == 0 || zug.row == 7)
                    {
                        if (figur.weiss)
                        {
                            Zug zurDame = new Zug(figur, zug.row, zug.col, new Dame(bauer.weiss, zug.row, zug.col, weisseDame));
                            Zug zumLaeufer = new Zug(figur, zug.row, zug.col, new Laeufer(bauer.weiss, zug.row, zug.col, weisserLaeufer));
                            Zug zumSpringer = new Zug(figur, zug.row, zug.col, new Springer(bauer.weiss, zug.row, zug.col, weisserSpringer));
                            Zug zumTurm = new Zug(figur, zug.row, zug.col, new Turm(bauer.weiss, zug.row, zug.col, weisserTurm));
                            Rzuege.Add(zug);
                            Azuege.Add(zurDame);
                            Azuege.Add(zumLaeufer);
                            Azuege.Add(zumSpringer);
                            Azuege.Add(zumTurm);

                        }
                        else
                        {
                            Zug zurDame = new Zug(figur, zug.row, zug.col, new Dame(bauer.weiss, zug.row, zug.col, schwarzeDame));
                            Zug zumLaeufer = new Zug(figur, zug.row, zug.col, new Laeufer(bauer.weiss, zug.row, zug.col, schwarzerLaeufer));
                            Zug zumSpringer = new Zug(figur, zug.row, zug.col, new Springer(bauer.weiss, zug.row, zug.col, schwarzerSpringer));
                            Zug zumTurm = new Zug(figur, zug.row, zug.col, new Turm(bauer.weiss, zug.row, zug.col, schwarzerTurm));
                            Rzuege.Add(zug);
                            Azuege.Add(zurDame);
                            Azuege.Add(zumLaeufer);
                            Azuege.Add(zumSpringer);
                            Azuege.Add(zumTurm);
                        }

                    }
                }
                foreach(Zug zug in Rzuege)
                {
                    MoeglicheZuege.Remove(zug);
                }
                foreach(Zug zug in Azuege)
                {
                    MoeglicheZuege.Add(zug);
                }
            }
            return MoeglicheZuege;
        }

        public List<Zug> MoeglicheZuegeFuerWeissOderSchwarzSuchen(Schachfeld schachfeld1)
        {
            //Der Zug muss immer durch ZugMachen gemacht werden, damit enpassant und die Rochade gemacht werden
            List<Zug> MoeglicheZuege = new List<Zug>();
            List<Figur> figurenAmZug = new List<Figur>();
                figurenAmZug = schachfeld1.ListDerFiugrenAmZug();
            foreach (Figur figur in figurenAmZug)
            {
                MoeglicheZuege.AddRange(MoeglicheZuegeHinzufuegen(figur, schachfeld1));
            }

            return MoeglicheZuege;
        }

            public void SchlechterZug(Schachfeld Schachfeld1)
            {
                //Durch die Deep Coypy verschwinden die Figuren nicht mehr
                Schachfeld Schachfeld = new Schachfeld(Schachfeld1);
                List<Zug> MoeglicheZuege = MoeglicheZuegeFuerWeissOderSchwarzSuchen(Schachfeld);
                Random random = new Random();
                Debug.WriteLine(MoeglicheZuege.Count);
                int randomIndex = random.Next(0, MoeglicheZuege.Count);
                Zug zufaelligerZug = MoeglicheZuege[randomIndex];
                Zug besterZug = zufaelligerZug;
                int AltRow = besterZug.figur.row;
                int AltCol = besterZug.figur.col;
                besterZug.bewertung = besterZug.FigurenZaehlen(Schachfeld);
                for (int i = 0; i < MoeglicheZuege.Count; i++)
                {
                    AltRow = besterZug.figur.row;
                    AltCol = besterZug.figur.col;
                    MoeglicheZuege[i].bewertung = MoeglicheZuege[i].FigurenZaehlen(Schachfeld);
                    if (MoeglicheZuege[i].bewertung != besterZug.bewertung)
                    {

                        if (!weiss)
                        {
                            if (MoeglicheZuege[i].bewertung < besterZug.bewertung)
                            {
                                besterZug = MoeglicheZuege[i];
                            }
                        }
                        if (weiss)
                        {
                            if (MoeglicheZuege[i].bewertung > besterZug.bewertung)
                            {
                                besterZug = MoeglicheZuege[i];
                            }
                        }
                    }


                }
                Debug.WriteLine("Bewertung: " + besterZug.bewertung);
                besterZug.figur = Schachfeld1.figurBei(AltRow, AltCol);
                ZugMachen(besterZug, Schachfeld1);
            }
            public void ZufallsZug(Schachfeld Schachfeld1)
            {
                Schachfeld Schachfeld = new Schachfeld(Schachfeld1);
                List<Zug> MoeglicheZuege = MoeglicheZuegeFuerWeissOderSchwarzSuchen(Schachfeld);
                Random random = new Random();
                Debug.WriteLine(MoeglicheZuege.Count);
                int randomIndex = random.Next(0, MoeglicheZuege.Count);
                Zug zufaelligerZug = MoeglicheZuege[randomIndex];

                zufaelligerZug.figur = Schachfeld1.figurBei(zufaelligerZug.figur.row, zufaelligerZug.figur.col);
                ZugMachen(zufaelligerZug, Schachfeld1);

            }
        }
    } 

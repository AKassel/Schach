using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    internal class Schachfeld
    {
        //Im Schachfeld sind die gleichen Panel Objekte wie im ChessBoardPanels Array aus der Schach Klasse
        public Panel[,] schachfeld;
        public bool weissAmZug = true;
        public List<Figur> WeisseFiguren = new List<Figur>();
        public List<Figur> SchwarzeFiguren = new List<Figur>();
        public Bauer enpassantBauer;
        public Bauer vorherigerEnpassantBauer;
        public Schachfeld(Panel[,] schachfeld)
        {
            this.schachfeld = schachfeld;
        }
        //Nachfolger der DeepCopyVomSchachfeld Methode
        public Schachfeld(Schachfeld schachfeld)
        {
            //Panel[,] copySchachfeld = new Panel[8, 8];
            // Kopieren des Array von Panels

            this.schachfeld = new Panel[8, 8];
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    this.schachfeld[row, col] = new Panel();

                }
            }
            
            //this.schachfeld = copySchachfeld;
            
            this.weissAmZug = schachfeld.weissAmZug;
            
            // Deep Copy der Figurenliste
            this.WeisseFiguren = new List<Figur>();
            foreach (Figur figur in schachfeld.WeisseFiguren)
            {
                Figur kopie = figur.klonen(); // Hier musst du die Clone-Methode der Figur implementieren
                this.WeisseFiguren.Add(kopie);
                this.schachfeld[figur.row, figur.col].Controls.Add(kopie);
                if (figur == schachfeld.enpassantBauer)
                {
                    enpassantBauer = (Bauer)kopie;
                }
                /*
                if (figur == schachfeld.vorherigerEnpassantBauer)
                {
                    vorherigerEnpassantBauer = (Bauer)kopie;
                }
                */
            }
            this.SchwarzeFiguren = new List<Figur>();

            foreach (Figur figur in schachfeld.SchwarzeFiguren)
            {
                Figur kopie = figur.klonen(); // Hier musst du die Clone-Methode der Figur implementieren
                this.SchwarzeFiguren.Add(kopie);
                this.schachfeld[figur.row, figur.col].Controls.Add(kopie);
                if (figur == schachfeld.enpassantBauer)
                {
                    enpassantBauer = (Bauer)kopie;
                }
                /*
                if(figur == schachfeld.vorherigerEnpassantBauer)
                {
                    vorherigerEnpassantBauer = (Bauer)kopie;
                }
                */
            }
            
            if (weissAmZug)
            {
                foreach(Figur figur in this.WeisseFiguren)
                {
                    figur.LegaleZuege(this.schachfeld);
                }
            }
            else
            {
                foreach(Figur figur in this.SchwarzeFiguren)
                {
                    figur.LegaleZuege(this.schachfeld);
                }
            }
            
            // Deep Copy des Schachfelds
        }
        public Figur figurBei(int row, int column)
        {
            return schachfeld[row, column].Controls.OfType<Figur>().FirstOrDefault();
        }
        public List<Figur> ListDerFiugrenAmZug()
        {
            if (weissAmZug)
            {
                return WeisseFiguren;
            }
            else
            {
                return SchwarzeFiguren;
            }
        }

    }
}

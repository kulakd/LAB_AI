using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_1
{
    public class Praca
    {
        public int Numer { get; set; } // numer zadania
        public int CzasPracy { get; set; } // czas pracy
        public int Start { get; set; } // czas rozpoczęcia
        public Praca NastepnaPraca { get; set; } // zadanie które może się wykonać dopiero gdy to się skończy
        public Praca PoprzedniaPraca { get; set; } // zadanie, po którego zakończeniu, to może się rozpocząć
        public bool PrzydzielProcesor { get; set; } // czy już przypisane do procesora
        public Procesor Procesor { get; set; }
        public int IDProcesor { get; set; } // indeks zadania w liście zadań procesora

        // Konstruktor
        public Praca(int numer, int czas)
        {
            Numer = numer;
            CzasPracy = czas;
            NastepnaPraca = null;
            PoprzedniaPraca = null;
            PrzydzielProcesor = false;
            Procesor = null;
        }

        public Praca(Praca praca)
        {
            Numer = praca.Numer;
            CzasPracy = praca.CzasPracy;
            Start = praca.Start;
            NastepnaPraca = null;
            PoprzedniaPraca = null;
            PrzydzielProcesor = true;
            Procesor = null;
            IDProcesor = praca.IDProcesor;
        }

        public int Koniec()
        {
            return Start + CzasPracy;
        }
    }
}


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
        #region Dane
        public int Numer { get; set; } // Numer danego zadania
        public int CzasPracy { get; set; } // Ogólny czas pracy
        public int Start { get; set; } // Czas rozpoczęcia pracy
        public Praca NastepnaPraca { get; set; } // Zakolejkowanie zadania, które będzie wykonywane po zrobieniu aktualnego
        public Praca PoprzedniaPraca { get; set; } // Zadanie wykonywane przed aktualnym
        public bool PrzydzielProcesor { get; set; } // Sprawdzenie, czy zadanie zostało już dopisane do procesora
        public Procesor Procesor { get; set; } // Procesor zadania
        public int IDProcesor { get; set; } // Indeks pracy w lisćie procesora
        #endregion
        #region Konstrukory i metody
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
        #endregion
    }
}


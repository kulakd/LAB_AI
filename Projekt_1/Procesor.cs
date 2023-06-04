using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_1
{
    public class Procesor
    {
        public List<Praca> ProcesorPrace { get; set; } // lista zadań
        // Konstruktor
        public Procesor()
        {
            ProcesorPrace = new List<Praca>();
        }
        // Metoda dodająca zadanie do procesora
        public void DodajPrace(Praca praca)
        {
            if (ProcesorPrace.Count > 0)
                praca.Start = ProcesorPrace[ProcesorPrace.Count - 1].Koniec();
            else
                praca.Start = 0;
            ProcesorPrace.Add(praca);

            praca.Procesor = this;
            praca.IDProcesor = ProcesorPrace.Count - 1;
            praca.PrzydzielProcesor = true;
        }
        // Metoda usuwająca zadanie z procesora
        public void UsunPrace(Praca praca)
        {
            ProcesorPrace.Remove(praca);
            praca.PrzydzielProcesor = false;
        }
        public int Koniec()
        {
            return ProcesorPrace.Count > 0 ? ProcesorPrace[ProcesorPrace.Count - 1].Koniec() : 0;
        }
    }
}

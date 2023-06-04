using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_1
{
    public class Harmonogram
    {
        public List<Procesor> Procesory { get; set; } // lista procesorów

        // Konstruktor
        public Harmonogram(int licznik)
        {
            Procesory = new List<Procesor>();
            for (int i = 0; i <licznik; i++)
                Procesory.Add(new Procesor());
        }

        // inicjalizacja harmonogramu
        public void Initialize(List<Praca> praca)
        {
            foreach (var p in praca)
                if (!p.PrzydzielProcesor)
                    PrzydzielPrace(p);
        }

        public static Harmonogram StworzKolejkePrac(List<Praca> prace, int licznik)
        {
            var harmonogram = new Harmonogram(licznik);
            foreach (var p in prace)
                harmonogram.PrzydzielPraceWKolejnosci(p);
            return harmonogram;
        }

        private void PrzydzielPraceWKolejnosci(Praca praca)
        {
            Praca praca_temp = praca;
            Procesor procesor = Procesory.OrderBy(p => p.Koniec()).FirstOrDefault();
            while (praca_temp != null)
            {
                procesor.DodajPrace(praca_temp);
                praca_temp = praca_temp.NastepnaPraca;
            }
        }

        // generowanie nowego harmonogramu na podstawie poprzedniego (zamiana miejscami zadań lub przeniesienie zadania na inny procesor)
        public Harmonogram Sasiad(Random rand)
        {
            // kopia starego harmonogramu
            Harmonogram harmonogram_kopia = Kopiuj();
            Praca praca_1 = null;
            Praca praca_2 = null;
            Procesor Procesor1 = null;
            Procesor Procesor2 = null;
            int IDpraca_1 = 0;
            int IDpraca_2 = 0;

            // wybór dwóch losowych zadań
            while (praca_1 == praca_2 || Procesor1 == Procesor2)
            {
                praca_1 = null;
                praca_2 = null;
                Procesor1 = harmonogram_kopia.Procesory[rand.Next(harmonogram_kopia.Procesory.Count)];
                Procesor2 = harmonogram_kopia.Procesory[rand.Next(harmonogram_kopia.Procesory.Count)];

                IDpraca_1 = rand.Next(Procesor1.ProcesorPrace.Count + 1); // + 1 ponieważ ostatni index oznacza dopisanie zadania na koniec listy, a nie podmianę
                IDpraca_2 = rand.Next(Procesor2.ProcesorPrace.Count + 1);

                if (Procesor1.ProcesorPrace.Count != 0 && IDpraca_1 != Procesor1.ProcesorPrace.Count)
                    praca_1 = Procesor1.ProcesorPrace[IDpraca_1];

                if (Procesor2.ProcesorPrace.Count != 0 && IDpraca_2 != Procesor2.ProcesorPrace.Count)
                    praca_2 = Procesor2.ProcesorPrace[IDpraca_2];
            }
            if (praca_1 != null)
            {
                praca_1 = PierwszaPraca(praca_1); // rekurencyjne pobranie ostatniego poprzednika
                IDpraca_1 = praca_1.IDProcesor;
            }
            if (praca_2 != null)
            {
                praca_2 = PierwszaPraca(praca_2);
                IDpraca_2 = praca_2.IDProcesor;
            }

            // robocze listy, zawierające ciąg zadań połączonych relacjami, służą do zamiany zadań lub przeniesienia zadania na inny procesor
            List<Praca> pom_prace_1 = new List<Praca>();
            List<Praca> pom_prace_2 = new List<Praca>();

            // dopisywanie kolejnych zadań połączonych relacjami do roboczej listy i usuwanie ich z listy zadań procesora
            while (praca_1 != null)
            {
                pom_prace_1.Add(praca_1);
                Procesor1.ProcesorPrace.Remove(praca_1);
                praca_1 = praca_1.NastepnaPraca;
            }
            while (praca_2 != null)
            {
                pom_prace_2.Add(praca_2);
                Procesor2.ProcesorPrace.Remove(praca_2);
                praca_2 = praca_2.NastepnaPraca;
            }

            // zamiana zadań lub przeniesienie zadania
            Procesor1.ProcesorPrace.InsertRange(IDpraca_1, pom_prace_2);
            Procesor2.ProcesorPrace.InsertRange(IDpraca_2, pom_prace_1);

            // Aktualizacja StartTime i IndexOnProcesor dla zadań na procesorach po podmianie
            for (int i = IDpraca_1; i < Procesor1.ProcesorPrace.Count; i++)
            {
                Procesor1.ProcesorPrace[i].Start = i == 0 ? 0 : Procesor1.ProcesorPrace[i - 1].Koniec();
                Procesor1.ProcesorPrace[i].IDProcesor = i;
            }
            for (int i = IDpraca_2; i < Procesor2.ProcesorPrace.Count; i++)
            {
                Procesor2.ProcesorPrace[i].Start = i == 0 ? 0 : Procesor2.ProcesorPrace[i - 1].Koniec();
                Procesor2.ProcesorPrace[i].IDProcesor = i;
            }
            return harmonogram_kopia;
        }

        // Metoda wyciągająca największy czas zakończenia ze wszystkich procesorów
        public int MaxCzas()
        {
            int maxKoniec = int.MinValue;
            foreach (var Procesor in Procesory)
                foreach (var Praca in Procesor.ProcesorPrace)
                    if (Praca.Koniec() > maxKoniec)
                        maxKoniec = Praca.Koniec();
            return maxKoniec;
        }

        // wypisanie harmonogramu - lista zadań dla każdego procesora gdzie liczba * oznacza czas wykonania zadania
        public void Print()
        {
            int i = 0;
            foreach (var procesor in Procesory)
            {
                i++;
                Console.Write($"P{i}: | ");
                foreach (var p in procesor.ProcesorPrace)
                {
                    Console.Write($"J{p.Numer}: ");
                    for (int j = 0; j < p.CzasPracy; j++)
                        Console.Write("*");
                    Console.Write(" | ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine($"Czas zakończenia pracy: {this.MaxCzas()}");
        }
        private Harmonogram Kopiuj()
        {
            Harmonogram harmonogram = new Harmonogram(this.Procesory.Count);
            int i=0,j=0;
            foreach (var procesor in harmonogram.Procesory)
            {
                foreach (var p in this.Procesory[i].ProcesorPrace)
                {
                    Praca nowaPraca = new Praca(p);
                    procesor.ProcesorPrace.Add(nowaPraca);
                    nowaPraca.Procesor = procesor;
                    if (p.PoprzedniaPraca != null)
                    {
                        nowaPraca.PoprzedniaPraca = procesor.ProcesorPrace[j - 1];
                        procesor.ProcesorPrace[j - 1].NastepnaPraca = nowaPraca;
                    }
                    j++;
                }
                j = 0;
                i++;
            }
            return harmonogram;
        }
        private Praca PierwszaPraca(Praca praca)
        {
            if (praca.PoprzedniaPraca == null) return praca;
            return PierwszaPraca(praca.PoprzedniaPraca);
        }
        private void PrzydzielPrace(Praca praca)
        {
            if (!praca.PrzydzielProcesor)
            {
                if (praca.PoprzedniaPraca != null)
                    PrzydzielPrace(praca.PoprzedniaPraca);
                var random = new Random();

                if (praca.PoprzedniaPraca != null)
                {
                    praca.PoprzedniaPraca.Procesor.DodajPrace(praca);
                    praca.PoprzedniaPraca.NastepnaPraca = praca;
                }
                else
                    Procesory[random.Next(0, Procesory.Count)].DodajPrace(praca);
            }
        }
    }
}

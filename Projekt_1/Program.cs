using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Projekt_1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Dane wejściowe
            // Dane wejściowe do zadania
            List<Praca> prace = new List<Praca>()
            {
                new Praca(1, 2),
                new Praca(2, 4),
                new Praca(3, 8),
                new Praca(4, 16),
                new Praca(5, 32)
            };
            // relacje pomiędzy pracami
            prace[0].PoprzedniaPraca = prace[2];
            prace[4].NastepnaPraca = prace[0];

            prace[4].PoprzedniaPraca = prace[3];
            prace[3].NastepnaPraca = prace[4];
            
            // procesory
            int procesory = 3;

            Stopwatch stoper = new Stopwatch();
            Harmonogram harmonogram;
            // Inicjalizacja harmonogramu początkowego
            Harmonogram harmonogramInit = new Harmonogram(procesory);       
            harmonogramInit.Initialize(prace);
            #endregion
            #region SA
            // znalezienie najlepszego harmonogramu
            SimulatedAnnealing sa = new SimulatedAnnealing(100.0, 0.01, 0.97);
            stoper.Start();
            harmonogram = sa.NajlepszyHarmonogram(harmonogramInit);
            stoper.Stop();
            long czastrwania = stoper.ElapsedMilliseconds;
            Console.WriteLine("Algorytm SimulatedAnnealing");
            harmonogram.Print();
            Console.WriteLine("Czas trwania algorytmu w ms: "+ czastrwania);
            Console.WriteLine("");
            Console.WriteLine("");
            #endregion
            #region Genetyczy
            Genetic ga = new Genetic(prace, procesory);
            stoper.Start();
            harmonogram = ga.NajlepszyHarmonogram();
            stoper.Stop();
            czastrwania = stoper.ElapsedMilliseconds;
            Console.WriteLine("Algorytm Genetyczny");
            harmonogram.Print();
            Console.WriteLine("Czas trwania algorytmu w ms: "+ czastrwania);
            Console.WriteLine("");
            Console.WriteLine("");
            #endregion
        }
    }
}
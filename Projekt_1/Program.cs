using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Projekt_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dane wejściowe
            // zbiór zadań
            List<Praca> prace = new List<Praca>()
            {
                new Praca(1, 2),
                new Praca(2, 12),
                new Praca(3, 4),
                new Praca(4, 8),
                new Praca(5, 7)
            };

            // relacje
            prace[0].PoprzedniaPraca = prace[2];
            prace[4].NastepnaPraca = prace[0];
            prace[4].PoprzedniaPraca = prace[3];
            prace[3].NastepnaPraca = prace[4];
            // liczba procesorów
            int k = 3;

            Stopwatch stoper = new Stopwatch();
            Harmonogram harmonogram;
            // Inicjalizacja harmonogramu początkowego
            Harmonogram harmonogramInit = new Harmonogram(k);
            harmonogramInit.Initialize(prace);
            SimulatedAnnealing sa = new SimulatedAnnealing(100.0, 0.01, 0.97);

            stoper.Start();
            harmonogram = sa.NajlepszyHarmonogram(harmonogramInit);
            stoper.Stop();

            long duration = stoper.ElapsedMilliseconds;
            harmonogram.Print();
            Console.WriteLine($"Czas trwania algorytmu w ms: {duration}");
        }
    }
}
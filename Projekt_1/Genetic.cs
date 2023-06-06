using GAF;
using GAF.Extensions;
using GAF.Operators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_1
{
    class Genetic
    {
        #region Konstruktor i dane wejściowe
        private const int Populacja = 100;
        private List<Praca> prace;
        private int Licznik;

        public Genetic(List<Praca> praca, int licznik)
        {
            Licznik = licznik;
            prace = praca;
        }
        #endregion
        #region Metody
        public Harmonogram NajlepszyHarmonogram()
        {
            
            var population = new Population();
            // Dodanie pracy do populacji
            List<Praca> PopulacjaPrace = new List<Praca>();
            foreach (var p in prace)
            {
                if (p.PoprzedniaPraca == null)
                    PopulacjaPrace.Add(p);
            }

            //tworzenie chromosomów
            for (var p = 0; p < Populacja; p++)
            {
                var chromosome = new Chromosome();
                foreach (var c in PopulacjaPrace)
                    chromosome.Genes.Add(new Gene(c));
                chromosome.Genes.ShuffleFast();
                population.Solutions.Add(chromosome);
            }

            //Tworzenie operatorów elit
            var elite = new Elite(5);

            //Tworzenie operatora mieszania
            var crossover = new Crossover(0.8)
            {
                CrossoverType = CrossoverType.DoublePointOrdered
            };

            //Tworzenie operatora mutacji
            var mutate = new SwapMutate(0.02);

            //Tworzenie algorytmu genetycznego
            var ga = new GeneticAlgorithm(population, CalculateFitness);

            //Dodaj operatory
            ga.Operators.Add(elite);
            ga.Operators.Add(crossover);
            ga.Operators.Add(mutate);

            //odpal algorytm
            ga.Run(Terminate);

            var fittest = ga.Population.GetTop(1)[0];

            List<Praca> prace2 = new List<Praca>();

            foreach (var gene in fittest.Genes)
                prace2.Add((Praca)gene.ObjectValue);

            Harmonogram harmonogram = Harmonogram.StworzKolejkePrac(prace2, Licznik);
            return harmonogram;
        }
        // Wyznaczanie najlepszego
        public double CalculateFitness(Chromosome chromosome)
        {
            var minTime = CalculateMinTime(chromosome);
            var fitness = 10 / minTime;
            return fitness > 1.0 ? 1.0 : fitness;

        }

        //Generowanie nowego harmonogramu na podstawie genów i zwrócenie czasu wykonywania
        private double CalculateMinTime(Chromosome chromosome)
        {
            List<Praca> prace = new List<Praca>();
            foreach (var gene in chromosome.Genes)
                prace.Add((Praca)gene.ObjectValue);
            Harmonogram harmonogram = Harmonogram.StworzKolejkePrac(prace, Licznik);
            return harmonogram.MaxCzas();
        }
        // Masowe wymieranie populacji
        public static bool Terminate(Population population, int currentGeneration, long currentEvaluation)
        {
            return currentGeneration > 100;
        }
        #endregion
    }
}

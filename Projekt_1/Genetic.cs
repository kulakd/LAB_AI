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
        private const int PopulationSize = 100;
        private List<Praca> prace;
        private int Licznik;

        public Genetic(List<Praca> praca, int licznik)
        {
            Licznik = licznik;
            prace = praca;
        }

        public Harmonogram NajlepszyHarmonogram()
        {
            var population = new Population();

            List<Praca> populationJobs = new List<Praca>();
            foreach (var job in prace)
            {
                if (job.PoprzedniaPraca == null)
                {
                    populationJobs.Add(job);
                }
            }

            //create the chromosomes
            for (var p = 0; p < PopulationSize; p++)
            {
                var chromosome = new Chromosome();
                foreach (var job in populationJobs)
                {
                    chromosome.Genes.Add(new Gene(job));
                }

                chromosome.Genes.ShuffleFast();
                population.Solutions.Add(chromosome);
            }

            //create the elite operator
            var elite = new Elite(5);

            //create the crossover operator
            var crossover = new Crossover(0.8)
            {
                CrossoverType = CrossoverType.DoublePointOrdered
            };

            //create the mutation operator
            var mutate = new SwapMutate(0.02);

            //create the GA
            var ga = new GeneticAlgorithm(population, CalculateFitness);

            //hook up to some useful events
            /*ga.OnGenerationComplete += ga_OnGenerationComplete;
            ga.OnRunComplete += ga_OnRunComplete;*/

            //add the operators
            ga.Operators.Add(elite);
            ga.Operators.Add(crossover);
            ga.Operators.Add(mutate);

            //run the GA
            ga.Run(Terminate);

            var fittest = ga.Population.GetTop(1)[0];

            List<Praca> jobs = new List<Praca>();

            foreach (var gene in fittest.Genes)
            {
                jobs.Add((Praca)gene.ObjectValue);
            }

            Harmonogram schedule = Harmonogram.StworzKolejkePrac(jobs, Licznik);

            return schedule;
        }

        public double CalculateFitness(Chromosome chromosome)
        {
            var minTime = CalculateMinTime(chromosome);

            var fitness = 10 / minTime;
            return fitness > 1.0 ? 1.0 : fitness;

        }

        // Generowanie nowego Harmonogram na podstawie posortowanych zadań (genów) i zwrócenie czasu wykonania tego harmonogramu
        private double CalculateMinTime(Chromosome chromosome)
        {
            List<Praca> jobs = new List<Praca>();

            foreach (var gene in chromosome.Genes)
            {
                jobs.Add((Praca)gene.ObjectValue);
            }

            Harmonogram newSchedule = Harmonogram.StworzKolejkePrac(jobs, Licznik);

            return newSchedule.MaxCzas();
        }

        public static bool Terminate(Population population, int currentGeneration, long currentEvaluation)
        {
            return currentGeneration > 100;
        }
    }
}

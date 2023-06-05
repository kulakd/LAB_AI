using Microsoft.ML.Probabilistic.Models;
using Microsoft.ML.Probabilistic.Distributions;
using Range = Microsoft.ML.Probabilistic.Models.Range;

namespace projekt3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Zmienne
            Variable<double> Prawdopodobienstwo = Variable.Beta(1, 1);
            Range rzuty = new Range(100); // ile razy rzucamy monete
            VariableArray<bool> runda = Variable.Array<bool>(rzuty);
            int Orzel = 0, Reszka=0;
            // Definiowanie prior dla parametru sukcesu
            runda[rzuty] = Variable.Bernoulli(Prawdopodobienstwo).ForEach(rzuty);
            // cała zabawa
            runda.ObservedValue = new bool[] {
               true, true, false, true, true, true, false, true, false, true,
                false, true, false, true, false, true, false, true, false, true,
                true, false, true, false, true, false, true, false, true, false, 
                false, true, false, true, false, true, false, true, false, true, 
                true, false, true, false, true, false, true, false, true, false,
                false, true, true, true, false, true, false, true,true, true, 
                true, true, true, false, true, false, true, false, true, false, 
                false, true, false, true, false, true, false, true, false, true,
                true, false, true, false, true, true, true, false, true, false,
                false, true, false, true, false, true, false, true, true, true };
            for(int i=0;i<100;i++) 
            {
                if (runda.ObservedValue[i] == true)
                    Orzel++;
                if (runda.ObservedValue[i] == false)
                    Reszka++;
            }
            // Wnioskowanie
            InferenceEngine engine = new InferenceEngine();
            var inferredProbability = engine.Infer<Beta>(Prawdopodobienstwo);
            // Wyników
            Console.WriteLine("Średnia rzutów: " + inferredProbability.GetMean());
            Console.WriteLine("Orzel: "+ Orzel+" Reszka: " +Reszka);
        }
    }
}
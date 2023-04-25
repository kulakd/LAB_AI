using System;

public class SimulatedAnnealing
{
    private static readonly Random rand = new Random();
    private readonly double initTemp;
    private readonly double coolingRate;
    
    public SimulatedAnnealing(double initTemp, double coolingRate)
    {
        this.initTemp = initTemp;
        this.coolingRate = coolingRate;
    }

    public double Solve(Func<double, double> square, Func<double, double> AssistFunc, double initSolution)
    {
        double temp = initTemp;
        double currSolution = initSolution;
        double bestSolution = currSolution;

        while (temp > 0.00001)
        {
            double neighbor = AssistFunc(currSolution);
            double delta = square(neighbor) - square(currSolution);
            if (delta < 0 || rand.NextDouble() < Math.Exp(-delta / temp))
                currSolution = neighbor;
            if (square(currSolution) < square(bestSolution))
                bestSolution = currSolution;
            temp *= 1 - coolingRate;
        }
        return bestSolution;
    }

    static void Main()
    {
        SimulatedAnnealing final = new SimulatedAnnealing(1000, 0.003);
        Func<double, double> square = x => x * x;
        Func<double, double> AssistFunc = x => x + (rand.NextDouble() - 0.5) * 10;

        double initSolution = 100;
        double solution = final.Solve(square, AssistFunc, initSolution);

        Console.WriteLine("Minimum value: {0}", solution);
        Console.WriteLine("Minimum value: {0}", square(solution));
    }
}
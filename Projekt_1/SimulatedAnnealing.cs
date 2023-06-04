using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_1
{
    class SimulatedAnnealing
    {
        private double Temperature;
        private double TemperatureEnd;
        private double Alpha;

        public SimulatedAnnealing(double temperature, double temperatureEnd, double alpha)
        {
            Temperature = temperature;
            TemperatureEnd = temperatureEnd;
            Alpha = alpha;
        }

        public Harmonogram NajlepszyHarmonogram(Harmonogram harmonogram)
        {
            int iterator = 0;

            Random rand = new Random();
            while (Temperature >= TemperatureEnd) // Stopping Criterion
            {
                // Wygeneruj nowe rozwiązanie na podstawie poprzedniego
                Harmonogram newSchedule = harmonogram.Sasiad(rand); // Exploration Criterion

                // Sprawdź czy nowe rozwiązanie jest lepsze lub zaakceptuj gorsze na podstawie funkcji akceptacji
                if (newSchedule.MaxCzas() < harmonogram.MaxCzas() || Math.Exp((harmonogram.MaxCzas() - newSchedule.MaxCzas()) / Temperature) > rand.NextDouble()) // Acceptance Criterion
                {
                    harmonogram = newSchedule;
                }

                // Schłodzenie
                if (iterator % 10 == 0) // Temperature Length
                {
                    Temperature *= Alpha; // Cooling Scheme
                }
                iterator++;
            }

            return harmonogram;
        }
    }
}

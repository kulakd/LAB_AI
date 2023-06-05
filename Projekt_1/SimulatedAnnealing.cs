using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_1
{
    class SimulatedAnnealing
    {
        #region Konstruktory i dane początkowe
        private double Temperature;
        private double TemperatureEnd;
        private double Alpha;
        
        public SimulatedAnnealing(double temperature, double temperatureEnd, double alpha)
        {
            Temperature = temperature;
            TemperatureEnd = temperatureEnd;
            Alpha = alpha;
        }
        #endregion
        #region Metody
        public Harmonogram NajlepszyHarmonogram(Harmonogram harmonogram)
        {
            int iterator = 0;
            Random rand = new Random();
            while (Temperature >= TemperatureEnd) // Wyszarzanie 
            {
                //Generuj nowy harmonogram na podstawie poprzednich 
                Harmonogram nowy = harmonogram.Sasiad(rand);
                // Wybranie nowego harmonogramu lub gorszego
                if (nowy.MaxCzas() < harmonogram.MaxCzas() || Math.Exp((harmonogram.MaxCzas() - nowy.MaxCzas()) / Temperature) > rand.NextDouble())
                    harmonogram = nowy;
                // Chłodzenie
                if (iterator % 10 == 0)
                    Temperature *= Alpha;
                iterator++;
            }
            return harmonogram;
        }
        #endregion
    }
}

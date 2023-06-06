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
        private double Temperatura;
        private double TemperaturaKoniec;
        private double zmiana;
        
        public SimulatedAnnealing(double _temperatura, double _temperaturaKoniec, double _zmiana)
        {
            Temperatura = _temperatura;
            TemperaturaKoniec = _temperaturaKoniec;
            zmiana = _zmiana;
        }
        #endregion
        #region Metody
        public Harmonogram NajlepszyHarmonogram(Harmonogram harmonogram)
        {
            int iterator = 0;
            Random rand = new Random();
            while (Temperatura >= TemperaturaKoniec) // Wyszarzanie 
            {
                //Generuj nowy harmonogram na podstawie poprzednich 
                Harmonogram nowy = harmonogram.Sasiad(rand);
                // Wybranie nowego harmonogramu lub gorszego
                if (nowy.MaxCzas() < harmonogram.MaxCzas() || Math.Exp((harmonogram.MaxCzas() - nowy.MaxCzas()) / Temperatura) > rand.NextDouble())
                    harmonogram = nowy;
                // Chłodzenie
                if (iterator % 10 == 0)
                    Temperatura *= zmiana;
                iterator++;
            }
            return harmonogram;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.ReadHistogram
{
    internal abstract class LanguageInterval
    {
        private readonly Dictionary<char, List<double>> stats;

        protected LanguageInterval(Dictionary<char, List<double>> statsStorage, Dictionary<char, double> histogram)
        {
            // инициализируется ссылкой на конкретный язык
            stats = statsStorage;
            //проходим по каждому символу 
            foreach (var c in histogram.Keys)
            {
                //если символ уже есть в статс то
                if (stats.ContainsKey(c))
                {
                    //добавляем его частоту
                    stats[c].Add(histogram[c]);
                }
                //если символа нет то
                else
                {
                    //создаем новый список с этой частотой и добавляем 
                    stats.Add(c, new List<double> { histogram[c] });
                }
            }
        }
        //PLInterval - словарь куда записываются доверительный интервал символ - min,max                                                         
        public void GetInterval(Dictionary<char, (double min, double max)> PLInterval)
        {
            const double z = 1.96; // Z-значение для 95% доверительного интервала

            foreach (var c in stats.Keys)
            {
                List<double> freqs = stats[c];
                //если у символа нет данных - пропускаем
                if (freqs.Count == 0)
                {
                    continue;
                }
                //если 1 частота - то записываем ее как минимальную и максимальную
                else if (freqs.Count == 1)
                {
                    double value = freqs[0];
                    PLInterval[c] = (value, value);
                }
                else
                {
                    double mean = freqs.Average();
                    double sumOfSquaredDeviations = 0.0;
                    foreach (double f in freqs)
                    {
                        double deviation = f - mean;
                        sumOfSquaredDeviations += deviation * deviation;
                    }
                    double variance = sumOfSquaredDeviations / (freqs.Count - 1);
                    double stdDev = Math.Sqrt(variance);
                    double margin = z * (stdDev / Math.Sqrt(freqs.Count));
                    double min = mean - margin;
                    double max = mean + margin;

                    PLInterval[c] = (min, max);
                }
            }
        }
    }

    class CsInterval : LanguageInterval
    {
        private static readonly Dictionary<char, List<double>> _csStats = new Dictionary<char, List<double>>();

        public CsInterval(Dictionary<char, double> histogram) : base(_csStats, histogram) { }
    }

    class CppInterval : LanguageInterval
    {
        private static readonly Dictionary<char, List<double>> _cppStats = new Dictionary<char, List<double>>();

        public CppInterval(Dictionary<char, double> histogram) : base(_cppStats, histogram) { }
    }

    class PyInterval : LanguageInterval
    {
        private static readonly Dictionary<char, List<double>> _pyStats = new Dictionary<char, List<double>>();

        public PyInterval(Dictionary<char, double> histogram) : base(_pyStats, histogram) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.ReadHistogram
{
    internal abstract class Histogram
    {
        // гистограмма в ввиде символ - значение
        public Dictionary<char, double> histogram = new Dictionary<char, double>();
        public static Dictionary<char, (double min, double max)> CsInterval = new Dictionary<char, (double min, double max)>();
        public static Dictionary<char, (double min, double max)> CppInterval = new Dictionary<char, (double min, double max)>();
        public static Dictionary<char, (double min, double max)> PyInterval = new Dictionary<char, (double min, double max)>();

        public Histogram(List<char> chars)
        {
            // сколько раз встречается символ в массиве.
            foreach (char c in chars)
            {
                //если символ есть в массиве
                if (histogram.ContainsKey(c))
                {
                    //увеличиваем его количество
                    histogram[c] ++;
                }
                else
                {
                    //если символ впервые встретился в массиве то задаем ему 1
                    histogram.Add(c, 1);
                }
            }

            // обновляем histogram
            foreach (char c in histogram.Keys)
            {   
                //делим количество нужно символа на общее количество найденых символов
                histogram[c] /= (double)chars.Count;
            }
        }
    }

    class CsHistogram : Histogram
    {
        private LanguageInterval interval;
        public CsHistogram(List<char> chars) : base(chars)
        {
            interval = new CsInterval(histogram);
        }
        public void GetInterval()
        {
            interval.GetInterval(CsInterval);
        }
    }
    class CppHistogram : Histogram
    {
        private LanguageInterval interval;
        public CppHistogram(List<char> chars) : base(chars)
        {
            interval = new CppInterval(histogram);
        }
        public void GetInterval()
        {
            interval.GetInterval(CppInterval);
        }
    }
    class PyHistogram : Histogram
    {
        private LanguageInterval interval;
        public PyHistogram(List<char> chars) : base(chars)
        {
            interval = new PyInterval(histogram);
        }
        public void GetInterval()
        {
            interval.GetInterval(PyInterval);
        }
    }
    class UnknownLanguageHistogram : Histogram
    {
        public Dictionary<char, double> UKhistogram;
        public UnknownLanguageHistogram(List<char> chars) : base(chars) 
        {
            this.UKhistogram = histogram;
        }
    }
}



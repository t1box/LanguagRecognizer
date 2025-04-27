using System;
using System.Collections.Generic;
using System.IO;
using ProgramLanguage.ReadHistogram;

namespace ProgramLanguage
{
    internal class Program
    {
        class Comparison
        {
            Dictionary<char, (double min, double max)> Cs = Histogram.CsInterval;
            Dictionary<char, (double min, double max)> Cpp = Histogram.CppInterval;
            Dictionary<char, (double min, double max)> Py = Histogram.PyInterval;
            

            public int CsCount = 0;
            public int CppCount = 0;
            public int PyCount = 0;
            public int ConstructsScore = 0;

            public Comparison(Dictionary<char, double> histogram, int СonstructsScore)
            {
                foreach (char test in histogram.Keys)
                {
                    (double min, double max) CSinterval;
                    (double min, double max) CPPinterval;
                    (double min, double max) PYinterval;
                    if (Cs.TryGetValue(test, out CSinterval))
                    {
                        double value = histogram[test];
                        if (value < CSinterval.max && value > CSinterval.min)
                        {
                            CsCount++;
                        }
                    }

                    if (Cpp.TryGetValue(test, out CPPinterval))
                    {
                        double value = histogram[test];
                        if (value < CPPinterval.max && value > CPPinterval.min)
                        {
                            CppCount++;
                        }
                    }

                    if (Py.TryGetValue(test, out PYinterval))
                    {
                        double value = histogram[test];
                        if (value < PYinterval.max && value > PYinterval.min)
                        {
                            PyCount++;
                        }
                    }
                }
                Console.WriteLine("\n  Частота встречания символов в искомом файле\n" + "-----------------------------------------------");
                foreach (var c in histogram)
                {
                    Console.WriteLine($"{c.Key} : {c.Value}");
                }

                this.ConstructsScore = СonstructsScore;
            }

            public void PrintResult()
            {
                // Распределения весов
                int csScore = CsCount + (ConstructsScore > 0 ? ConstructsScore * 3 : 0);// C# и C++ имеют одинаковый коэф. 3, так как ситаксис очень похож
                int cppScore = CppCount + (ConstructsScore < 0 && ConstructsScore > -2 ? -ConstructsScore * 3 : 0);
                int pyScore = PyCount + (ConstructsScore == -2 ? 10 : 0); // Python конструкции имеют больший вес (коэф. 10), так как его синтаксис сильно отличается

                if (csScore > cppScore && csScore > pyScore)
                {
                    Console.WriteLine("Код написан на C#");
                    Console.WriteLine($"cs: {csScore},  cpp: {cppScore},  py: {pyScore}");
                }
                else if (cppScore > csScore && cppScore > pyScore)
                {
                    Console.WriteLine("Код написан на C++");
                    Console.WriteLine($"cs: {csScore},  cpp: {cppScore},  py: {pyScore}");
                }
                else if (pyScore > csScore && pyScore > cppScore)
                {
                    Console.WriteLine("Код написан на Python");
                    Console.WriteLine($"cs: {csScore},  cpp: {cppScore},  py: {pyScore}");
                }
                else
                {
                    Console.WriteLine("Не удалось точно определить язык");
                    Console.WriteLine($"cs: {CsCount},  cpp: {CppCount},  py: {PyCount}");
                }
            }
           
        }
        class Studing
        {
            private Read reader;
            private Histogram _histogram;
            private static int cscount = 1;
            private static int cppcount = 1;
            private static int pycount = 1;
            public Studing(ILanguageFactory factory, string way)
            {
                reader = factory.CreateReader(way);
                _histogram = factory.CreateHistogram(reader);
                CalculateIntervals();
            }
            public void CalculateIntervals()
            {
                if (_histogram is CsHistogram csHistogram)
                {
                    csHistogram.GetInterval();
                    Console.WriteLine($"\nИтерация {cscount} (после {cscount} файла):\n");
                    foreach (var c in _histogram.histogram.Keys)
                    {
                        Console.WriteLine($"Символ {c}:");
                        Console.WriteLine($"Частота появления в файле: {_histogram.histogram[c]}");
                        if (Histogram.CsInterval.ContainsKey(c))
                        {
                            Console.WriteLine($"Доверительный интервал: [{Histogram.CsInterval[c].min}, {Histogram.CsInterval[c].max}]"); ;
                        }
                    }
                    cscount++;
                }
                else if (_histogram is CppHistogram cppHistogram)
                {
                    cppHistogram.GetInterval();
                    Console.WriteLine($"\nИтерация {cppcount} (после {cppcount} файла):\n");
                    foreach (var c in _histogram.histogram.Keys)
                    {
                        Console.WriteLine($"Символ {c}:");
                        Console.WriteLine($"Частота появления в файле: {_histogram.histogram[c]}");
                        if (Histogram.CsInterval.ContainsKey(c))
                        {
                            Console.WriteLine($"Доверительный интервал: [{Histogram.CppInterval[c].min}, {Histogram.CppInterval[c].max}]"); ;
                        }
                    }
                    cppcount++;
                }
                else if(_histogram is PyHistogram pyHistogram)
                {
                    pyHistogram.GetInterval();
                    Console.WriteLine($"\nИтерация {pycount} (после {pycount} файла):\n");
                    foreach (var c in _histogram.histogram.Keys)
                    {
                        Console.WriteLine($"Символ {c}:");
                        Console.WriteLine($"Частота появления в файле: {_histogram.histogram[c]}");
                        if (Histogram.CsInterval.ContainsKey(c))
                        {
                            Console.WriteLine($"Доверительный интервал: [{Histogram.PyInterval[c].min}, {Histogram.PyInterval[c].max}]"); ;
                        }
                    }
                    pycount++;
                }
            }
            public static void Print()
            {
                Console.WriteLine("\n\tИтоговые доверительные интервалы\n");
                Console.WriteLine("\tДоверительный интервал для C#\n" + "-----------------------------------------------");
                foreach (var c in Histogram.CsInterval)
                {
                    Console.WriteLine($"{c.Value.min} < {c.Key} < {c.Value.max}");
                }
                Console.WriteLine("\n\tДоверительный интервал для C++\n" + "-----------------------------------------------");
                foreach (var c in Histogram.CppInterval)
                {
                    Console.WriteLine($"{c.Value.min} < {c.Key} < {c.Value.max}");
                }
                Console.WriteLine("\n\tДоверительный интервал для Python\n" + "-----------------------------------------------");
                foreach (var c in Histogram.PyInterval)
                {
                    Console.WriteLine($"{c.Value.min} < {c.Key} < {c.Value.max}");
                }
            }
        }
        static void Main(string[] args)
        {
            Console.BufferHeight = 9999;

            //CS
            string Csfolder = "CS";

            string[] CsFiles = Directory.GetFiles(Csfolder);
            Console.WriteLine("\n\t\tОбучения для C#\n" + "-------------------------------------------------------");
            Studing[] Cs = new Studing[CsFiles.Length];
            for (int i = 0; i < CsFiles.Length; i++)
            {
                Cs[i] = new Studing(new CsFactory(), CsFiles[i]);
            }

            //CPP
            string Cppfolder = "CPP";

            string[] CppFiles = Directory.GetFiles(Cppfolder);
            Console.WriteLine("\n\t\tОбучения для C++\n" + "-------------------------------------------------------");
            Studing[] Cpp = new Studing[CppFiles.Length];
            for (int i = 0; i < CppFiles.Length; i++)
            {
                Cpp[i] = new Studing(new CppFactory(), CppFiles[i]);
            }


            //PY
            string Pyfolder = "PY";

            string[] PyFiles = Directory.GetFiles(Pyfolder);
            Console.WriteLine("\n\t\tОбучения для Python\n" + "-------------------------------------------------------");
            Studing[] Py = new Studing[PyFiles.Length];
            for (int i = 0; i < PyFiles.Length; i++)
            {
                Py[i] = new Studing(new PyFactory(), PyFiles[i]);
            }
            

            Studing.Print();

            Console.WriteLine("\n\t\tИскомый язык\n" + "-------------------------------------------------------");
            string TestWay = "1.txt";
            ILanguageFactory factory = new UnknownLanguageFactory();
            Read reader = factory.CreateReader(TestWay);
            Histogram histogram = factory.CreateHistogram(reader);

            Console.WriteLine("\n\tРаспределения весов для языка\n" + "-----------------------------------------------");
            int ConstructsScore = reader.CountLanguageSpecificConstructs();
            Console.WriteLine($"Закончил с кодом: {ConstructsScore}");

            Comparison t = new Comparison(histogram.histogram, ConstructsScore);
            Console.WriteLine();
            t.PrintResult();
        }
    }
}

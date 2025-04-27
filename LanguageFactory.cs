using System;
using System.Collections.Generic;
using ProgramLanguage.ReadHistogram;

namespace ProgramLanguage
{
    internal interface ILanguageFactory
    {
        public Read CreateReader(string filePath);
        public Histogram CreateHistogram(Read reader);  // Принимаем Read, а не List<char>
    }

    internal class CsFactory : ILanguageFactory
    {
        public Read CreateReader(string filePath) => new CsRead(filePath);
        public Histogram CreateHistogram(Read reader) => new CsHistogram(reader.GetLanguageSpecificChars());
    }
    internal class CppFactory : ILanguageFactory
    {
        public Read CreateReader(string filePath) => new CppRead(filePath);
        public Histogram CreateHistogram(Read reader) => new CppHistogram(reader.GetLanguageSpecificChars());
    }
    internal class PyFactory : ILanguageFactory
    {
        public Read CreateReader(string filePath) => new PyRead(filePath);
        public Histogram CreateHistogram(Read reader) => new PyHistogram(reader.GetLanguageSpecificChars());
    }
    internal class UnknownLanguageFactory : ILanguageFactory
    {
        public Read CreateReader(string filePath) => new UnknownLanguageRead(filePath);
        public Histogram CreateHistogram(Read reader) => new UnknownLanguageHistogram(reader.GetLanguageSpecificChars());
    }

}

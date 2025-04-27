using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage.ReadHistogram
{
    internal abstract class Read
    {
        protected static char[] separators = new char[] { ' ', '\n', '\t', '.', ',', ';', '!', '?', '-', '(', ')', '"' };

        protected List<string> words = new List<string>();

        public Read(string way)
        {
            //читаем построчно файл
            using (StreamReader file = new StreamReader(way))
            {
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    //разбиваем строку на слова с помощью сепараторов и убираем пустые пробелы в строке
                    string[] wordsinline = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    //добавляем слова в массив words
                    words.AddRange(wordsinline);
                }
            }
        }

        public abstract List<char> GetLanguageSpecificChars();
        public virtual int CountLanguageSpecificConstructs() { return 100; }
    }

    class CsRead : Read
    {
        static readonly string[] CSwords = { 
            // Основные ключевые слова
            "var", "async", "await", "namespace", "class", "interface", "struct", "enum",
            "public", "private", "protected", "internal", "static", "readonly", "const",
            "using", "new", "this", "base", "out", "ref", "in", "params", "operator",
    
            // Управляющие конструкции
            "if", "else", "switch", "case", "for", "foreach", "while", "do", "break",
            "continue", "return", "throw", "try", "catch", "finally", "goto",
    
            // ООП и типы
            "abstract", "virtual", "override", "sealed", "partial", "record", "delegate",
            "event", "property", "get", "set", "init", "required",
    
            // LINQ и коллекции
            "from", "where", "select", "group", "into", "orderby", "join", "let", "on", "equals",
    
            // Типы и модификаторы
            "bool", "byte", "sbyte", "char", "decimal", "double", "float", "int", "uint",
            "long", "ulong", "short", "ushort", "object", "string", "dynamic", "void",
    
            // Проверки и безопасность
            "is", "as", "typeof", "nameof", "checked", "unchecked", "unsafe", "fixed",
            "stackalloc", "volatile",
    
            // Современные фичи
            "scoped", "with", "and", "or", "not", "global",
    
            // Атрибуты
            "Attribute", "Obsolete", "Serializable", "DllImport"};

        private List<char> _FinalChars = new List<char>();

        public CsRead(string way) : base(way)
        {
            //проходим по каждому ключевому слову из словаря
            foreach (string word in CSwords)
            {
                //проверям содержится ли ключевое слово в массиве слов
                if (words.Contains(word))
                {
                    //если слово содержится то каждый его символ добавляем в массив FingalChars
                    foreach (char ch in word) { _FinalChars.Add(ch); }
                }
            }
        }

        public override List<char> GetLanguageSpecificChars() => _FinalChars;
    }
    class CppRead : Read
    {
        static readonly string[] CPPwords = {
            // Основные ключевые слова
            "auto", "decltype", "constexpr", "constinit", "consteval", "template", "typename",
            "namespace", "class", "struct", "union", "enum", "using", "typedef",
            "public", "private", "protected", "friend", "virtual", "override", "final",
            "static", "thread_local", "mutable", "const", "volatile", "register", "extern",
    
            // Управляющие конструкции
            "if", "else", "switch", "case", "for", "while", "do", "break", "continue",
            "return", "throw", "try", "catch", "goto",
    
            // Операторы и приведения
            "new", "delete", "operator", "explicit", "and", "or", "not", "bitand", "bitor",
            "compl", "xor", "and_eq", "or_eq", "xor_eq", "not_eq", "dynamic_cast",
            "static_cast", "const_cast", "reinterpret_cast",
    
            // Шаблоны и концепты
            "template", "requires", "concept", "module", "import", "export",
    
            // Корутины
            "co_await", "co_return", "co_yield",
    
            // Типы
            "bool", "char", "char8_t", "char16_t", "char16_t", "char32_t", "wchar_t",
            "short", "int", "long", "float", "double", "void", "nullptr", "size_t",
            "ptrdiff_t", "int8_t", "int16_t", "int32_t", "int64_t", "uint8_t", "uint16_t",
            "uint32_t", "uint64_t",
    
            // Атрибуты и спецификаторы
            "alignas", "alignof", "noexcept", "inline", "asm", "export", "typename",
    
            // Макросы и препроцессор (хотя технически не ключевые слова)
            "#include", "#define", "#ifdef", "#ifndef", "#endif", "#pragma", "#if", "#else",
            "#elif", "#error", "#warning", "#line", "#undef",
    
            // Популярные типы из STD
            "vector", "string", "map", "unordered_map", "set", "unordered_set", "pair",
            "tuple", "optional", "variant", "any", "function", "shared_ptr", "unique_ptr"};

        private List<char> _FinalChars = new List<char>();

        public CppRead(string way) : base(way)
        {
            foreach (string word in CPPwords)
            {
                if (words.Contains(word))
                {
                    foreach (char ch in word) { _FinalChars.Add(ch); }
                }
            }
        }
        public override List<char> GetLanguageSpecificChars() => _FinalChars;
    }

    class PyRead : Read
    {
        static readonly string[] PYwords = { 
            // Основные ключевые слова  
            "False", "None", "True", "and", "as", "assert", "async", "await",
            "break", "continue", "class", "def", "del", "elif", "else", "except",
            "finally", "for", "from", "global", "if", "import", "in", "is",
            "lambda", "nonlocal", "not", "or", "pass", "raise", "return",
            "try", "while", "with", "yield",  

            // Встроенные функции и часто используемые  
            "abs", "all", "any", "ascii", "bin", "bool", "bytearray", "bytes",
            "callable", "chr", "classmethod", "compile", "complex",
            "delattr", "dict", "dir", "divmod", "enumerate", "eval", "exec",
            "filter", "float", "format", "frozenset",
            "getattr", "globals", "hasattr", "hash", "help", "hex",
            "id", "input", "int", "isinstance", "issubclass", "iter",
            "len", "list", "locals", "map", "max", "memoryview", "min",
            "next", "object", "oct", "open", "ord", "pow", "print", "property",
            "range", "repr", "reversed", "round", "set", "setattr", "slice",
            "sorted", "staticmethod", "str", "sum", "super",
            "tuple", "type", "vars", "zip",  

            // Специальные методы (dunder methods)  
            "__init__", "__name__", "__main__", "__file__", "__doc__",
            "__annotations__", "__dict__", "__slots__", "__module__",
            "__enter__", "__exit__", "__iter__", "__next__",
            "__getitem__", "__setitem__", "__delitem__",
            "__len__", "__contains__", "__call__",
            "__add__", "__sub__", "__mul__", "__truediv__",
            "__eq__", "__ne__", "__lt__", "__le__", "__gt__", "__ge__",
            "__str__", "__repr__", "__bool__",  

            // Популярные модули и типы  
            "os", "sys", "math", "random", "datetime", "json", "re",
            "collections", "itertools", "functools", "typing",
            "numpy", "pandas", "matplotlib", "tensorflow", "pytorch",
            "flask", "django", "asyncio", "threading", "multiprocessing",
            "argparse", "logging", "unittest", "pytest",
            "pathlib", "socket", "ssl", "hashlib", "csv", "sqlite3" };

        private List<char> _FinalChars = new List<char>();

        public PyRead(string way) : base(way)
        {
            foreach (string word in PYwords)
            {
                if (words.Contains(word))
                {
                    foreach (char ch in word) { _FinalChars.Add(ch); }
                }
            }
        }
        public override List<char> GetLanguageSpecificChars() => _FinalChars;
    }

    class UnknownLanguageRead : Read
    {
        static readonly string[] Allwords = {
            // Основные ключевые слова (общие для нескольких языков)
            "class", "namespace", "struct", "enum", "public", "private", "protected",
            "static", "const", "using", "new", "operator", "if", "else", "switch",
            "case", "for", "while", "do", "break", "continue", "return", "throw",
            "try", "catch", "finally", "goto", "virtual", "override", "and", "or",
            "not", "bool", "char", "float", "double", "int", "long", "short", "void",
            "async", "await", "this", "base", "true", "false", "null", "import",
            "from", "global", "in", "is", "lambda", "pass", "raise", "with", "yield",
    
            // Уникальные для C#
            "var", "interface", "readonly", "internal", "out", "ref", "in", "params",
            "abstract", "sealed", "partial", "record", "delegate", "event", "property",
            "get", "set", "init", "required", "where", "select", "group", "into",
            "orderby", "join", "let", "on", "equals", "byte", "sbyte", "uint", "ulong",
            "ushort", "object", "string", "dynamic", "as", "typeof", "nameof", "checked",
            "unchecked", "unsafe", "fixed", "stackalloc", "volatile", "scoped", "with",
            "Attribute", "Obsolete", "Serializable", "DllImport",
    
            // Уникальные для C++
            "auto", "decltype", "constexpr", "constinit", "consteval", "template", "typename",
            "union", "typedef", "friend", "final", "thread_local", "mutable", "register", "extern",
            "delete", "explicit", "bitand", "bitor", "compl", "xor", "and_eq", "or_eq", "xor_eq",
            "not_eq", "dynamic_cast", "static_cast", "const_cast", "reinterpret_cast", "requires",
            "concept", "module", "export", "co_await", "co_return", "co_yield", "char8_t", "char16_t",
            "char32_t", "wchar_t", "nullptr", "size_t", "ptrdiff_t", "int8_t", "int16_t", "int32_t",
            "int64_t", "uint8_t", "uint16_t", "uint32_t", "uint64_t", "alignas", "alignof", "noexcept",
            "inline", "asm", "#include", "#define", "#ifdef", "#ifndef", "#endif", "#pragma", "#if", "#else",
            "#elif", "#error", "#warning", "#line", "#undef", "vector", "map", "unordered_map", "set",
            "unordered_set", "pair", "tuple", "optional", "variant", "any", "function", "shared_ptr", "unique_ptr",
    
            // Уникальные для Python
            "None", "assert", "def", "del", "elif", "except", "nonlocal", "abs", "all", "any", "ascii", "bin",
            "bytearray", "bytes", "callable", "chr", "classmethod", "compile", "complex", "delattr", "dict",
            "dir", "divmod", "enumerate", "eval", "exec", "filter", "format", "frozenset", "getattr", "globals",
            "hasattr", "hash", "help", "hex", "id", "input", "isinstance", "issubclass", "iter", "len", "list",
            "locals", "map", "max", "memoryview", "min", "next", "object", "oct", "open", "ord", "pow", "print",
            "property", "range", "repr", "reversed", "round", "set", "setattr", "slice", "sorted", "staticmethod",
            "str", "sum", "super", "type", "vars", "zip", "__init__", "__name__", "__main__", "__file__", "__doc__",
            "__annotations__", "__dict__", "__slots__", "__module__", "__enter__", "__exit__", "__iter__", "__next__",
            "__getitem__", "__setitem__", "__delitem__", "__len__", "__contains__", "__call__", "__add__", "__sub__",
            "__mul__", "__truediv__", "__eq__", "__ne__", "__lt__", "__le__", "__gt__", "__ge__", "__str__", "__repr__",
            "__bool__", "os", "sys", "math", "random", "datetime", "json", "re", "collections", "itertools", "functools",
            "typing", "numpy", "pandas", "matplotlib", "tensorflow", "pytorch", "flask", "django", "asyncio", "threading",
            "multiprocessing", "argparse", "logging", "unittest", "pytest", "pathlib", "socket", "ssl", "hashlib", "csv", "sqlite3"};
        public List<char> _FinalChars = new List<char>();
        
        public UnknownLanguageRead(string way) : base(way)
        {
            foreach (string word in Allwords)
            {
                if (words.Contains(word))
                {
                    foreach (char ch in word) { _FinalChars.Add(ch); }
                }
            }
        }
        public override List<char> GetLanguageSpecificChars() => _FinalChars;

        public override int CountLanguageSpecificConstructs()
        {
            int csCount = 0, cppCount = 0, pyCount = 0;

            foreach (string word in words)
            {
                // C# конструкции
                if (word.Contains("@") || word.Contains("$")) csCount++;                    // строковые литералы (@)
                if (word.Contains("=>")) csCount++;                   // лямбда-стрелка
                if (word.Contains("?") || word.Contains("?[")) csCount++; // null-conditional
                if (word.Contains("get;") || word.Contains("set;")) csCount++; // свойства
                if (word.Contains("using") && word.StartsWith("using")) csCount++; // подключение библиотек
                if (word.Contains("namespace")) csCount++;           // пространства имен
                if (word.Contains("var ")) csCount++;                 // неявная типизация
                if (word.Contains("async") || word.Contains("await")) csCount++; // асинхронность
                if (word.Contains("$")) csCount++;                    // интерполированные строки
                if (word.Contains("nameof")) csCount++;              // оператор nameof
                if (word.Contains("record")) csCount++;              // records
                if (word.Contains("init;")) csCount++;                // init-свойства

                // C++ конструкции
                if (word.StartsWith("#")) cppCount++;                 // препроцессор
                if (word.Contains("::")) cppCount++;                  // scope resolution
                if (word.Contains("<") && word.Contains(">") &&
                    !word.Contains("<<")) cppCount++;                 // шаблоны (исключаем <<)
                if (word.Contains("#include") || word.StartsWith("#include")) cppCount++; // подключение библиотек
                if (word.Contains("->")) cppCount++;                  // указатель на член
                if (word.Contains("std::")) cppCount++;               // пространство std
                if (word.Contains("&") && word.Length == 1) cppCount++; // ссылки
                if (word.Contains("template")) cppCount++;           // шаблоны
                if (word.Contains("constexpr")) cppCount++;          // constexpr
                if (word.Contains("noexcept")) cppCount++;            // noexcept
                if (word.Contains("[[") && word.Contains("]]")) cppCount++; // атрибуты
                if (word.Contains("dynamic_cast<") ||
                    word.Contains("static_cast<") ||
                    word.Contains("reinterpret_cast<")) cppCount++;   // C++-style касты
                if (word.Contains("nullptr")) cppCount++;             // nullptr

                // Python конструкции
                if (word.StartsWith("__") && word.EndsWith("__")) pyCount++; // magic methods
                if (word.Contains("lambda ")) pyCount++;             // lambda
                if (word.Contains("self.") || word.Contains("cls.")) pyCount++; // self/cls
                if (word.Contains("def") || word.StartsWith("def")) pyCount++; // функции
                if (word.Contains("import") || word.StartsWith("import")) pyCount++;
                if (word.Contains("from") || word.StartsWith("from")) pyCount++;
                if (word.Contains("[") && word.Contains("]") &&
                    word.Contains(":")) pyCount++;                   // срезы
                if (word == "True" || word == "False" || word == "None") pyCount++;
                if (word.Contains("elif")) pyCount++;              // elif
                if (word.Contains("yield")) pyCount++;             // yield
                if (word.Contains("#")) pyCount++;                   // комментарии
                if (word.Contains("with")) pyCount++;              // context manager
                if (word.Contains("@")) pyCount++;                   // декораторы
                if (word.Contains("except")) pyCount++;             // except
                if (word.Contains("finally:")) pyCount++;           // finally
                if (word.Contains("nonlocal")) pyCount++;          // nonlocal
                if (word.Contains("__init__")) pyCount++;            // конструктор
            }

            Console.WriteLine($"cs: {csCount},  cpp: {cppCount},  py: {pyCount}");
           
            // Определяем доминирующий язык по конструкциям
            if (pyCount > csCount && pyCount > cppCount) return -2;    // Python
            if (csCount > cppCount && csCount > pyCount) return 2;     // C#
            if (cppCount > csCount && cppCount > pyCount) return -1;   // C++

            // Если нет явного лидера, возвращаем разницу между C# и C++
            return csCount - cppCount;
        }
    }
}

using System;
using System.IO;

namespace inheritance_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder _pathfinder01 = new Pathfinder(new FileLogger("pathfinder01.txt", "Hello iJunior"));
            _pathfinder01.Find();

            Pathfinder _pathfinder02 = new Pathfinder(new ConsoleLogger("Hello iJunior"));
            _pathfinder02.Find();
            
            Pathfinder _pathfinder03 = new Pathfinder(new SecureFileLogger("pathfinder03.txt", "Hello friday iJunior"));
            _pathfinder03.Find();
            
            Pathfinder _pathfinder04 = new Pathfinder(new SecureConsoleLogger("Hello friday iJunior"));
            _pathfinder04.Find();
            
            Pathfinder _pathfinder05 = new Pathfinder(new MultipleLogger("pathfinder05.txt", "Hello iJunior from multiple"));
            _pathfinder05.Find();
        }
    }

    interface ILogger
    {
        void WriteLog();
    }

    class FileLogger : ILogger
    {
        private string _path;
        private string _text;

        public FileLogger(string path, string text)
        {
            _path = path;
            _text = text;
        }
        
        public void WriteLog()
        {
            using (StreamWriter writer = new StreamWriter(_path, false))
            {
                writer.Write(_text);
            }
        }
    }

    class ConsoleLogger : ILogger
    {
        private string _text;

        public ConsoleLogger(string text)
        {
            _text = text;
        }
        
        public void WriteLog()
        {
            Console.WriteLine(_text);
        }
    }
    
    class SecureFileLogger : ILogger
    {
        private string _path;
        private string _text;

        public SecureFileLogger(string path, string text)
        {
            _path = path;
            _text = text;
        }
        
        public void WriteLog()
        {
            if (DateTime.Now.DayOfWeek != DayOfWeek.Friday) 
                return;
            
            using (StreamWriter writer = new StreamWriter(_path, false))
                writer.Write(_text);
        }
    }

    class SecureConsoleLogger : ILogger
    {
        private string _text;

        public SecureConsoleLogger(string text)
        {
            _text = text;
        }
        
        public void WriteLog()
        {
            if (DateTime.Now.DayOfWeek != DayOfWeek.Friday) 
                return;
            
            Console.WriteLine(_text);
        }
    }

    class MultipleLogger : ILogger
    {
        private string _path;
        private string _text;

        public MultipleLogger(string path, string text)
        {
            _path = path;
            _text = text;
        }
        
        public void WriteLog()
        {
            Console.WriteLine(_text);
            
            if (DateTime.Now.DayOfWeek != DayOfWeek.Friday) 
                return;
            
            using (StreamWriter writer = new StreamWriter(_path, false))
                writer.Write(_text);
        }
    }

    class Pathfinder
    {
        private ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }

        public void Find()
        {
            Console.WriteLine($"Context: write something using {_logger}");
            _logger.WriteLog();
        }
    }
}
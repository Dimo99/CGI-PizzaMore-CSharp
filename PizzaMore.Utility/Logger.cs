using System.IO;

namespace PizzaMore.Utility
{
    public static class Logger
    {
        private const string path="../htdocs/log.txt";
        public static void Log(string message)
        {
            File.AppendAllText(path,'\n'+message);
        }
    }
}

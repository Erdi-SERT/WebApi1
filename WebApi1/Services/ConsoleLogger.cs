using System;

namespace WebApi1.Services
{
    public class ConsoleLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[Console Logger]"+message);
        }
    }
}

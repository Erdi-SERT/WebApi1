using System;
namespace WebApi1.Services
{
    public class DbLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[DBlogger]"+message);
        }
    }
}

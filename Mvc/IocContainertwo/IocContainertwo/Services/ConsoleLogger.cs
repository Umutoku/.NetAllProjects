using System;

namespace IocContainertwo.Services
{
    public class ConsoleLogger
    {
        public void Info(string message)
        {
            Console.WriteLine(message);
        }
    }
}

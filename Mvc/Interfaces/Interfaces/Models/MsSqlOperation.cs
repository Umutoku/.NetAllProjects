using System;

namespace Interfaces.Models
{
    public class MsSqlOperation :Test, ISqlOperation,ITestInterface
    {
        public void Connection()
        {
            Console.WriteLine("MsSql için bağlantı sağlandı.");
        }

        public bool Insert(Products product)
        {
            return true;
        }
    }

    public class MySqlOperation : ISqlOperation
    {
        public void Connection()
        {
            Console.WriteLine("MySql bağlantısı sağlandı.");
        }

        public bool Insert(Products product)
        {
            return true;
        }
    }

    public interface ISqlOperation
    {
        void Connection();

        bool Insert(Products product);
        
    }

    public interface ITestInterface
    {

    }

    public class Test
    {

    }

}

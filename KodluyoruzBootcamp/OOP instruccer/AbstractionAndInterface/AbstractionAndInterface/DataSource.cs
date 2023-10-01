using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionAndInterface
{
    public abstract class DataSource
    {
        public abstract void SaveData(string data);
        public abstract void GetData(string data);

        public abstract void Open();

        public bool IsConnected { get; set; }
        public void GetInfo()
        {
            Console.WriteLine("Bağlantı bilgileri");
        }

    }

    public class SqlDataSource : DataSource
    {
        public override void GetData(string data)
        {
            Console.WriteLine("Sqlden veri okunuyor");
        }

        public override void Open()
        {
            Console.WriteLine("Sqlden bağlantısı açılıyor");
        }

        public override void SaveData(string data)
        {
            Console.WriteLine("Sqlden veri yazılıyor");
        }
    }

    public class XmlDataSource : DataSource
    {
        public override void GetData(string data)
        {
            Console.WriteLine("xmlden veri okunuyor");
        }

        public override void Open()
        {
            Console.WriteLine("xmlden veri açılıyor");
        }

        public override void SaveData(string data)
        {
            Console.WriteLine("xmlden veri yazılıyor");
        }
    }
}

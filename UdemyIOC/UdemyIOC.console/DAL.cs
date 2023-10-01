using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyIOC.console
{
    public class DAL:IDAL
    {
        public List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product{ID=1,Name="Kalem",Price=100,Stock=200},
                new Product{ID=2,Name="Kağıt",Price=100,Stock=200},
                new Product{ID=3,Name="ler",Price=100,Stock=200},
                new Product{ID=4,Name="Kalem",Price=100,Stock=200},
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.App.ISPGoodAndBad
{
    //Class library read Impl
    //Class library Create/Update/delete



    public class ReadProductRepository : IReadRepository
    {
        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetList()
        {
            throw new NotImplementedException();
        }
    }

    public class WriteProductRepository : IWriteRepository
    {
        public Product Create(Product p)
        {
            throw new NotImplementedException();
        }

        public Product Delete(Product p)
        {
            throw new NotImplementedException();
        }

        public Product Update(Product p)
        {
            throw new NotImplementedException();
        }
    }


    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
    }
    public interface IWriteRepository
    {
        Product Create(Product p);

        Product Update(Product p);

        Product Delete(Product p);
    }

    public interface IReadRepository
    {
        List<Product> GetList();
        Product GetById(int id);
    }


}

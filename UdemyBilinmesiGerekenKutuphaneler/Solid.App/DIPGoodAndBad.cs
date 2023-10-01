using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.App.DIPGoodAndBad
{
    public class ProductService
    {
        private readonly IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }
        public List<string> GetAll()
        {
            return _repository.GetAll();
        }
    }
    public class ProductRepositoryFromSqlServer:IRepository
    {
        public List<string> GetAll()
        { return new List<string>() { "sql Kalem1", " sql kalem2" }; }
    }

    public interface IRepository
    {
        List<string> GetAll();
    }

    public class ProductRepositoryFromOracleServer : IRepository
    {
        public List<string> GetAll()
        { return new List<string>() { "oracle Kalem1", " oracle kalem2" }; }
    }
}

using AzureFuncWithDependencyInj.DAL;
using AzureFuncWithDependencyInj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFuncWithDependencyInj.Service
{
    public class ProductService : IProductService
    {

        private readonly IFakeProductDB _fakeProductDB;

        public ProductService(IFakeProductDB fakeProductDB) 
        {
            _fakeProductDB = fakeProductDB;
        }
        
        public Product CreateProduct(string productname)
        {
            var products = _fakeProductDB.CreateProduct(productname);
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = _fakeProductDB.GetProductById(id);
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            IEnumerable<Product> product = _fakeProductDB.GetProducts();
            return product;
        }
    }
}

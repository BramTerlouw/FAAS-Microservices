using AzureFuncWithDependencyInj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFuncWithDependencyInj.DAL
{
    public interface IFakeProductDB
    {
        Product CreateProduct(string productname);
        Product GetProductById(int id);
        IEnumerable<Product> GetProducts();
    }
}

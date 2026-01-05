using FunctionApp.Models;
using System.Collections.Generic;


namespace FunctionApp.Services
{
    public interface IProductService
    {
        public Product CreateProduct(Product product);
        public IEnumerable<Product> GetAllProducts();
        public Product GetProductById(int id);
        public Product UpdateProduct(int id, Product product);
        public Product DeleteProduct(int id);
    }
}
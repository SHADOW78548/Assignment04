using FunctionApp.Data;
using FunctionApp.Models;
using FunctionApp.Services;
using System.Collections.Generic;
using System.Linq;



namespace FunctionApp.Services
{
    public class ProductServiceImpl : IProductService
    {
        private readonly FunctionAppDbContext _context;
       public ProductServiceImpl(FunctionAppDbContext context)
        {
            _context = context;
        }

        public Product CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product UpdateProduct(int id, Product product)
        {
            var existingProduct = _context.Products.Find(id);
            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.ProductName = product.ProductName;
            existingProduct.ProductPrice = product.ProductPrice;
            _context.SaveChanges();
            return existingProduct;
        }

        public Product DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return null;
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return product;
        }
    }
    
}
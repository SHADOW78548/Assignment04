using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FunctionApp.Services;
using FunctionApp.Models;

namespace FunctionApp.Controllers
{
    public class ProductControllers
    {
        private readonly ILogger<ProductControllers> _logger;
        private readonly IProductService _productService;

        public ProductControllers(ILogger<ProductControllers> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [FunctionName("CreateProduct")]
        public async Task<IActionResult> CreateProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products/add")] HttpRequest req)
        {
            var product = await req.ReadFromJsonAsync<Product>();
            var createdProduct = _productService.CreateProduct(product!);
            _logger.LogInformation("Product created successfully.");
            return new OkObjectResult(createdProduct);
        }

        [FunctionName("GetAllProducts")]
        public IActionResult GetAllProducts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest req)
        {
            var products = _productService.GetAllProducts();
            _logger.LogInformation("All products retrieved successfully.");
            return new OkObjectResult(products);
        }

        [FunctionName("GetProductById")]
        public IActionResult GetProductById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/{id}")] HttpRequest req, int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return new NotFoundResult();
            }
            _logger.LogInformation("Product retrieved successfully.");
            return new OkObjectResult(product);
        }

        [FunctionName("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "products/{id}")] HttpRequest req, int id)
        {
            var product = await req.ReadFromJsonAsync<Product>();
            var updatedProduct = _productService.UpdateProduct(id, product!);
            if (updatedProduct == null)
            {
                return new NotFoundResult();
            }
            _logger.LogInformation("Product updated successfully.");
            return new OkObjectResult(updatedProduct);
        }

        [FunctionName("DeleteProduct")]
        public IActionResult DeleteProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "products/{id}")] HttpRequest req, int id)
        {
            var deletedProduct = _productService.DeleteProduct(id);
            if (deletedProduct == null)
            {
                return new NotFoundResult();
            }
            _logger.LogInformation("Product deleted successfully.");
            return new OkObjectResult(deletedProduct);
        }
    }
}

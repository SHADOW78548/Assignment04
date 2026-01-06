using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
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

        [Function("CreateProduct")]
        public async Task<HttpResponseData> CreateProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products/add")] HttpRequestData req)
        {
            var product = await req.ReadFromJsonAsync<Product>();
            var createdProduct = _productService.CreateProduct(product!);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(createdProduct);
            _logger.LogInformation("Product created successfully.");
            return response;
        }

        [Function("GetAllProducts")]
        public async Task<HttpResponseData> GetAllProducts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequestData req)
        {
            var products = _productService.GetAllProducts();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(products);
            _logger.LogInformation("All products retrieved successfully.");
            return response;
        }

        [Function("GetProductById")]
        public async Task<HttpResponseData> GetProductById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/{id}")] HttpRequestData req, int id)
        {
            var product = _productService.GetProductById(id);
            var response = req.CreateResponse(product == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);

            if (product == null)
            {
                await response.WriteAsJsonAsync(new { message = "Product not found" });
            }
            else
            {
                await response.WriteAsJsonAsync(product);
            }

            _logger.LogInformation("Product retrieved successfully.");
            return response;
        }

        [Function("UpdateProduct")]
        public async Task<HttpResponseData> UpdateProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "products/{id}")] HttpRequestData req, int id)
        {
            var product = await req.ReadFromJsonAsync<Product>();
            var updatedProduct = _productService.UpdateProduct(id, product!);

            var response = req.CreateResponse(updatedProduct == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);

            if (updatedProduct == null)
            {
                await response.WriteAsJsonAsync(new { message = "Product not found" });
            }
            else
            {
                await response.WriteAsJsonAsync(updatedProduct);
            }

            _logger.LogInformation("Product updated successfully.");
            return response;
        }

        [Function("DeleteProduct")]
        public async Task<HttpResponseData> DeleteProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "products/{id}")] HttpRequestData req, int id)
        {
            var deletedProduct = _productService.DeleteProduct(id);
            var response = req.CreateResponse(deletedProduct == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);

            if (deletedProduct == null)
            {
                await response.WriteAsJsonAsync(new { message = "Product not found" });
            }
            else
            {
                await response.WriteAsJsonAsync(deletedProduct);
            }

            _logger.LogInformation("Product deleted successfully.");
            return response;
        }
    }
}

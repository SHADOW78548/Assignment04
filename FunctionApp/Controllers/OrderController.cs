using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using FunctionApp.Services;
using FunctionApp.Models;

namespace FunctionApp.Controller
{
    public class OrderController
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Function("CreateOrder")]
        public async Task<HttpResponseData> CreateOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "orders/add")] HttpRequestData req)
        {
            var order = await req.ReadFromJsonAsync<Order>();
            var createdOrder = _orderService.CreateOrder(order!);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(createdOrder);
            _logger.LogInformation("Order created successfully.");
            return response;
        }

        [Function("GetAllOrders")]
        public async Task<HttpResponseData> GetAllOrders(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "orders")] HttpRequestData req)
        {
            var orders = _orderService.GetAllOrders();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(orders);
            _logger.LogInformation("All orders retrieved successfully.");
            return response;
        }

        [Function("GetOrderById")]
        public async Task<HttpResponseData> GetOrderById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "orders/{id}")] HttpRequestData req, int id)
        {
            var order = _orderService.GetOrderById(id);
            var response = req.CreateResponse(order == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);

            if (order == null)
            {
                await response.WriteAsJsonAsync(new { message = "Order not found" });
            }
            else
            {
                await response.WriteAsJsonAsync(order);
            }

            _logger.LogInformation("Order retrieved successfully.");
            return response;
        }

        [Function("UpdateOrder")]
        public async Task<HttpResponseData> UpdateOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "orders/{id}")] HttpRequestData req, int id)
        {
            var order = await req.ReadFromJsonAsync<Order>();
            var updatedOrder = _orderService.UpdateOrder(id, order!);

            var response = req.CreateResponse(updatedOrder == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);

            if (updatedOrder == null)
            {
                await response.WriteAsJsonAsync(new { message = "Order not found" });
            }
            else
            {
                await response.WriteAsJsonAsync(updatedOrder);
            }

            _logger.LogInformation("Order updated successfully.");
            return response;
        }

        [Function("DeleteOrder")]
        public async Task<HttpResponseData> DeleteOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "orders/{id}")] HttpRequestData req, int id)
        {
            var deletedOrder = _orderService.DeleteOrder(id);
            var response = req.CreateResponse(deletedOrder == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);

            if (deletedOrder == null)
            {
                await response.WriteAsJsonAsync(new { message = "Order not found" });
            }
            else
            {
                await response.WriteAsJsonAsync(deletedOrder);
            }

            _logger.LogInformation("Order deleted successfully.");
            return response;
        }
    }
}

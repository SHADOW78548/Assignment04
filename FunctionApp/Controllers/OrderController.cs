using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
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

        [FunctionName("CreateOrder")]
        public async Task<IActionResult> CreateOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "orders/add")] HttpRequest req)
        {
            var order = await req.ReadFromJsonAsync<Order>();
            var createdOrder = _orderService.CreateOrder(order!);
            _logger.LogInformation("Order created successfully.");
            return new OkObjectResult(createdOrder);
        }

        [FunctionName("GetAllOrders")]
        public IActionResult GetAllOrders(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "orders")] HttpRequest req)
        {
            var orders = _orderService.GetAllOrders();
            _logger.LogInformation("All orders retrieved successfully.");
            return new OkObjectResult(orders);
        }

        [FunctionName("GetOrderById")]
        public IActionResult GetOrderById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "orders/{id}")] HttpRequest req, int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return new NotFoundResult();

            _logger.LogInformation("Order retrieved successfully.");
            return new OkObjectResult(order);
        }

        [FunctionName("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "orders/{id}")] HttpRequest req, int id)
        {
            var order = await req.ReadFromJsonAsync<Order>();
            var updatedOrder = _orderService.UpdateOrder(id, order!);
            if (updatedOrder == null) return new NotFoundResult();

            _logger.LogInformation("Order updated successfully.");
            return new OkObjectResult(updatedOrder);
        }

        [FunctionName("DeleteOrder")]
        public IActionResult DeleteOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "orders/{id}")] HttpRequest req, int id)
        {
            var deletedOrder = _orderService.DeleteOrder(id);
            if (deletedOrder == null) return new NotFoundResult();

            _logger.LogInformation("Order deleted successfully.");
            return new OkObjectResult(deletedOrder);
        }
    }
}

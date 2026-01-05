using FunctionApp.Models;
using System.Collections.Generic;

namespace FunctionApp.Services
{
    public interface IOrderService
    {
        public Order CreateOrder(Order order);
        public IEnumerable<Order> GetAllOrders();
        public Order GetOrderById(int id);
        public Order UpdateOrder(int id, Order order);
        public Order DeleteOrder(int id);
    }
    
}
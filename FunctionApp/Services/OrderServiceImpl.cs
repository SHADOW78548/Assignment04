using FunctionApp.Data; 
using FunctionApp.Models;
using System.Collections.Generic;
using System.Linq;
using FunctionApp.Services;


namespace FunctionApp.Services
{
    public class OrderServiceImpl : IOrderService
    {
        private readonly FunctionAppDbContext _dbContext;

        public OrderServiceImpl(FunctionAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Order CreateOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return order;
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return _dbContext.Orders.ToList();
        }
        public Order GetOrderById(int id)
        {
            return _dbContext.Orders.Find(id);
        }
        public Order UpdateOrder(int id, Order order)
        {
            var existingOrder = _dbContext.Orders.Find(id);
            if (existingOrder == null)
            {
                return null;
            }
            existingOrder.UserId = order.UserId;
            existingOrder.ProductId = order.ProductId;
            _dbContext.SaveChanges();
            return existingOrder;
        }
        public Order DeleteOrder(int id)
        {
            var order = _dbContext.Orders.Find(id);
            if (order == null)
            {
                return null;
            }
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
            return order;
        }

    }
}
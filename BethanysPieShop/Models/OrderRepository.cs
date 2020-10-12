using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(AppDbContext appDbContext,ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            foreach (var item in _shoppingCart.ShoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    PieId = item.Pie.PieId,
                    Amount = item.Amount,
                    Price = item.Pie.Price,
                };
                _appDbContext.OrderDetails.Add(orderDetail);
            }
            _appDbContext.Orders.Add(order);

            _appDbContext.SaveChanges();
        }
    }
}

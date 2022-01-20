using Keyhanatr.Core.Interfaces.Users;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Order;
using Keyhanatr.Data.Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Keyhanatr.Controllers
{
    public class ShoppingCartController : Controller
    {
        private KeyhanatrContext _context;
        public ShoppingCartController(KeyhanatrContext context)
        {
            _context = context;
        }

        [Authorize]
        public void AddToCart(int productId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = _context.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
            if (order == null)
            {
                order = new Order()
                {
                    CreateDate = DateTime.Now,
                    IsFinaly = false,
                    UserId = userId
                };
                _context.Add(order);
                _context.SaveChanges();
            }

            var detail =
                _context.OrderDetails.FirstOrDefault(d => d.OrderId == order.OrderId && d.ProductId == productId);
            if (detail != null)
            {
                detail.Count += 1;
                _context.Update(detail);
            }
            else
            {
                _context.OrderDetails.Add(new OrderDetail()
                {
                    ProductId = productId,
                    Count = 1,
                    Price = _context.Products.Find(productId).Price,
                    OrderId = order.OrderId,
                });
            }
            _context.SaveChanges();
        }

        public IActionResult ShowCart()
        {
            List<OrderDetail> list = new List<OrderDetail>();

            if (User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var order = _context.Orders.FirstOrDefault(o => !o.IsFinaly && o.UserId == userId);
                if (order != null)
                {
                    list.AddRange(_context.OrderDetails
                        .Where(d => d.OrderId == order.OrderId).Include(p => p.Product).ToList());
                }
            }
            return PartialView(list);
        }
    }
}

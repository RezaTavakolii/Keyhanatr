
using Keyhanatr.Core.Interfaces.Orders;
using Keyhanatr.Core.Interfaces.Users;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Order;
using Keyhanatr.Data.Domain.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Services.Orders
{
    public class ShopingCardService : IShopingCardService
    {
        private KeyhanatrContext _context;
        private IUserService _userService;
        public ShopingCardService(KeyhanatrContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        
       
    }
}
using Keyhanatr.Core.Interfaces.Users;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.User;
using Keyhanatr.Data.ViewModel.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Keyhanatr.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService _userService;
        private KeyhanatrContext _Context;
        public HomeController(IUserService userService, KeyhanatrContext context)
        {
            _userService = userService;
            _Context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        #region Change Password
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel change)
        {
            if (!ModelState.IsValid)
                return View(change);

            int currentUser = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!_userService.IsUserPassword(currentUser, change.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نمی باشد");
                return View(change);
            }
            _userService.ChangeUserPassword(currentUser, change.Password);
            ViewBag.isOk = true;
            return View();
        }
        #endregion

        #region LogOut
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Login");
        }
        #endregion

       
    }
}

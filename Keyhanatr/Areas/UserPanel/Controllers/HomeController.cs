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

        

        public IActionResult Order()
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

        //#region Address
        //public IActionResult Address(int id)
        //{
        //    ViewBag.Address = _Context.Addresses.Where(u => u.UserId == id).ToList();
        //    return View(new Address()
        //    {
        //        UserId = id
        //    });
        //}

        //[HttpPost]
        //public IActionResult Address(Address address)
        //{
        //    Address Adrs = new Address()
        //    {
        //        UserId = address.UserId,
        //        Name = address.Name,
        //        Family = address.Family,
        //        Company = address.Company,
        //        Ostan = address.Ostan,
        //        Shahr = address.Shahr,
        //        Adress = address.Adress,
        //        CodePosti = address.CodePosti,
        //        Mobile = address.Mobile,
        //    };
        //    _Context.Addresses.Add(Adrs);
        //    _Context.SaveChanges();
        //    ViewBag.Address = _Context.Addresses.Where(g => g.UserId == address.UserId).ToList();
        //    return View();
        //}

        //public void DeleteAddress(int id)
        //{
        //    var adress = _Context.Addresses.Find(id);
        //    _Context.Addresses.Remove(adress);
        //    _Context.SaveChanges();
        //}
        //#endregion
    }
}

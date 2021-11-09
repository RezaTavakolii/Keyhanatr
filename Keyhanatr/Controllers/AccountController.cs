using Keyhanatr.Core.Interfaces.Users;
using Keyhanatr.Data.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Keyhanatr.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #region Register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            if (_userService.IsExistMobile(register.Mobile))
            {
                ModelState.AddModelError("Mobile", "شماره همراه وارد شده تکراری است");
                return View(register);
            }
            _userService.RegisterUser(register);
            return View("SuccessRegister", register);
        }
        #endregion

        #region Active User

        public IActionResult ActiveUser(string id)
        {
            var result = _userService.ActiveUser(id);
            return View(result);
        }

        #endregion

        //#region Login & Logout

        //[Route("Login")]
        //public IActionResult Login(string ReturnUrl = "/")
        //{
        //    ViewBag.ret = ReturnUrl;
        //    return View();
        //}

        //[Route("Login")]
        //[HttpPost]
        //public IActionResult Login(LoginViewModel login, string ReturnUrl)
        //{
        //    if (!ModelState.IsValid)
        //        return View(login);

        //    var user = _userService.LoginUser(login);

        //    if (user == null)
        //    {
        //        ModelState.AddModelError("Email", "اطلاعات صحیح نمی باشد");
        //        return View(login);
        //    }
        //    if (user.IsActive)
        //    {
        //        var claims = new List<Claim>()
        //        {
        //            new Claim(ClaimTypes.Name,user.UserName),
        //            new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
        //            new Claim("RoleId",user.RoleId.ToString()),
        //        };
        //        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //        var principal = new ClaimsPrincipal(identity);
        //        var properties = new AuthenticationProperties()
        //        {
        //            IsPersistent = login.RememberMe
        //        };

        //        HttpContext.SignInAsync(principal, properties);
        //        return Redirect("/");
        //    }

        //    ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است");

        //    return View(login);
        //}

        //#endregion

        //#region ForgetPass
        //public IActionResult messageforpass(string id)
        //{
        //    return View();
        //}
        //public IActionResult ActivePass(string id)
        //{
        //    string email = _userService.GetEmailforpass(id);
        //    var result = _userService.ActiveUser(id);
        //    ViewBag.activecode = _userService.GetUserActiveCode(email);
        //    return View("ActivePass", result);
        //}

        //[Route("ForgetPass")]
        //public IActionResult ForgetPass(string activecode)
        //{
        //    ViewBag.activecode2 = activecode;
        //    return View();
        //}

        //[Route("ForgetPass")]
        //[HttpPost]
        //public IActionResult ForgetPass(ForgetPasswordViewModel forgetpass, string activecode)
        //{
        //    if (!_userService.ActiveUser(activecode))
        //    {
        //        return View();
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("error", "خطا");
        //        return View(forgetpass);
        //    }
        //    if (!_userService.IsExistEmail(forgetpass.Email))
        //    {
        //        ModelState.AddModelError("Email", "ایمیل نامعتبر می باشد");
        //        return View(forgetpass);
        //    }

        //    if (_userService.ForgetPassword(forgetpass) == true)
        //    {
        //        //forgetpass.Email = ViewBag.email;
        //        //forgetpass.ActiveCode = ViewBag.activecode;

        //        // _userServices.ForgetPassword(forgetpass);

        //        return View("SuccessChangePass", forgetpass);
        //    }
        //    return View("ActivePass");
        //}


        //[Route("GetEmail")]
        //public IActionResult GetEmail()
        //{
        //    return View();
        //}

        //[Route("GetEmail")]
        //[HttpPost]
        //public IActionResult GetEmail(EmailViewModel getemail)
        //{
        //    if (!ModelState.IsValid)
        //        return View(getemail);

        //    var user = _userService.GetEmail(getemail);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError("GetMail", "ایمیل یافت نشد");
        //        // return View(getemail);
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("GetMail", "ایمیل وارد شده اشتباه هست ");
        //    }
        //    if (user.IsActive)
        //    {
        //        if (_userService.SendLink(getemail))
        //        {
        //            _userService.GetEmail(getemail);

        //            ViewBag.email = user.Email;

        //            return View("messageforpass", getemail);
        //        }
        //        return View();
        //    }
        //    ModelState.AddModelError("GetMail", "ایمیل شما فعال نشده است");
        //    return View();
        //}
        //#endregion

        [Route("RecoveryPassSuccess")]
        public IActionResult RecoveryPassSuccess()
        {
            return View();
        }
    }
}


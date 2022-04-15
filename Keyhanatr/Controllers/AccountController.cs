using Keyhanatr.Core.Interfaces.Users;
using Keyhanatr.Core.Senders;
using Keyhanatr.Data.ViewModel.Account;
using Keyhanatr.Data.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
            return View("Active");
        }
        #endregion

        #region Active User
        public IActionResult Active()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Active(ActiveViewModel active)
        {
            if (ModelState.IsValid)
            {
                if (_userService.ActiveUser(active.ActiveCode))
                {
                    return RedirectToAction("ActiveUser");
                }
                else
                {
                    ModelState.AddModelError("Code", "کد فعالسازی صحیح نمی باشد");
                    return View(active);
                }
            }
            else
            {
                return View(active);
            }
        }

        public IActionResult ActiveUser(string id)
        {
            var result = _userService.ActiveUser(id);
            return View(result);
        }
        #endregion

        #region Login & Logout

        [Route("Login")]
        public IActionResult Login(string ReturnUrl = "/")
        {
            ViewBag.ret = ReturnUrl;
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel login, string ReturnUrl)
        {
            if (!ModelState.IsValid)
                return View(login);

            var user = _userService.LoginUser(login);

            if (user == null)
            {
                ModelState.AddModelError("Password",  "اطلاعات ورود صحیح نمی باشد");
                return View(login);
            }
            if (user.IsActive)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim("RoleId",user.RoleId.ToString()),
                   
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = login.RememberMe
                };

                HttpContext.SignInAsync(principal, properties);
                return Redirect("/");
            }

            ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است");

            return View(login);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Login");
        }
        #endregion

        #region ForgetPassword

        [Route("ResetPassword")]
        public IActionResult ResetPassword(string activecode)
        {
            return View();
        }

        [Route("ResetPassword")]
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasssword)
        {
            if (ModelState.IsValid)
            {
                if (_userService.ResetPassword(resetPasssword.ActiveCode, resetPasssword.Password))
                {
                    return RedirectToAction("RecoveryPassSuccess");
                }
                else
                {
                    ModelState.AddModelError("ActiveCode", "کد وارد شده صحیح نمی باشد");
                    return View(resetPasssword);
                }
            }
            else
            {
                return View("ActivePass");
            }
        }  

        [Route("GetMobile")]
        public IActionResult GetMobile()
        {
            return View();
        }

        [Route("GetMobile")]
        [HttpPost]
        public IActionResult GetMobile(MobileViewModel getMobile)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetMobile(getMobile.Mobile);
                if (user != null)
                {
                    MessageSender sms = new MessageSender();
                    sms.SendMessage(getMobile.Mobile, user.ActiveCode, "Verify");
                    return RedirectToAction("ResetPassword");
                }
                else
                {
                    ModelState.AddModelError("Mobile", "شماره وارد شده صحیح نمی باشد");
                    return View(getMobile);
                }
            }
            else
            {
                return View(getMobile);
            }
        }

        [Route("RecoveryPassSuccess")]
        public IActionResult RecoveryPassSuccess()
        {
            return View();
        }
        #endregion
    }
}


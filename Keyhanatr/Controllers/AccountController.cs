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
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
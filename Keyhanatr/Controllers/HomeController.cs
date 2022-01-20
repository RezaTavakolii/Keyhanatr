using Keyhanatr.Data.Context;
using Keyhanatr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr.Controllers
{
    public class HomeController : Controller
    {

        private readonly KeyhanatrContext _context;
        public HomeController(KeyhanatrContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Slider()
        {
            return View(_context.Sliders.Where(s => s.IsActive));
        }

        

    }
}

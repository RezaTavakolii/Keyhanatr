using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Core.Interfaces.Sliders;
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

        private ISliderServices _sliderServices;
        private IProductServices  _productServices;

        public HomeController(ISliderServices sliderServices, IProductServices productServices)
        {
            _sliderServices = sliderServices;
            _productServices = productServices;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}

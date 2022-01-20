using Keyhanatr.Core.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr.Controllers
{
    public class SearchController : Controller
    {
        private IProductServices _productService;
      
        public SearchController(IProductServices productService)
        {
            _productService = productService;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        
    }
}

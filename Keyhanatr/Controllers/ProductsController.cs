using Keyhanatr.Core.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr.Controllers
{
    public class ProductsController : Controller
    {
        private IProductServices _productService;
        private IProductColorServices _colorServices;
        public ProductsController(IProductServices productService, IProductColorServices colorServices)
        {
            _productService = productService;
            _colorServices = colorServices;
        }
        public IActionResult Index()
        {
            return View();
        }


        [Route("/Single/{id}")]
        public IActionResult ShowSingleProduct(int id)
        {
            var product = _productService.GetProductById(id);
            return View(product);
        }

        //id== productId
        public IActionResult _ShowComments(int id) {
            var comments = _productService.GetAllCommentsByProductId(id);
            return PartialView(comments);
        }

        //id is ColorId
        //it gives us the info of the clicked color on the SingleProduct page using the passed "id"
        public IActionResult _GetColorInfo(int id) {
            var color = _colorServices.GetColorByColorId(id);      
            return PartialView(color);
        }
    }
}

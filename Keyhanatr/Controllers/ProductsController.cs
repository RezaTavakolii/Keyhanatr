using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Data.Context;
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
        private KeyhanatrContext _context;
        public ProductsController(IProductServices productService, IProductColorServices colorServices, KeyhanatrContext context)
        {
            _productService = productService;
            _colorServices = colorServices;
            _context = context;
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
        public IActionResult _ShowComments(int id)
        {
            var comments = _productService.GetAllCommentsByProductId(id);
            return PartialView(comments);
        }

        //id is ColorId
        //it gives us the info of the clicked color on the SingleProduct page using the passed "id"
        public IActionResult _GetColorInfo(int id)
        {
            var color = _colorServices.GetColorByColorId(id);
            return PartialView(color);
        }

        [Route("NavGroup/{navGroupId}/{title}")]
        public IActionResult ShowProductByNavGroupId(int navGroupId, string title)
        {
            ViewBag.NavGroupName = title;
            return View(_context.Products.Where(p => p.ProductGroup.ProductNavGroupId == navGroupId));
            //return View(_productService.GetProductGroupById(productGroupId));
        }

        [Route("Group/{groupId}/{title}")]
        public IActionResult ShowProductByGroupId(int groupId, string title)
        {
            ViewBag.GroupName = title;
            return View(_context.Products.Where(p => p.ProductGroupId == groupId));
            //return View(_productService.GetProductGroupById(productGroupId));
        }

        [Route("SubGroup/{subgroupId}/{title}")]
        public IActionResult ShowProductBySubGroupId(int subgroupId, string title)
        {
            ViewBag.GroupName = title;
            return View(_context.Products.Where(p => p.ProductSubGroupId == subgroupId));
            //return View(_productService.GetProductGroupById(subgroupId));
        }

    }
}

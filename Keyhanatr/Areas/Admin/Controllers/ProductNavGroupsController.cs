using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Data.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductNavGroupsController : Controller
    {
        private IProductServices _productService;
        public ProductNavGroupsController(IProductServices productServices)
        {
            _productService = productServices;
        }
        public IActionResult Index()
        {
            return View(_productService.GetAllNavGroups());
        }


        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Create( ProductNavGroup navGroup)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _productServices.AddProductGroup(productGroup);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(productGroup);
        //}
    }
}

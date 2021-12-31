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

        //POST: Admin/ProductGroups/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductNavGroup navGroup)
        {
            if (ModelState.IsValid)
            {
                _productService.AddNavGroup(navGroup);
                return RedirectToAction(nameof(Index));
            }
            return View(navGroup);
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navGroup = _productService.GetNavGroupById(id.Value);
            if (navGroup == null)
            {
                return NotFound();
            }

            return View(navGroup);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navGroup = _productService.GetNavGroupById(id.Value);
            if (navGroup == null)
            {
                return NotFound();
            }
            return View(navGroup);
        }

        // POST: Admin/ProductGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(ProductNavGroup navGroup)
        {


            if (ModelState.IsValid)
            {

                _productService.EditNavGroup(navGroup);
                return RedirectToAction(nameof(Index));
            }
            return View(navGroup);
        }


        ////// GET: Admin/ProductGroups/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navGroup = _productService.GetNavGroupById(id.Value);

            if (navGroup == null)
            {
                return NotFound();
            }

            return View(navGroup);
        }

        //// POST: Admin/ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productService.DeleteNavGroupById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

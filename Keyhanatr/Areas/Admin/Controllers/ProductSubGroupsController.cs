using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Products;
using Keyhanatr.Core.Interfaces.Products;

namespace Keyhanatr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductSubGroupsController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductSubGroupsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        // GET: Admin/ProductSubGroups
        public  IActionResult Index()
        {
            return View(_productServices.GetAllProductSubGroups());
        }

        // GET: Admin/ProductSubGroups/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSubGroup = _productServices.GetProductSubGroupById(id.Value);
            if (productSubGroup == null)
            {
                return NotFound();
            }

            return View(productSubGroup);
        }

        // GET: Admin/ProductSubGroups/Create
        public IActionResult Create()
        {
            var groups = _productServices.GetGroupsListItem();
            ViewData["ProductGroupId"] = new SelectList(groups, "Value", "Text");
            return View();
        }

        // POST: Admin/ProductSubGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SubGroupId,ProductGroupId,SubGroupTitle")] ProductSubGroup productSubGroup)
        {
            if (ModelState.IsValid)
            {
                _productServices.AddProductSubGroup(productSubGroup);
                return RedirectToAction(nameof(Index));
            }
            var groups = _productServices.GetGroupsListItem();
            ViewData["GroupId"] = new SelectList(groups, "Value", "Text",productSubGroup.ProductGroupId);

            return View(productSubGroup);
        }

        //// GET: Admin/ProductSubGroups/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSubGroup = _productServices.GetProductSubGroupById(id.Value);
            if (productSubGroup == null)
            {
                return NotFound();
            }
            var groups = _productServices.GetGroupsListItem();
            ViewData["ProductGroupId"] = new SelectList(groups, "Value", "Text", productSubGroup.ProductGroupId);
            return View(productSubGroup);
        }

        //// POST: Admin/ProductSubGroups/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("SubGroupId,ProductGroupId,SubGroupTitle")] ProductSubGroup productSubGroup)
        {
        

            if (ModelState.IsValid)
            {
                _productServices.EditProductSubGroup(productSubGroup);
                return RedirectToAction(nameof(Index));
            }
            var groups = _productServices.GetGroupsListItem();
            ViewData["GroupId"] = new SelectList(groups, "Value", "Text", productSubGroup.ProductGroupId);
            return View(productSubGroup);
        }

        //// GET: Admin/ProductSubGroups/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSubGroup = _productServices.GetProductSubGroupById(id.Value);
            if (productSubGroup == null)
            {
                return NotFound();
            }

            return View(productSubGroup);
        }

        //// POST: Admin/ProductSubGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var productSubGroup = _productServices.GetProductSubGroupById(id);

            return RedirectToAction(nameof(Index));
        }

    }
}

﻿using System;
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
    public class ProductGroupsController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductGroupsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        // GET: Admin/ProductGroups
        public IActionResult Index()
        {
            return View(_productServices.GetAllProducts());
        }

        // GET: Admin/ProductGroups/Details/5
        //public  IActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var productGroup =  _context.ProductGroups
        //        .FirstOrDefaultAsync(m => m.GroupId == id);
        //    if (productGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(productGroup);
        //}

        // GET: Admin/ProductGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GroupId,GroupTitle")] ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                _productServices.AddProductGroup(productGroup);
                return RedirectToAction(nameof(Index));
            }
            return View(productGroup);
        }

        //GET: Admin/ProductGroups/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = _productServices.GetProductGroupById(id.Value);
            if (productGroup == null)
            {
                return NotFound();
            }
            return View(productGroup);
        }

        // POST: Admin/ProductGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("GroupId,GroupTitle")] ProductGroup productGroup)
        {
           

            if (ModelState.IsValid)
            {

                _productServices.EditProductGroup(productGroup);
                return RedirectToAction(nameof(Index));
            }
            return View(productGroup);
        }


        //// GET: Admin/ProductGroups/Delete/5
        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = _productServices.GetProductGroupById(id.Value);
                
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // POST: Admin/ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(int id)
        {
            var productGroup =  _productServices.GetProductGroupById(id);
            _productServices.DeleteProductGroup(productGroup);
            return RedirectToAction(nameof(Index));
        }
    }
}

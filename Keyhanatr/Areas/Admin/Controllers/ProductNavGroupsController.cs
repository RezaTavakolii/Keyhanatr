using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Data.Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
        public IActionResult Create([Bind("NavGroupId,NavTitle,BackColor,ImageName")] ProductNavGroup navGroup, IFormFile imgUp)
        {
            if (ModelState.IsValid)
            {
                navGroup.ImageName = "default.jpg";
                if (imgUp != null)
                {
                    navGroup.ImageName = Guid.NewGuid().ToString().Replace("-", "")
                                                + Path.GetExtension(imgUp.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/NavGroupImages",
                        navGroup.ImageName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        imgUp.CopyTo(stream);
                    }
                }
                _productService.AddNavGroup(navGroup);
                return RedirectToAction(nameof(Index));
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
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("NavGroupId,NavTitle,BackColor,ImageName")] ProductNavGroup navGroup, IFormFile imgUp)
        {

            if (id != navGroup.NavGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imgUp != null)
                    {
                        if (navGroup.ImageName != "default.jpg")
                        {
                            string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/NavGroupImages",
                                navGroup.ImageName);
                            if (System.IO.File.Exists(oldImagePath))
                                System.IO.File.Delete(oldImagePath);
                        }

                        navGroup.ImageName = Guid.NewGuid().ToString().Replace("-", "")
                                                + Path.GetExtension(imgUp.FileName);
                        string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/NavGroupImages",
                            navGroup.ImageName);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            imgUp.CopyTo(stream);
                        }
                    }
                    _productService.EditNavGroup(navGroup);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NaveGroupExists(navGroup.NavGroupId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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
        private bool NaveGroupExists(int id)
        {
            return _productService.NaveGroupExists(id);
        }
    }
}

using Keyhanatr.Core.Interfaces.Brands;
using Keyhanatr.Data.Domain.Brand;
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
    public class BrandController : Controller
    {
        private IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            return View( _brandService.GetAllBrands());
        }

        #region Details

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brand =  _brandService.GetBrandById(id.Value);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }
        #endregion

        #region Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandID,Title,ImageName")] Brand brand, IFormFile imgUp)
        {
            if (ModelState.IsValid)
            {
                brand.ImageName = "default.jpg";
                if (imgUp != null)
                {
                    brand.ImageName = Guid.NewGuid().ToString().Replace("-", "")
                                                + Path.GetExtension(imgUp.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BrandImages",
                        brand.ImageName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        imgUp.CopyTo(stream);
                    }
                }
                 _brandService.AddBrand(brand);
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }
        #endregion

        #region Edie

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brand =  _brandService.GetBrandById(id.Value);

            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandID,Title,ImageName")] Brand brand, IFormFile imgUp)
        {
            if (id != brand.BrandID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (imgUp != null)
                    {
                        if (brand.ImageName != "default.jpg")
                        {
                            string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BrandImages",
                                brand.ImageName);
                            if (System.IO.File.Exists(oldImagePath))
                                System.IO.File.Delete(oldImagePath);
                        }

                        brand.ImageName = Guid.NewGuid().ToString().Replace("-", "")
                                                + Path.GetExtension(imgUp.FileName);
                        string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BrandImages",
                            brand.ImageName);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            imgUp.CopyTo(stream);
                        }
                    }
                     _brandService.EditBrand(brand);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.BrandID))
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
            return View(brand);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brand =  _brandService.GetBrandById(id.Value);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             _brandService.DeleteBrand(id);
            return RedirectToAction(nameof(Index));
        }
        private bool BrandExists(int id)
        {
            return  _brandService.BrandExist(id);
        }
        #endregion
    }
}
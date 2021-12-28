using Keyhanatr.Core.Convertor;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Slider;

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

    public class SlidersController : Controller
    {
        private readonly KeyhanatrContext _context;
        public SlidersController(KeyhanatrContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var myKeyhanatrContext = _context.Sliders;
            return View(await myKeyhanatrContext.ToListAsync());
        }

        #region Details

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.SlideID == id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }
        #endregion

        #region Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SlideID,Title,Text,ImageName,IsActive")] Slider slider, IFormFile imgUp)
        {
            if (ModelState.IsValid)
            {
                slider.ImageName = "default.jpg";
                if (imgUp != null)
                {
                    slider.ImageName = Guid.NewGuid().ToString().Replace("-", "")
                                                + Path.GetExtension(imgUp.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SliderImages",
                        slider.ImageName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        imgUp.CopyTo(stream);
                    }
                }
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }
        #endregion

        #region Edie

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SlideID,Title,Text,ImageName,IsActive")] Slider slider, IFormFile imgUp)
        {
            if (id != slider.SlideID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (imgUp != null)
                    {
                        if (slider.ImageName != "default.jpg")
                        {
                            string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SliderImages",
                                slider.ImageName);
                            if (System.IO.File.Exists(oldImagePath))
                                System.IO.File.Delete(oldImagePath);
                        }
                        
                        slider.ImageName = Guid.NewGuid().ToString().Replace("-", "")
                                                + Path.GetExtension(imgUp.FileName);
                        string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SliderImages",
                            slider.ImageName);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            imgUp.CopyTo(stream);
                        }
                    }
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.SlideID))
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
            return View(slider);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var service = await _context.Sliders
                .FirstOrDefaultAsync(m => m.SlideID == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool SliderExists(int id)
        {
            return _context.Sliders.Any(e => e.SlideID == id);
        }
        #endregion
    }
}
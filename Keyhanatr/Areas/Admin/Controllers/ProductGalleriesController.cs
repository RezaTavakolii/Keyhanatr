using Keyhanatr.Core.Convertors;
using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Data.Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductGalleriesController : Controller
    {

        private IProductGallery _galleryService;
        public ProductGalleriesController(IProductGallery galleryService)
        {
            _galleryService = galleryService;
        }

        public IActionResult Index(int id)
        {
            ViewBag.ProductTitle = _galleryService.GetProductById(id).ProductTitle;
            var galeries = _galleryService.GetGalleryByProductId(id).ToList();
            return View(galeries);
        }


        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_galleryService.GetAllProducts(),
                "ProductId", "ProductTitle");
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductGallery gallery, IFormFile[] imgUp)
        {
            // If the if statement below is Exist, it will be called
            if (imgUp != null && imgUp.Any())
            {
                foreach (var img in imgUp)
                {
                    ProductGallery gal = new()
                    {
                        ProductId = gallery.ProductId,
                        ImageName = NameGenerator.GenerateUniqCode() +
                            Path.GetExtension(img.FileName)
                    };
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/Images/ProductImages", gal.ImageName);
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }
                    _galleryService.AddProductGallery(gal);
                }
                return Redirect("~/Admin/Products/Index");
            }
            //If we don't have Any Images, we will stay in current view
            else
            {
                ViewData["ProductId"] = new SelectList(_galleryService.GetAllProducts(),
                    "ProductId", "ProductTitle", gallery.ProductId);
                return View(gallery);
            }
        }

        public void DeleteGallery(int id)
        {
            var gallery = _galleryService.GetGalleryById(id);
            var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductImages",
                gallery.ImageName);
            if (System.IO.File.Exists(deletePath))
                System.IO.File.Delete(deletePath);

            _galleryService.DeleteProductGallery(gallery.GalleryId);
        }
    }
}

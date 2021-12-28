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
    public class ProductFeaturesController : Controller
    {
        private  IProductFeatureServices _productFeature;
        public ProductFeaturesController(IProductFeatureServices productFeature)
        {
            _productFeature = productFeature;
        }
        public IActionResult Index()
        {
            List<ProductFeature> features = _productFeature.GetAllFeatures().ToList();
            return View(features);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductFeature productFeature)
        {
            if (!ModelState.IsValid)
            {
                return View(productFeature);
            }
            _productFeature.AddFeature(productFeature);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var feature = _productFeature.GetFeatureById(id);
            return View(feature);
        }
        [HttpPost]
        public IActionResult Edit(ProductFeature productFeature)
        {
            if (!ModelState.IsValid)
            {
                return View(productFeature);
            }
            _productFeature.EditFeature(productFeature);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult delete(int id)
        {
            var feature = _productFeature.GetFeatureById(id);
            return View(feature);
        }
        public IActionResult ConfirmedDelete(int id)
        {
            _productFeature.DeleteFeature(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

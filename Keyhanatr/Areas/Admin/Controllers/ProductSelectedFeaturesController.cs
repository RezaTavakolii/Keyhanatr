using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Data.Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductSelectedFeaturesController : Controller
    {
        private IProductSelectedFeature _selectedFeature;
        public ProductSelectedFeaturesController(IProductSelectedFeature selectedFeature)
        {
            _selectedFeature = selectedFeature;
        }


        public IActionResult Index(int? id=null)
        {
            ViewBag.ProductName = _selectedFeature.GetProductById(id.Value).ProductTitle;
            var selectedFeatures = _selectedFeature.GetAllSelectedFeatures().Where(s => s.ProductId == id.Value).ToList();
            return View(selectedFeatures);
        }

        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_selectedFeature.GetAllProducts().ToList(), "ProductId",
                "ProductTitle");
            ViewData["FeatureId"] = new SelectList(_selectedFeature.GetAllFeatures().ToList(), "FeatureId",
               "FeatureTitle");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductSelectedFeature selectedFeature)
        {

            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = new SelectList(_selectedFeature.GetAllProducts().ToList(), "ProductId",
                    "ProductTitle", selectedFeature.ProductId);
                ViewData["FeatureId"] = new SelectList(_selectedFeature.GetAllFeatures().ToList(), "FeatureId",
                   "FeatureTitle", selectedFeature.FeatureId);
                return View(selectedFeature);
            }
            _selectedFeature.AddSelectedFeature(selectedFeature);

            return RedirectToAction(nameof(Index), new { id = selectedFeature.ProductId });
        }

        public IActionResult Edit(int id)
        {
            var selectedFeature = _selectedFeature.GetSelectedFeatureById(id);
            ViewData["ProductId"] = new SelectList(_selectedFeature.GetAllProducts().ToList(), "ProductId",
                   "ProductTitle", selectedFeature.ProductId);
            ViewData["FeatureId"] = new SelectList(_selectedFeature.GetAllFeatures().ToList(), "FeatureId",
               "FeatureTitle", selectedFeature.FeatureId);

            return View(selectedFeature);
        }
        [HttpPost]
        public IActionResult Edit(ProductSelectedFeature selectedFeature)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = new SelectList(_selectedFeature.GetAllProducts().ToList(), "ProductId",
                  "ProductTitle", selectedFeature.ProductId);
                ViewData["FeatureId"] = new SelectList(_selectedFeature.GetAllFeatures().ToList(), "FeatureId",
                   "FeatureTitle", selectedFeature.FeatureId);

                return View(selectedFeature);
            }
            _selectedFeature.EditSelectedFeature(selectedFeature);
            return RedirectToAction(nameof(Index), new { id = selectedFeature.ProductId });

        }

        public IActionResult Delete(int id)
        {
            var selectedFeature = _selectedFeature.GetSelectedFeatureById(id);
            return View(selectedFeature);
        }

        public IActionResult DeleteConfirmed(int selectedFeatureId, int productId)
        {
            _selectedFeature.DeleteSelectedFeature(selectedFeatureId);
            return RedirectToAction(nameof(Index), new { id = productId });
        }
    }
}

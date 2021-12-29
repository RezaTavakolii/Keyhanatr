using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Core.Security;
using Keyhanatr.Data.Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductColorsController : Controller
    {
        IProductColorServices _colorServices;
        public ProductColorsController(IProductColorServices colorServices)
        {
            _colorServices = colorServices;
        }
        public IActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            ViewBag.ProductTitle = _colorServices.GetProductNameByProductId(id);
            List<ProductColor> color = _colorServices.GetColorByProductId(id);
            return View(color);
        }


        public IActionResult Create(int id)
        {
            ViewBag.ProductId = id;
            ViewBag.ProductTitle = _colorServices.GetProductNameByProductId(id);
            return View(new ProductColor() { ProductId = id });
        }

        [HttpPost]
        public IActionResult Create(ProductColor color, IFormFile imgUp,string productExist)
        {
            if (string.IsNullOrEmpty(productExist))
            {
                ViewBag.IsNullExist = true;
                ModelState.AddModelError("","لطفا تعداد موجودی را برای این رنگ تعیین کنید.");
                return View(color);
            }
            int num;
            ViewBag.NonNumericExist = false;
            if (!string.IsNullOrEmpty(productExist))
            {
                if (!int.TryParse(productExist, out num))
                {
                    ModelState.AddModelError("", "لطفا عدد 'تعداد موجودی' را درست وارد کنید.");
                    ViewBag.NonNumericExist = true;

                        return View(color);
                }
                else
                {
                    color.ProductExist = Convert.ToInt32(productExist);
                }
            }

            if (string.IsNullOrEmpty(color.ColorCode))
                color.ColorCode = $"ندارد";

            if (!ModelState.IsValid)
                return View(color);

            if (imgUp == null)
            {
                ViewBag.IsNullImg = true;
                ModelState.AddModelError("", "لطفا یک تصویر را برای رنگ مورد نظر انتخاب کنید");
                return View(color);
            }

            //TODO Create Color
            int productId = _colorServices.CreateColor(color, imgUp);
            return RedirectToAction("Index", new { id = productId });
        }


        public IActionResult Edit(int id)
        {
            ProductColor color = _colorServices.GetColorByColorId(id);
            ViewBag.ProductTitle = _colorServices.GetColorByColorId(id).Product.ProductTitle;

            return View(color);
        }

        [HttpPost]
        public IActionResult Edit(ProductColor color, IFormFile imgUp,string productExist)
        {
            if (string.IsNullOrEmpty(productExist))
            {
                ViewBag.IsNullExist = true;
                color.ProductId = color.ProductId;//needed
                //below line is not needed because in ViewRazore, inside the input related to "ProductExist", we have added
                //"@Model.ProductExist" inside "value attr", so when the admin leave this input null, it assigne 0 to it automatically!
                //color.ProductExist = 0;
                ViewBag.ProductTitle = _colorServices.GetColorByColorId(color.ColorId).Product.ProductTitle;
                return View(color);
            }
            int num;
            ViewBag.NonNumericExist = false;
            if (!string.IsNullOrEmpty(productExist))
            {
                if (!int.TryParse(productExist, out num))
                {
                    color.ColorId = color.ColorId;//needed
                    ViewBag.ProductTitle = _colorServices.GetColorByColorId(color.ColorId).Product.ProductTitle;//needed
                    ModelState.AddModelError("", "لطفا عدد 'تعداد موجودی' را درست وارد کنید.");
                    ViewBag.NonNumericExist = true;

                    return View(color);
                }
                else
                {
                    color.ProductExist = Convert.ToInt32(productExist);
                }


            }
            if (!ModelState.IsValid)
                return View(color);

            if (imgUp != null && !imgUp.IsImage())
            {
                ModelState.AddModelError("", "لطفا از فرمت درستی برای انتخاب تصویر اسنفاده کنید.");
                ViewBag.IsNotImage = true;
                return View(color);
            }
            //TODO Edit Color
            int productId = _colorServices.EditColor(color, imgUp);
            return RedirectToAction("Index", new { id = productId });
        }

        public IActionResult Delete(int id)
        {
            ProductColor color = _colorServices.GetColorByColorId(id);
            return View(color);
        }

        public IActionResult ConfirmedDelete(int id)
        {
            ProductColor color = _colorServices.GetColorByColorId(id);
            //Todo Delete color
            int productId = _colorServices.DeletColorById(id);
            return RedirectToAction("Index", new { id = productId });
        }
    }
}

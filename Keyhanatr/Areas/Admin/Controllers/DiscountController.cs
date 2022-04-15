using Keyhanatr.Core.Interfaces.Discounts;
using Keyhanatr.Data.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountController : Controller
    {

        private IDiscountServices _discountServices;
        public DiscountController(IDiscountServices discountServices)
        {
            _discountServices = discountServices;
        }

        //id == ProductId
        public IActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            ViewBag.ProductTitle = _discountServices.GetProductByProductId(id).ProductTitle;
            var discountOfProduct = _discountServices.GetProductByProductId(id).Discount;
            return View(discountOfProduct);
        }


        //id==ProductId
        public IActionResult Create(int id)
        {
            ViewBag.ProductId = _discountServices.GetProductByProductId(id).ProductId;
            ViewBag.ProductTitle = _discountServices.GetProductByProductId(id).ProductTitle;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Discount discount, int PId, string startDate = "", string endDate = "")
        {
            if (!string.IsNullOrEmpty(startDate))
            {
                string[] std = startDate.Split("/");
                discount.StartDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                string[] std = endDate.Split("/");
                discount.EndDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());
            }
            if (!ModelState.IsValid || discount.PercentValue < 0)
            {
                ViewBag.ProductId = _discountServices.GetProductByProductId(PId).ProductId;
                ViewBag.ProductTitle = _discountServices.GetProductByProductId(PId).ProductTitle;
                return View(discount);
            }

            _discountServices.AddDiscountToOneProduct(discount, PId);
            return Redirect("/Admin/Products");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.ProductId = id;
            ViewBag.ProductTitle = _discountServices.GetProductByProductId(id).ProductTitle;
            var discount = _discountServices.GetDiscountByProductId(id);
            return View(discount);
        }

        [HttpPost]
        public IActionResult Edit(Discount discount, int PId, string strtDate = "", string edDate = "")
        {

            if (!ModelState.IsValid || discount.PercentValue < 0)
            {
                ViewBag.ProductId = PId;
                ViewBag.ProductTitle = _discountServices.GetProductByProductId(PId).ProductTitle;
                return View(discount);
            }

            _discountServices.EditDiscount(discount, strtDate, edDate);
            return Redirect("/Admin/Products");
        }


        public IActionResult Delete(int id)
        {
            ViewBag.ProductId = id;
            ViewBag.ProductTitle = _discountServices.GetProductByProductId(id).ProductTitle;
            var discount = _discountServices.GetDiscountByProductId(id);
            return View(discount);
        }


        public IActionResult ConfirmeDelete(int id) {
            _discountServices.DeleteDiscountByProductId(id);
            return Redirect("/Admin/Products");
        }

        public void DeleteExpiredDiscounts()
        {
            _discountServices.deleteExpiredDiscounts();
        }
    }
}

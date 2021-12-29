using Keyhanatr.Core.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Keyhanatr.Data.Domain.Products;
using Microsoft.AspNetCore.Http;
using Keyhanatr.Core.Security;
using System.IO;

namespace Keyhanatr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private IProductServices _productServices;
        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        public IActionResult Index(int pageId = 1, int take = 2, string filter = "",
            string sortType = "all", string buyType = "all")
        {
            var options = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>() {
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem{ Value="date",Text="تاریخ"},
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem{ Value="producttitle",Text="نام محصول"},
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem{ Value="price",Text="قیمت"},
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem{ Value="updatedate",Text="بروز رسانی"},
            };

            ViewBag.SelectedSort = sortType;

            ViewData["SelectLists"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(options, "Value", "Text", sortType);

            ViewBag.SortType = sortType;
            ViewBag.Filter = filter;
            int pageCount = _productServices.ShowProductsByFilter(pageId, take, filter, sortType, buyType).Item1.PageCount;
            if (pageId > pageCount)
                pageId = pageCount;
            int start = 0; int end = 0;
            if (pageId <= 4)
            {
                start = 1;
                if (pageCount > 4)
                {
                    end = start + 3;
                }
                else
                {
                    end = pageCount;
                }
            }

            ViewBag.Start = start;
            ViewBag.End = end;

            int min = pageId;
            if (pageId > 4)
            {
                //8
                while (true)
                {
                    if (min % 4 == 0)
                    {
                        if (min == pageCount)
                        {
                            while (true)
                            {
                                --min;
                                if (min % 4 == 0)
                                {
                                    start = min + 1;
                                    break;
                                }

                            }
                        }
                        else
                        {
                            start = min + 1;
                        }
                        break;
                    }
                    min--;
                }
                ViewBag.Start = start;
                if (pageCount > start + 3)
                {
                    ViewBag.End = start + 3;
                }
                else
                {
                    ViewBag.End = pageCount;
                }


            }

            return View(_productServices.ShowProductsByFilter(pageId, take, filter, sortType, buyType));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Create()
        {
            var groups = _productServices.GetGroupsListItem();
            var subGroups = _productServices.GetSubGroups_ByGroupId_ListItem(int.Parse(groups.First().Value));
            ViewData["ProductGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups.ToList(), "Value", "Text");
            ViewData["ProductSubGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subGroups.ToList(), "Value", "Text");

            return View();
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Create(Product product, IFormFile imgUp, string productExist)
        {

            List<System.Web.Mvc.SelectListItem> groups = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> subGroups = new List<System.Web.Mvc.SelectListItem>();

            //string stringedExist = productExist.ToString();
            if (string.IsNullOrEmpty(productExist))
            {
                product.ProductExist = null;
            }
            int num;
            ViewBag.NonNumericExist = false;
            if (!string.IsNullOrEmpty(productExist))
            {
                if (!int.TryParse(productExist, out num))
                {
                    ModelState.AddModelError("", "لطفا عدد 'تعداد موجودی' را درست وارد کنید.");
                    ViewBag.NonNumericExist = true;

                    groups.AddRange(_productServices.GetGroupsListItem());
                    subGroups.AddRange(_productServices.GetSubGroups_ByGroupId_ListItem(int.Parse(groups.First().Value)));

                    ViewData["ProductGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups.ToList(), "Value", "Text");
                    ViewData["ProductSubGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subGroups.ToList(), "Value", "Text");
                    return View(product);
                }
                else
                {
                    product.ProductExist = Convert.ToInt32(productExist);
                }


            }


            if (!ModelState.IsValid)
            {

                if (imgUp == null || !imgUp.IsImage())
                {
                    ViewBag.IsNullImg = true;
                }


                groups.AddRange((IEnumerable<System.Web.Mvc.SelectListItem>)_productServices.GetGroupsListItem());
                subGroups.AddRange((IEnumerable<System.Web.Mvc.SelectListItem>)_productServices.GetSubGroups_ByGroupId_ListItem(int.Parse(groups.First().Value)));
                ViewData["ProductGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups.ToList(), "Value", "Text");
                ViewData["ProductSubGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subGroups.ToList(), "Value", "Text");

                return View(product);
            }


            if (imgUp == null || !imgUp.IsImage())
            {
                groups.AddRange((IEnumerable<System.Web.Mvc.SelectListItem>)_productServices.GetGroupsListItem());
                subGroups.AddRange((IEnumerable<System.Web.Mvc.SelectListItem>)_productServices.GetSubGroups_ByGroupId_ListItem(int.Parse(groups.First().Value)));
                ViewData["ProductGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups.ToList(), "Value", "Text");
                ViewData["ProductSubGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subGroups.ToList(), "Value", "Text");

                ViewBag.IsNullImg = true;

                return View(product);
            }

            //if (product.ProductSubGroupId == 0)
            //{
            //    groups.AddRange((IEnumerable<System.Web.Mvc.SelectListItem>)_productServices.GetGroupsListItem());
            //    subGroups.AddRange((IEnumerable<System.Web.Mvc.SelectListItem>)_productServices.GetSubGroups_ByGroupId_ListItem(int.Parse(groups.First().Value)));
            //    ViewData["ProductGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups.ToList(), "Value", "Text", product.ProductGroupId);
            //    ViewData["ProductSubGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subGroups.ToList(), "Value", "Text");

            //    ViewBag.IsNullSubGroup = true;
            //    ModelState.AddModelError(nameof(product.ProductSubGroupId), "برای ساخت محصول، حتما باید برای این گروه، زیرگروهی انتخاب شود.");
            //    return View(product);
            //}
            //Check if the price is decimal or numeric, or not.

            string stringedPrice = product.Price.ToString();
            if (!int.TryParse(stringedPrice, out num))
            {
                ModelState.AddModelError(nameof(product.Price), "لطفا عدد را درست وارد کنید..");

                groups.AddRange(_productServices.GetGroupsListItem());
                subGroups.AddRange(_productServices.GetSubGroups_ByGroupId_ListItem(int.Parse(groups.First().Value)));

                ViewData["ProductGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups.ToList(), "Value", "Text");
                ViewData["ProductSubGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subGroups.ToList(), "Value", "Text");


                return View(product);
            }


            _productServices.AddProduct(product, imgUp);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Product product = _productServices.GetProductById(id);

            var groups = _productServices.GetGroupsListItem();
            var subGroups = _productServices.GetSubGroups_ByGroupId_ListItem(product.ProductGroupId);


            ViewData["ProductGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups.ToList(), "Value", "Text", product.ProductGroupId);
            ViewData["ProductSubGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subGroups.ToList(), "Value", "Text", product.ProductSubGroup);

            
            return View(product);
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Edit(Product product, IFormFile imgUp,string productExist)
        {

            List<System.Web.Mvc.SelectListItem> groups = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> subGroups = new List<System.Web.Mvc.SelectListItem>();

            if (string.IsNullOrEmpty(productExist))
            {
                product.ProductExist = null;
            }
            int num;
            ViewBag.NonNumericExist = false;
            if (!string.IsNullOrEmpty(productExist))
            {
                if (!int.TryParse(productExist, out num))
                {
                    ModelState.AddModelError("", "لطفا عدد 'تعداد موجودی' را درست وارد کنید.");
                    ViewBag.NonNumericExist = true;

                    groups.AddRange(_productServices.GetGroupsListItem());
                    subGroups.AddRange(_productServices.GetSubGroups_ByGroupId_ListItem(int.Parse(groups.First().Value)));

                    ViewData["ProductGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups.ToList(), "Value", "Text");
                    ViewData["ProductSubGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subGroups.ToList(), "Value", "Text");
                    return View(product);
                }
                else
                {
                    product.ProductExist = Convert.ToInt32(productExist);
                }


            }


            if (!ModelState.IsValid)
            {
                groups.AddRange(_productServices.GetGroupsListItem());
                subGroups.AddRange(_productServices.GetSubGroups_ByGroupId_ListItem(product.ProductGroupId));
                ViewData["ProductGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups.ToList(), "Value", "Text", product.ProductGroupId);
                ViewData["ProductSubGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subGroups.ToList(), "Value", "Text");

                return View(product);
            }


            if (imgUp != null && !imgUp.IsImage())
            {
                groups.AddRange(_productServices.GetGroupsListItem());
                subGroups.AddRange(_productServices.GetSubGroups_ByGroupId_ListItem(product.ProductGroupId));
                ViewData["ProductGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups.ToList(), "Value", "Text", product.ProductGroupId);
                ViewData["ProductSubGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subGroups.ToList(), "Value", "Text");

                ViewBag.IsFormatIncorrect = true;

                return View(product);
            }

            if (product.ProductSubGroupId == 0)
            {
                groups.AddRange(_productServices.GetGroupsListItem());
                subGroups.AddRange(_productServices.GetSubGroups_ByGroupId_ListItem(product.ProductGroupId));
                ViewData["ProductGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups.ToList(), "Value", "Text", product.ProductGroupId);
                ViewData["ProductSubGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subGroups.ToList(), "Value", "Text");

                ModelState.AddModelError(nameof(product.ProductSubGroupId), "برای ساخت محصول، حتما باید برای این گروه، زیرگروهی انتخاب شود.");
                return View(product);
            }
            //Check if the price is decimal or numeric, or not.
            string stringedPrice = product.Price.ToString();
            if (!int.TryParse(stringedPrice, out num))
            {
                ModelState.AddModelError(nameof(product.Price), "لطفا عدد را درست وارد کنید..");

                groups.AddRange(_productServices.GetGroupsListItem());
                subGroups.AddRange(_productServices.GetSubGroups_ByGroupId_ListItem(product.ProductGroupId));
                ViewData["ProductGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(groups.ToList(), "Value", "Text", product.ProductGroupId);
                ViewData["ProductSubGroupId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subGroups.ToList(), "Value", "Text");

                return View(product);
            }

            _productServices.EditProduct(product, imgUp);
            return RedirectToAction("Index");


        }


        public IActionResult Delete(int id)
        {
            Product product = _productServices.GetProductById(id);
            return View(product);
        }

        public IActionResult ConfirmeDelete(int id)
        {
            //todo only delete the specified pruduct 
            bool isSuccess = _productServices.DeleteProduct(id);
            if (isSuccess == true)
            {
                TempData["IsSuccess"] = true;
                return RedirectToAction("Index");
            }
            TempData["failure"] = true;
            return RedirectToAction("Delete");
        }
        //for CkEditor images
        #region CkEditorImgUploaded
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("/file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/Images/MyImagesForCKEditor",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/Images/MyImagesForCKEditor/"}{fileName}";


            return Json(new { uploaded = true, url });
        }
        #endregion

        #region SubGroups_Json
        public IActionResult GetSubGroups_Json(int id)
        {
            List<System.Web.Mvc.SelectListItem> subGroups = new List<System.Web.Mvc.SelectListItem>();

            if (_productServices.GetSubGroups_ByGroupId_ListItem(id).Any())
            {
                subGroups.AddRange(_productServices.GetSubGroups_ByGroupId_ListItem(id));
                return Json(subGroups);
            }
            else
            {

                return Json(new List<System.Web.Mvc.SelectListItem>() { new System.Web.Mvc.SelectListItem() { Value = "0", Text = "برای این گروه ، زیر گروهی یافت نشد .." } }
);
            }
        }
        #endregion
    }
}

using Keyhanatr.Core.Convertors;
using Keyhanatr.Core.DTOs.Products;
using Keyhanatr.Core.ImageMethods;
using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static Keyhanatr.Core.DTOs.Products.ProductsViewModels;

namespace Keyhanatr.Core.Services.Products
{
    public class ProductServices : IProductServices
    {
        public IHttpContextAccessor _accessor;
        private KeyhanatrContext _context;
        public ProductServices(KeyhanatrContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }


        #region AdminLayout
        public int GetProductCount()
        {
            return _context.Products.Count();
        }
        #endregion


        #region Product Groups
        public List<ProductGroup> GetAllProducts()
        {
            return _context.ProductGroups.ToList();
        }

        public void AddProductGroup(ProductGroup productGroup)
        {
            _context.ProductGroups.Add(productGroup);
            _context.SaveChanges();
        }

        public void EditProductGroup(ProductGroup productGroup)
        {
            _context.ProductGroups.Update(productGroup);
            _context.SaveChanges();
        }

        public ProductGroup GetProductGroupById(int productGroupId)
        {
            return _context.ProductGroups.Find(productGroupId);
        }

        public void DeleteProductGroup(int productGroupId)
        {
            ProductGroup group = _context.ProductGroups.Include(s => s.ProductSubGroups)
               .ThenInclude(p=>p.Products).FirstOrDefault(g => g.ProductGroupId == productGroupId);

            group.ProductSubGroups.ForEach(s=>s.Products.ForEach(p=> DeleteProduct(p.ProductId)));
            foreach (var sub in group.ProductSubGroups)
            {
                _context.ProductSubGroups.Remove(sub);
            }
            _context.ProductGroups.Remove(group);
            _context.SaveChanges();
        }
        #endregion

        #region Product Sub Groups
        public List<ProductSubGroup> GetAllProductSubGroups()
        {
            return _context.ProductSubGroups.Include(s => s.ProductGroup).ToList();
        }

        public ProductSubGroup GetProductSubGroupById(int productSubGroupId)
        {
            return _context.ProductSubGroups.Include(s => s.ProductGroup)
                .FirstOrDefault(s => s.SubGroupId == productSubGroupId);
        }

        public List<SelectListItem> GetGroupsListItem()
        {
            return _context.ProductGroups.Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value = g.ProductGroupId.ToString()
            }).ToList();
        }

        public void AddProductSubGroup(ProductSubGroup productSubGroup)
        {
            _context.ProductSubGroups.Add(productSubGroup);
            _context.SaveChanges();
        }

        public void EditProductSubGroup(ProductSubGroup productSubGroup)
        {
            _context.ProductSubGroups.Update(productSubGroup);
            _context.SaveChanges();
        }

        public void DeleteProductSubGroupById(int productSubGroupId)
        {
            ProductSubGroup subGroup = _context.ProductSubGroups
               .Include(p=>p.Products).FirstOrDefault(sg => sg.SubGroupId == productSubGroupId);
            subGroup.Products.ForEach(p=> DeleteProduct(p.ProductId));
            _context.ProductSubGroups.Remove(subGroup);
            _context.SaveChanges();
        }


        #endregion


        #region Products
        public void AddProduct(Product product, IFormFile imgUp)
        {
            product.ImageName = ImgMehods.CreateProductImg(imgUp);
            product.CreateDate = DateTime.Now;
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public List<SelectListItem> GetSubGroups_ByGroupId_ListItem(int groupId)
        {
            return _context.ProductSubGroups.Where(s => s.ProductGroupId == groupId).Select(s => new SelectListItem()
            {
                Text = s.SubGroupTitle,
                Value = s.SubGroupId.ToString()
            }).ToList();
        }

        #endregion

        #region Index
        public Tuple<ShowProducts_VM, int> ShowProductsByFilter(int pageId = 1, int take = 8, string filter = "",
            string sortType = "all", string buyType = "all")
        {

            IQueryable<Product> products = _context.Products;


            //We have to assign the value to the "products" in order to have the searched term!
            //products=products.where(...) .... this is true, And ==> products.where(...) this is incorrect
            if (!string.IsNullOrEmpty(filter))
                products = products.Where(p => p.ProductTitle.Contains(filter) || p.Tags.Contains(filter));

            sortType = sortType.ToLower();
            switch (sortType)
            {
                case "all":
                    break;

                case "date":
                    products = products.OrderByDescending(p => p.CreateDate);
                    break;

                case "updatedate":
                    products = products.OrderByDescending(p => p.UpdateDate ?? DateTime.MinValue);
                    break;

                case "price":
                    products = products.OrderByDescending(p => Convert.ToDecimal(p.Price));
                    break;

                case "producttitle":
                    products = products.OrderByDescending(p => p.ProductTitle);
                    break;

                default:
                    break;
            }

            buyType = buyType.ToLower();
            switch (buyType)
            {

                case "all":
                    break;
                case "buy":
                    {
                        //we couldn't cast "c.CoursePric" to float or even to int!
                        //I think this kind of casting(float(c.CoursePrice)) is not allowed! So we used "Convert" to cast it!
                        products = products.Where(c => Convert.ToDecimal(c.Price) > 0);
                        break;
                    }
                case "free":
                    {
                        products = products.Where(c => Convert.ToDecimal(c.Price) == 0);
                        break;
                    }
            }



            ShowProducts_VM productsVM = new();

            int productCount = products.Count();

            decimal decimalPageCount = (decimal)productCount / take;
            int intPageCount = (int)decimalPageCount;
            int skip = (pageId - 1) * take;

            if (decimalPageCount - intPageCount > 0)
            {
                productsVM.PageCount = intPageCount + 1;
            }
            else
            {
                productsVM.PageCount = intPageCount;
            }


            productsVM.CurrentPage = pageId;

            productsVM.Products = products.Include(s => s.ProductSubGroup).Include(g => g.ProductGroup)
                .AsEnumerable().Skip(skip).Take(take).OrderByDescending(d => d.CreateDate).ToList();


            return Tuple.Create(productsVM, productCount);
        }

        #endregion

        #region Edit.cshtml
        public Product GetProductById(int productId)
        {
            return _context.Products.Include(g => g.ProductGroup).Include(s => s.ProductSubGroup)
                .Include(g => g.ProductGalleries).Include(c => c.ProductColors).Include(s => s.ProductSelectedFeatures)
                .ThenInclude(f => f.ProductFeature)
                .FirstOrDefault(p => p.ProductId == productId);
        }

        public void EditProduct(Product product, IFormFile imgUp)
        {
            product.UpdateDate = DateTime.Now;

            if (imgUp != null)
                product.ImageName = ImgMehods.EditProductImg(product.ImageName, imgUp);

            _context.Products.Update(product);
            _context.SaveChanges();
        }
        #endregion

        #region Delete Product
        public bool DeleteProduct(int productId)
        {
            try
            {
                Product product = _context.Products.Include(c => c.ProductColors).Include(g => g.ProductSelectedFeatures)
                    .Include(s => s.ProductGalleries).FirstOrDefault(p => p.ProductId == productId);
                ImgMehods.DeleteProductImage(product.ImageName);

                product.ProductColors.ForEach(c => _context.ProductColors.Remove(c));
                product.ProductGalleries.ForEach(g => _context.ProductGalleries.Remove(g));
                product.ProductSelectedFeatures.ForEach(s => _context.ProductSelectedFeatures.Remove(s));
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Comments


        Tuple<List<ProductComment>, int> IProductServices.GetAllCommentsByProductId(int productId)
        {
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int currentUserId = int.Parse(identifierClaim.Value);

            var comments = _context.ProductComments.Include(u => u.User)
                .Include(p => p.Product).Where(p => p.ProductId == productId).ToList();

            return Tuple.Create(comments, currentUserId);
        }


        #endregion


        #region NavGroups
        public List<ProductNavGroup> GetAllNavGroups()
        {
            return _context.ProductNavGroups.ToList();
        }

        public void AddNavGroup(ProductNavGroup navGroup)
        {
            _context.ProductNavGroups.Add(navGroup);
            _context.SaveChanges();
        }

        public ProductNavGroup GetNavGroupById(int navGroupId)
        {
            return _context.ProductNavGroups.FirstOrDefault(n => n.NavGroupId == navGroupId);
        }

        public void EditNavGroup(ProductNavGroup navGroup)
        {
            _context.ProductNavGroups.Update(navGroup);
            _context.SaveChanges();
        }

        public void DeleteNavGroupById(int navGroupId)
        {
            var navGroup = _context.ProductNavGroups.Include(g => g.ProductGroups)
                .ThenInclude(s => s.ProductSubGroups).ThenInclude(p=>p.Products).FirstOrDefault(n => n.NavGroupId == navGroupId);
            navGroup.ProductGroups.ForEach(g=> g.ProductSubGroups.ForEach(s=> s.Products.ForEach(p=> DeleteProduct(p.ProductId))));
            foreach (var item in navGroup.ProductGroups)
            {
                item.ProductSubGroups.ForEach(s => _context.ProductSubGroups.Remove(s));
            }
            foreach (var item in navGroup.ProductGroups)
            {
                _context.ProductGroups.Remove(item);
            }
            _context.ProductNavGroups.Remove(navGroup);
            _context.SaveChanges();
        }
        
        //it is used for ProductGroups to get the NavGroups if They are exist
        public List<SelectListItem> GetListOfNavGroups()
        {
            return _context.ProductNavGroups.Select(nav => new SelectListItem()
            {
                Text = nav.NavTitle,
                Value = nav.NavGroupId.ToString()
            }).ToList();
        }

        
        #endregion
    }
}

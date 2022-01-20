using Keyhanatr.Core.ImageMethods;
using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Services.Products
{
    public class ProductColorServices : IProductColorServices
    {
        private KeyhanatrContext _context;
        public ProductColorServices(KeyhanatrContext context)
        {
            _context = context;
        }



        public int CreateColor(ProductColor color, IFormFile imgUp)
        {
            color.ImageName = ImgMehods.CreateProductColorImg(imgUp);

            _context.ProductColors.Add(color);
            _context.SaveChanges();

            return color.ProductId;
        }

        public int DeletColorById(int colorId)
        {
            ProductColor color = GetColorByColorId(colorId);
            _context.ProductColors.Remove(color);
            _context.SaveChanges();

            return color.ProductId;
        }

        public int EditColor(ProductColor color, IFormFile imgUp)
        {
            color.ImageName = ImgMehods.EditProductColorImg(color.ImageName, imgUp);
            _context.ProductColors.Update(color);
            _context.SaveChanges();
            return color.ProductId;
        }

        public ProductColor GetColorByColorId(int colorId)
        {
            return _context.ProductColors.Include(p => p.Product)
                .FirstOrDefault(c => c.ColorId == colorId);
        }

        public List<ProductColor> GetColorByProductId(int productId)
        {
            return _context.ProductColors.Where(p => p.ProductId == productId).ToList();
        }

        public string GetProductNameByProductId(int id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == id).ProductTitle;
        }
    }
}

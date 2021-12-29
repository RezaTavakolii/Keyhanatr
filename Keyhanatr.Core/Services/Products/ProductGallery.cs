using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Services.Products
{
    public class ProductGallery : IProductGallery
    {

        private KeyhanatrContext _context;
        public ProductGallery(KeyhanatrContext context)
        {
            _context = context;
        }
        public void AddProductGallery(Data.Domain.Products.ProductGallery productGallery)
        {
            _context.ProductGalleries.Add(productGallery);
            SaveChanges();
        }

        public void DeleteProductGallery(int galleryId)
        {
            var gallery = GetGalleryById(galleryId);
            _context.ProductGalleries.Remove(gallery);
            SaveChanges();
        }

        public void EditProductGallery(Data.Domain.Products.ProductGallery productGallery)
        {
            _context.ProductGalleries.Update(productGallery);
            SaveChanges();
        }

        public IEnumerable<Data.Domain.Products.ProductGallery> GetAllGalleries()
        {
            return _context.ProductGalleries.Include(p => p.Product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }

        public Data.Domain.Products.ProductGallery GetGalleryById(int galleryId)
        {
            return _context.ProductGalleries.Include(p => p.Product).FirstOrDefault(g => g.GalleryId == galleryId);
        }

        public IEnumerable<Data.Domain.Products.ProductGallery> GetGalleryByProductId(int productId)
        {
            return _context.ProductGalleries.Where(g => g.ProductId == productId);
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

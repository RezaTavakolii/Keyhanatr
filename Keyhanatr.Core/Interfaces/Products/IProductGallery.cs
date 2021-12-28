using Keyhanatr.Data.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Interfaces.Products
{
   public interface IProductGallery
    {
        IEnumerable<ProductGallery> GetAllGalleries();
        IEnumerable<ProductGallery> GetGalleryByProductId(int productId);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int productId);
        ProductGallery GetGalleryById(int galleryId);
        void AddProductGallery(ProductGallery productGallery);
        void EditProductGallery(ProductGallery productGallery);
        void DeleteProductGallery(int galleryId);
        void SaveChanges();
    }
}

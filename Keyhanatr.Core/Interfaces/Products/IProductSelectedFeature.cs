using Keyhanatr.Core.Services.Products;
using Keyhanatr.Data.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Interfaces.Products
{
  public  interface IProductSelectedFeature
    {
        IEnumerable<Keyhanatr.Data.Domain.Products.ProductSelectedFeature> GetAllSelectedFeatures();
        Keyhanatr.Data.Domain.Products.ProductSelectedFeature GetSelectedFeatureById(int selectedFeatureId);
        void AddSelectedFeature(Keyhanatr.Data.Domain.Products.ProductSelectedFeature selectedFeature);
        void EditSelectedFeature(ProductSelectedFeature selectedFeature);
        void DeleteSelectedFeature(int selectedFeatureId);
        void SaveChanges();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> GetAllProducts();
        IEnumerable<ProductFeature> GetAllFeatures();
        Product GetProductById(int productId);
    }
}

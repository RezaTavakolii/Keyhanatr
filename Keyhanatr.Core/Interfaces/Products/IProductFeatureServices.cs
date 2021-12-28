using Keyhanatr.Data.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Interfaces.Products
{
    public interface IProductFeatureServices
    {
        IEnumerable<ProductFeature> GetAllFeatures();
        ProductFeature GetFeatureById(int featureId);
        void AddFeature(ProductFeature productFeature);
        void EditFeature(ProductFeature productFeature);
        void DeleteFeature(int FeatureId);
        void SaveChanges();
    }
}

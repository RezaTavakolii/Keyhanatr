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
    public class ProductFeatureServices:IProductFeatureServices
    {
        private KeyhanatrContext _context;

        public ProductFeatureServices(KeyhanatrContext context)
        {
            _context = context;
        }
        public void AddFeature(ProductFeature productFeature)
        {
            _context.ProductFeatures.Add(productFeature);
            SaveChanges();
        }

        public void DeleteFeature(int FeatureId)
        {
            var feature = GetFeatureById(FeatureId);
            feature.ProductSelectedFeatures.ToList().ForEach(s =>
                _context.Remove(s)
            ); 
            _context.ProductFeatures.Remove(feature);
            SaveChanges();
        }

        public void EditFeature(ProductFeature productFeature)
        {
            _context.ProductFeatures.Update(productFeature);
            SaveChanges();
        }

        public IEnumerable<ProductFeature> GetAllFeatures()
        {
            return _context.ProductFeatures;
        }

        public ProductFeature GetFeatureById(int featureId)
        {
            return _context.ProductFeatures.Include(s=>s.ProductSelectedFeatures).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

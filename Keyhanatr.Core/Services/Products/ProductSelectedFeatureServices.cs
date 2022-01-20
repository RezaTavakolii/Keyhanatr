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
    public class ProductSelectedFeatureServices:IProductSelectedFeature
    {

        private KeyhanatrContext _context;
        public ProductSelectedFeatureServices(KeyhanatrContext context)
        {
            _context = context;
        }

        public void AddSelectedFeature(ProductSelectedFeature selectedFeature)
        {
            _context.ProductSelectedFeatures.Add(selectedFeature);
            SaveChanges();
        }

        public void DeleteSelectedFeature(int selectedFeatureId)
        {
            var selectedFeature = GetSelectedFeatureById(selectedFeatureId);
            _context.ProductSelectedFeatures.Remove(selectedFeature);
            SaveChanges();
        }

        public void EditSelectedFeature(ProductSelectedFeature selectedFeature)
        {
            _context.ProductSelectedFeatures.Update(selectedFeature);
            SaveChanges();
        }

        public IEnumerable<Data.Domain.Products.ProductFeature> GetAllFeatures()
        {
            return _context.ProductFeatures.Include(s => s.ProductSelectedFeatures);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Include(s => s.ProductSelectedFeatures);
        }

        public IEnumerable<ProductSelectedFeature> GetAllSelectedFeatures()
        {
            return _context.ProductSelectedFeatures.Include(p => p.Product).Include(f =>
                 f.ProductFeature);
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public ProductSelectedFeature GetSelectedFeatureById(int selectedFeatureId)
        {
            return _context.ProductSelectedFeatures.Include(p => p.Product)
                .Include(f => f.ProductFeature).FirstOrDefault(s =>
                  s.SelectedFeatureId == selectedFeatureId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

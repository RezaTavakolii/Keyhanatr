using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Services.Products
{
    public class ProductServices : IProductServices
    {
        private KeyhanatrContext _context;
        public ProductServices(KeyhanatrContext context)
        {
            _context = context;
        }

       

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

        public void DeleteProductGroup(ProductGroup productGroup)
        {
            _context.ProductGroups.Remove(productGroup);
            _context.SaveChanges();
        }
    }
}

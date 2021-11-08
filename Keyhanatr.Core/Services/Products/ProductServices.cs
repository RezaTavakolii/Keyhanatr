using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Keyhanatr.Core.Services.Products
{
    public class ProductServices : IProductServices
    {
        private KeyhanatrContext _context;
        public ProductServices(KeyhanatrContext context)
        {
            _context = context;
        }



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
                .FirstOrDefault(g=> g.GroupId==productGroupId);

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
            return _context.ProductSubGroups.Include(s=> s.ProductGroup)
                .FirstOrDefault(s=>s.SubGroupId==productSubGroupId);
        }

        public List<SelectListItem> GetGroupsListItem()
        {
            return _context.ProductGroups.Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value = g.GroupId.ToString()
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
                .FirstOrDefault(sg => sg.SubGroupId == productSubGroupId);
            _context.ProductSubGroups.Remove(subGroup);
            _context.SaveChanges();
        }
        #endregion
    }
}

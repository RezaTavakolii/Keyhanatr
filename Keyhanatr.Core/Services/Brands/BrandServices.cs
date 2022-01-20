using Keyhanatr.Core.Interfaces.Brands;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Services.Brands
{
    public class BrandServices : IBrandService
    {

        private KeyhanatrContext _context;
        public BrandServices(KeyhanatrContext context)
        {
            _context = context;
        }
        public void AddBrand(Brand brand)
        {
            _context.Add(brand);
            _context.SaveChanges();
        }

        public bool BrandExist(int id)
        {
            return _context.Brands.Any(b=>b.BrandID==id);
        }

        public void DeleteBrand(int brandId)
        {
            var brand = GetBrandById(brandId);
            _context.Remove(brand);
            _context.SaveChanges();
        }

        public void EditBrand(Brand brand)
        {
            _context.Update(brand);
            _context.SaveChanges();
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return _context.Brands;
        }

        public Brand GetBrandById(int brandId)
        {
           return _context.Brands.Find(brandId);
        }
    }
}

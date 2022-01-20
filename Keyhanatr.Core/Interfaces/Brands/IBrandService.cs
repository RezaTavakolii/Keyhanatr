using Keyhanatr.Data.Domain.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Interfaces.Brands
{
   public interface IBrandService
    {
        public IEnumerable<Brand> GetAllBrands();
        Brand GetBrandById(int brandId);
        void AddBrand(Brand brand);
        void EditBrand(Brand brand);
        void DeleteBrand(int brandId);
        bool BrandExist(int id);
    }
}

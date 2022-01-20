using Keyhanatr.Data.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.DTOs.Products
{
    public class GroupsProductsViewModel
    {
        public List<ProductNavGroup> productNavGroups { get; set; }
        public List<ProductGroup> ProductGroups { get; set; }
        public List<ProductSubGroup> ProductSubGroups { get; set; }
    }
}

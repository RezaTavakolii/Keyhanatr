using Keyhanatr.Data.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.DTOs.Products
{
    public class ProductsViewModels
    {
        public class ShowProducts_VM
        {
            //for paging
            public int PageCount { get; set; }
            public int CurrentPage { get; set; }
            public List<Product> Products { get; set; }
        }
    }
}

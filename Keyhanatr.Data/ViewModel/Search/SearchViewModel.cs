using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.ViewModel.Search
{
    public class SearchViewModel
    {
        public List<Data.Domain.Products.Product> Products { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}

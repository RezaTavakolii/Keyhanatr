using Keyhanatr.Data.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Interfaces.Products
{
    public interface IProductServices
    {
        List<ProductGroup> GetAllProducts();

        void AddProductGroup(ProductGroup productGroup);

        void EditProductGroup(ProductGroup productGroup);

        ProductGroup GetProductGroupById(int productGroupId);

        void DeleteProductGroup(ProductGroup productGroup);
    }
}

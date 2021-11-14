using Keyhanatr.Data.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Keyhanatr.Core.Interfaces.Products
{
    public interface IProductServices
    {

        ////For ProductGroupsServices
       

        List<ProductGroup> GetAllProducts();

        void AddProductGroup(ProductGroup productGroup);

        void EditProductGroup(ProductGroup productGroup);

        ProductGroup GetProductGroupById(int productGroupId);

        void DeleteProductGroup(int productGroupId);


        ////For ProductSubGroupServices
        ///

        //for Index
        List<ProductSubGroup> GetAllProductSubGroups();

        //for Details
        ProductSubGroup GetProductSubGroupById(int productSubGroupId);

        //for Create
        List<SelectListItem> GetGroupsListItem();
        void AddProductSubGroup(ProductSubGroup productSubGroup);
        void EditProductSubGroup(ProductSubGroup productSubGroup);
        void DeleteProductSubGroupById(int productSubGroupId);
        

    }
}

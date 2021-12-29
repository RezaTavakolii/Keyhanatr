using Keyhanatr.Data.Domain.Products;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Interfaces.Products
{
    public interface IProductColorServices
    {
        //for Index.cshtml
        List<ProductColor> GetColorByProductId(int productId);
        string GetProductNameByProductId(int id);

        //for Create.cs

        //returns "ProductId" so that it can be redirected to Index.cs within ProductColorControllers
        int CreateColor(ProductColor color, IFormFile imgUp);

        //for Edit.cshtml
        ProductColor GetColorByColorId(int colorId);

        int EditColor(ProductColor color,IFormFile imgUp);

        //For Delete.cshtml

        //it returns ProductId in the end
        int DeletColorById(int colorId);
    }
}

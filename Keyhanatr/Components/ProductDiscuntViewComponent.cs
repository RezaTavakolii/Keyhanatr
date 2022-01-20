using Keyhanatr.Core.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr.Components
{
    public class ProductDiscuntViewComponent:ViewComponent
    {
        private IProductServices _productServices;

        public ProductDiscuntViewComponent(IProductServices productServices)
        {
            _productServices = productServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/Shared/_ProductDiscount.cshtml", _productServices.GetProducts());
        }
    }
}

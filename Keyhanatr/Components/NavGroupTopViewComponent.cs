using Keyhanatr.Core.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr.Components
{
    public class NavGroupTopViewComponent:ViewComponent
    {
        private IProductServices _productServices;

        public NavGroupTopViewComponent(IProductServices productServices)
        {
            _productServices = productServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/Shared/_NavGroupTop.cshtml", _productServices.GetProductsGroups());
        }
    }
}

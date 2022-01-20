using Keyhanatr.Core.Interfaces.Brands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr.Components
{
    public class BrandViewComponent:ViewComponent
    {
        private IBrandService _brandService;

        public BrandViewComponent(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/Shared/_Brands.cshtml", _brandService.GetAllBrands());
        }
    }
}

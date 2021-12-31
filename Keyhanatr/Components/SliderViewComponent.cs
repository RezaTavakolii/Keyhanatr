using Keyhanatr.Core.Interfaces.Sliders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr.Components
{
    public class SliderViewComponent: ViewComponent
    {
        private ISliderServices _sliderService;

        public SliderViewComponent(ISliderServices sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/Shared/_Carousel.cshtml", _sliderService.GetAllSlider());
        }
    }
}

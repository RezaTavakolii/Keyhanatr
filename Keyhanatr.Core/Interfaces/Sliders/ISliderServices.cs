using Keyhanatr.Data.Domain.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Interfaces.Sliders
{
    public interface ISliderServices
    {
        public IEnumerable<Slider> GetAllSlider();
        Slider GetSliderById(int sliderId);
        void AddSlider(Slider slider);
        void EditSlider(Slider slider);
        void DeleteSlider(int sliderId);
        bool SliderExist(int id);
    }
}

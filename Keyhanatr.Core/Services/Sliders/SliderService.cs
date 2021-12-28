using Keyhanatr.Core.Interfaces.Sliders;
using Keyhanatr.Data.Context;
using Keyhanatr.Data.Domain.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Services.Sliders
{
    public class SliderService : ISliderServices
    {
        private KeyhanatrContext _context;
        public SliderService(KeyhanatrContext context)
        {
            _context = context;
        }
        public void AddSlider(Slider slider)
        {
            _context.Add(slider);
            _context.SaveChanges();
        }

        public void DeleteSlider(int sliderId)
        {
            var slider = GetSliderById(sliderId);
            _context.Remove(slider);
            _context.SaveChanges();
        }

        public void EditSlider(Slider slider)
        {
            _context.Update(slider);
            _context.SaveChanges();
        }

        public IEnumerable<Slider> GetAllSlider()
        {
            return _context.Sliders.Where(s=>s.IsActive);
        }

        public Slider GetSliderById(int sliderId)
        {
            return _context.Sliders.Find(sliderId);
        }
    }
}

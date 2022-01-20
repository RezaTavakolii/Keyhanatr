using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Brand
{
    public class Brand
    {
        [Key]
        public int BrandID { get; set; }

        [Display(Name = " عنوان برند")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "تصویر برند")]
        public string ImageName { get; set; }

    }
}

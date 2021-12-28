using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Products
{
  public  class ProductFeature
    {
        [Key]
        public int FeatureId { get; set; }
        [Display(Name = "عنوان ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "مقدار {0} نمیتواند بیش تر از {1} کاراکتر باشد")]
        public string FeatureTitle { get; set; }

        //Navigation Property
        public List<ProductSelectedFeature> ProductSelectedFeatures { get; set; }
    }
}

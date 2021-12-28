using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Products
{
  public  class ProductSelectedFeature
    {
        [Key]
        public int SelectedFeatureId { get; set; }


        [Required]
        [Display(Name = "نام ویژگی")]
        public int FeatureId { get; set; }

        [Required]
        [Display(Name = "نام محصول")]
        public int ProductId { get; set; }

        [Display(Name = "نوع ویژگی انخابی")]
        [Required(ErrorMessage = "لطفا نوع ویژگی را انتخاب کنید")]
        [MaxLength(100, ErrorMessage = "نباید بیشتر از 100 کاراکتر برای این فیلد در نظر گرفته شود")]
        public string Value { get; set; }

        //Navigation Property
        [ForeignKey("ProductId")]
        public Product Product { get; set; }


        [ForeignKey("FeatureId")]
        public ProductFeature ProductFeature { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Products
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }


        [Required]
        public int ProductGroupId { get; set; }


        [Required]
        public int ProductSubGroupId { get; set; }

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیش از {1} باشد")]
        public string ProductTitle { get; set; }

        [Display(Name = "توضیح کوتاه ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیش از {1} باشد")]
        public string ShortDescription { get; set; }


        [Display(Name = "توضیح بلند ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(600, ErrorMessage = "{0} نمی تواند بیش از {1} باشد")]
        public string Description { get; set; }


        [Display(Name = "برچسب محصول ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از {1} باشد")]
        public string Tags { get; set; }

        [Display(Name = "تصویر محصول  ")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیش از {1} باشد")]
        public string ImageName { get; set; }


        [Display(Name = "قیمت محصول  ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public decimal Price { get; set; }



        [Display(Name = " تاریخ درج ")]
        public DateTime CreateDate { get; set; } = DateTime.Now;


        #region Relations

        [ForeignKey("ProductSubGroupId")]
        public ProductSubGroup ProductSubGroup { get; set; }
        #endregion
    }
}

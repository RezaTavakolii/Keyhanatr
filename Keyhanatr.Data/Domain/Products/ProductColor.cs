using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Products
{
    public class ProductColor
    {
        [Key]
        public int ColorId { get; set; }

        public int ProductId { get; set; }


        [Display(Name = "نام رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیش تر از {1}کاراکتر باشد")]
        public string ColorName { get; set; }


        [Display(Name = "بک گراند رنگ")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیش تر از {1}کاراکتر باشد")]
        public string ImageName { get; set; }


        [Display(Name = "کد رنگ")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیش تر از {1}کاراکتر باشد")]
        public string ColorCode { get; set; }

        [Required]
        [Display(Name = "تداد موجودی از این رنگ")]
        public int ProductExist { get; set; }

        //Navigation Properties

        [ForeignKey("ProductId")]
        public Product Product { get; set; }



    }
}

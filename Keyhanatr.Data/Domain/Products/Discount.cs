using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Products
{
   public class Discount
    {
        [Key]
        public int DiscountId { get; set; }


        [Display(Name ="عدد تخفیف")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public int PercentValue { get; set; }


        [Display(Name = " تاریخ شروع")]
        public DateTime? StartDate { get; set; }



        [Display(Name = " تاریخ پایان")]
        public DateTime? EndDate { get; set; }

        //Navigation Properties
        public List<Product> Products { get; set; }


    }
}

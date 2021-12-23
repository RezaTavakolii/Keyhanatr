using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.User
{
   public class Address
    {
        [Key]
        public int AddressID { get; set; }

        [Display(Name = "نام کاربری")]
        public int UserId { get; set; }

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Family { get; set; }

        [Display(Name = "نام شرکت ")]
        public string Company { get; set; }

        [Display(Name = " استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Ostan { get; set; }

        [Display(Name = "شهر ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Shahr { get; set; }

        [Display(Name = "آدرس ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Adress { get; set; }

        [Display(Name = "کد پستی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        public string CodePosti { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        [Phone(ErrorMessage = "شماره همراه وارد شده معتبر نمی باشد")]
        public string Mobile { get; set; }

        #region Relations
        public User User { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.ViewModel.Account
{
    public class ResetPasswordViewModel
    {

        [Display(Name = "کد تأیید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(6, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        public string ActiveCode { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        public bool ForgetPass { get; set; }
    }
}

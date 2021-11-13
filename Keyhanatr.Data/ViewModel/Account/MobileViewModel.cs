using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.ViewModel.Account
{
    public class MobileViewModel
    {
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        [Phone(ErrorMessage = "شماره همراه وارد شده معتبر نمی باشد")]
        public string Mobile { get; set; }
    }
}

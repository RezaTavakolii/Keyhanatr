using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Products
{
    public class ProductNavGroup
    {
        [Key]
        public int NavGroupId { get; set; }


        [Display(Name = "عنوان گروه ناوبری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100,ErrorMessage = "{0} نمیتواند بیش تر از {1}کاراکتر باشد")]
        public string NavTitle { get; set; }

        [Display(Name = "رنگ پس زمینه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string BackColor { get; set; }

        [Display(Name = "تصویر")]
        public string ImageName { get; set; }

        #region Relations
        public List<ProductGroup> ProductGroups { get; set; }
        #endregion

    }
}

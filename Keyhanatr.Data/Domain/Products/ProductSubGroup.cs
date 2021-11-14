using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Products
{
    public class ProductSubGroup
    {
        [Key]
        public int SubGroupId { get; set; }


        [Required]
        public int GroupId { get; set; }


        [DisplayName("عنوان زیر گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        public string SubGroupTitle { get; set; }


        #region Relations

        [ForeignKey("GroupId")]
        public ProductGroup ProductGroup { get; set; }


        public List<Product> Products { get; set; }

        #endregion
    }
}

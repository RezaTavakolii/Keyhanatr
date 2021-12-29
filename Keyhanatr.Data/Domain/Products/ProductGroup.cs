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
  public  class ProductGroup
    {
        [Key]
        public int ProductGroupId { get; set; } 
        
        
      [Required]
        public int ProductNavGroupId { get; set; }

        [DisplayName("عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        public string GroupTitle { get; set; }


        #region Relations
        public List<ProductSubGroup> ProductSubGroups { get; set; }


        public List<Product> Products { get; set; }

        [ForeignKey("ProductNavGroupId")]
        public ProductNavGroup ProductNavGroup { get; set; }
        #endregion

    }
}

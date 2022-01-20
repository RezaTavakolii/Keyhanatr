using Keyhanatr.Data.Domain.Order;
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


        public int? DiscountId { get; set; }

        [Display(Name ="قیمت")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public int Price { get; set; }


        [Display(Name = " تاریخ درج محصول ")]       
        public DateTime CreateDate { get; set; }


        public string ImageName { get; set; }


        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250,ErrorMessage = "{0} نمیتواند بیش تر از {1}کاراکتر باشد")]
        public string ProductTitle { get; set; }


        [Display(Name = "برچسب ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Tags { get; set; }


        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }


        [Display(Name = "تاریخ بروز رسانی")]
        public DateTime? UpdateDate { get; set; }


        public int? ProductExist { get; set; }


        [Display(Name = "تعدادفروش محصول")]
        public int? SalesCount { get; set; }

        [Display(Name = "تعدادفروش محصول")]
        public int? ToSalesCount { get; set; }



        [Display(Name = "مقدار کل دریافتی از محصول")]
        public int? SumSalesUntilNow { get; set; }


        #region Relations

        [ForeignKey("ProductGroupId")]
        public ProductGroup ProductGroup { get; set; }


        [ForeignKey("ProductSubGroupId")]
        public ProductSubGroup ProductSubGroup { get; set; }

        public List<ProductSelectedFeature> ProductSelectedFeatures { get; set; }


        public List<ProductGallery> ProductGalleries { get; set; }

        public List<ProductColor> ProductColors { get; set; }
        
        
        public List<ProductComment> ProductComments { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        [ForeignKey("DiscountId")]
        public Discount Discount { get; set; }

        #endregion





    }
}

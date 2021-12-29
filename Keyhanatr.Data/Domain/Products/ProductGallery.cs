using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Products
{
  public  class ProductGallery
    {
        [Key]
        public int GalleryId { get; set; }


        [Required]
        [Display(Name = "نام محصول")]
        public int ProductId { get; set; }


        [Display(Name = "تصویر گالری")]
        [MaxLength(50)]
        public string ImageName { get; set; }

        //Navigation Property

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}

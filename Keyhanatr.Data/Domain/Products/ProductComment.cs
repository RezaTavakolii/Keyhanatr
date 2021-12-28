using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Products
{
  public  class ProductComment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "متن نوضیحات بیش از حد می باشد !")]
        public string Comment { get; set; }

        [Display(Name ="تاریخ درج")]
        public DateTime CreateDate { get; set; } = DateTime.Now;


        public bool IsAccepted { get; set; }

        //Navigation Property

        [ForeignKey("UserId")]
        public User.User User { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}

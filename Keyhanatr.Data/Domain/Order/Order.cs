using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Order
{
   public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsFinaly { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}

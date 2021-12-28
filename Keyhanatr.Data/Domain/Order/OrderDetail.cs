using Keyhanatr.Data.Domain.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Order
{
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int Price { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}

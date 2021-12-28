using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Domain.Products
{
   public class Discount
    {
        [Key]
        public int Id { get; set; }
        public string Percent { get; set; }

        //
       

    }
}

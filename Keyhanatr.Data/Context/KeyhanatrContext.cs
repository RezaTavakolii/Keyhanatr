using Keyhanatr.Data.Domain.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Data.Context
{
   public class KeyhanatrContext : DbContext
    {
        public KeyhanatrContext(DbContextOptions<KeyhanatrContext> option) : base(option)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }


}

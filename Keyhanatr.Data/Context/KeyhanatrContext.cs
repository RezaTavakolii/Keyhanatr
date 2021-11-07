using Keyhanatr.Data.Domain.Products;
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
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductSubGroup> ProductSubGroups { get; set; }


        #region ModelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var item in modelBuilder.Model.GetEntityTypes().SelectMany(e =>
             e.GetForeignKeys()))
            {
                item.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        #endregion
    }


}

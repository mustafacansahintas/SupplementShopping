using Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext():base("DatabaseContext")
        {

        }
        public DbSet<Category> category { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<_User> user { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<Like> like { get; set; }
        public DbSet<Brand> brands { get; set; }


      
    }
}

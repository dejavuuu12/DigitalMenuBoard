using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DigitalMenu.Models
{
    public class DBContext : DbContext
    {
        public DBContext()
            : base("Name=dbContext")
        {
            Database.SetInitializer<DBContext>(new DropCreateDatabaseIfModelChanges<DBContext>());
        }
        //public DbSet<Item> Items { get; set; }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
    }
}
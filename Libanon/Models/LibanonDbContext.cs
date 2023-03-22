using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Libanon.Models;
using Libanon.Models.Connect_EF_Config;

namespace Libanon.Models
{
    public class LibanonDbContext:DbContext
    {
        public LibanonDbContext() : base("name = dbLibanon")
        {
            Database.SetInitializer<LibanonDbContext>(new DropCreateDatabaseIfModelChanges<LibanonDbContext>());
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookEntityConfiguration());
            modelBuilder.Configurations.Add(new ImageEntityConfiguration());
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Libanon.Models.Connect_EF_Config
{
    public class BookEntityConfiguration: EntityTypeConfiguration<Book>
    {
        public BookEntityConfiguration()
        {
            this.ToTable("Books");
            this.HasKey<int>(b => b.ISBN);

            this.HasOptional(b => b.Image)
                .WithOptionalPrincipal(i => i.Book)
                .WillCascadeOnDelete(true);
        }
    }
}
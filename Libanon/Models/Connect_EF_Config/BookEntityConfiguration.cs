using System.Data.Entity.ModelConfiguration;

namespace Libanon.Models.Connect_EF_Config
{
    public class BookEntityConfiguration: EntityTypeConfiguration<Book>
    {
        public BookEntityConfiguration()
        {
            this.ToTable("Books");
            this.HasKey<int>(b => b.ISBN);

            this.HasMany<Image>(b => b.ImagesList)
                .WithRequired(i => i.Book)
                .HasForeignKey<int?>(i => i.BookISBN)
                .WillCascadeOnDelete();
        }
    }
}
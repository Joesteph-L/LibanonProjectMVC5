using System.Data.Entity.ModelConfiguration;

namespace Libanon.Models.Connect_EF_Config
{
    public class ImageEntityConfiguration: EntityTypeConfiguration<Image>
    {
        public ImageEntityConfiguration()
        {
            this.ToTable("Images");
            this.HasKey<int>(i => i.ImageID);
        }
    }
}
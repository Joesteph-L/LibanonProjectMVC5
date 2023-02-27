using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

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
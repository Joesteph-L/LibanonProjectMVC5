using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Libanon.Models.Connect_EF_Config
{
    public class UserEntityConfiguration: EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            this.ToTable("Users");
            this.HasKey<string>(u => u.Id);
            //this.Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
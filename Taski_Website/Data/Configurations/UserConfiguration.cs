using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taski_Website.Model;

namespace Taski_Website.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<TaskiUser>
    {
        public void Configure(EntityTypeBuilder<TaskiUser> builder)
        {
            builder.HasKey(p => p.UserId);
            builder.Property(p => p.Email).HasColumnName("Email");
            //builder.Property(p => p.UserName).HasColumnName("UserName");
        }
    }
}

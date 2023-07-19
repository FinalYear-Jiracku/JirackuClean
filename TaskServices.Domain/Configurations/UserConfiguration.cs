using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskServices.Domain.Entities;

namespace TaskServices.Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Image).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.InviteToken).IsRequired(false).HasMaxLength(200);
        }
    }
}

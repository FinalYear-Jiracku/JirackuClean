using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Domain.Entities;

namespace UserServices.Domain.Configurations
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
            builder.Property(x => x.RefreshToken).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CodeSmS).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Role).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Image).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Phone).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.SecretKey).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.IsOtp).IsRequired(false);
            builder.Property(x => x.IsSms).IsRequired(false);
            builder.Property(x => x.ExpiredCodeSms).IsRequired(false);
            builder.Property(x => x.RefreshTokenExpiryTime).IsRequired(false);
            builder.Property(x => x.IsDeleted).IsRequired(false);
            builder.Property(x => x.CreatedAt).IsRequired(false);
        }
    }
}

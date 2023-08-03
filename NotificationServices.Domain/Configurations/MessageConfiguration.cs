using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationServices.Domain.Entities;

namespace NotificationServices.Domain.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Group).WithMany(x => x.Messages).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User).WithMany(x => x.Messages).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.Content).IsRequired(false).HasMaxLength(1000);
            builder.Property(x => x.CreatedAt).IsRequired(false);
        }
    }
}

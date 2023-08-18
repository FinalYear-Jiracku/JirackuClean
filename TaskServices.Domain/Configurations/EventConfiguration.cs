using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Entities;

namespace TaskServices.Domain.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<EventCalendar>
    {
        public void Configure(EntityTypeBuilder<EventCalendar> builder)
        {
            builder.ToTable("Events");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Title).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Link).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.StartTime).IsRequired(false);
            builder.Property(x => x.EndTime).IsRequired(false);
            builder.Property(x => x.IsDeleted).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).IsRequired(false);
            builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.UpdatedAt).IsRequired(false);

            builder.HasOne(x => x.Project).WithMany(x => x.Events).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

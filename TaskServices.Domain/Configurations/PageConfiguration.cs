using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskServices.Domain.Entities;

namespace TaskServices.Domain.Configurations
{
    public class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.ToTable("Pages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Title).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Content).IsRequired(false).HasMaxLength(2000);
            builder.Property(x => x.ParentPageId).IsRequired(false);
            builder.Property(x => x.UserId).IsRequired(false);
            builder.Property(x => x.IsDeleted).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).IsRequired(true);

            builder.HasOne(x => x.Sprint).WithMany(x => x.Pages).HasForeignKey(x => x.SprintId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

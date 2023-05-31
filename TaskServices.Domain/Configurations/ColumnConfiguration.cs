using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskServices.Domain.Entities;

namespace TaskServices.Domain.Configurations
{
    public class ColumnConfiguration : IEntityTypeConfiguration<Column>
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.ToTable("Columns");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Color).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.IsDeleted).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).IsRequired(false);
            builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.UpdatedAt).IsRequired(false);

            builder.HasOne(x => x.Sprint).WithMany(x => x.Columns).HasForeignKey(x => x.SprintId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

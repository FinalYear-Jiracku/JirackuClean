using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskServices.Domain.Entities;

namespace TaskServices.Domain.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Content).IsRequired(false).HasMaxLength(1000);
            builder.Property(x => x.IsDeleted).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).IsRequired(true);

            builder.HasOne(x => x.Issue).WithMany(x => x.Comments).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.SubIssue).WithMany(x => x.Comments).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Note).WithMany(x => x.Comments).OnDelete(DeleteBehavior.SetNull);
        }
    }
}

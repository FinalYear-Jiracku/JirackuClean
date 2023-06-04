﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskServices.Domain.Entities;

namespace TaskServices.Domain.Configurations
{
    public class SubIssueConfiguration : IEntityTypeConfiguration<SubIssue>
    {
        public void Configure(EntityTypeBuilder<SubIssue> builder)
        {
            builder.ToTable("SubIssues");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(1000);
            builder.Property(x => x.Type).IsRequired(false);
            builder.Property(x => x.Priority).IsRequired(false);
            builder.Property(x => x.StoryPoint).IsRequired(false);
            builder.Property(x => x.Order).IsRequired(false);
            builder.Property(x => x.StartDate).IsRequired(false);
            builder.Property(x => x.DueDate).IsRequired(false);
            builder.Property(x => x.IsDeleted).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).IsRequired(false);
            builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.UpdatedAt).IsRequired(false);

            builder.HasOne(x => x.Status).WithMany(x => x.SubIssues).HasForeignKey(x => x.StatusId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Issue).WithMany(x => x.SubIssues).HasForeignKey(x => x.IssueId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

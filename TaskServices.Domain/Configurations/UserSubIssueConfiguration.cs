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
    public class UserSubIssueConfiguration : IEntityTypeConfiguration<UserSubIssue>
    {
        public void Configure(EntityTypeBuilder<UserSubIssue> builder)
        {
            builder.ToTable("UserSubIssue");
            builder.HasKey(x => new { x.SubIssueId, x.UserId });
            builder.HasOne(x => x.SubIssue).WithMany(x => x.UserSubIssues).HasForeignKey(x => x.SubIssueId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User).WithMany(x => x.UserSubIssues).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.JoinDate).IsRequired(false);
        }
    }
}

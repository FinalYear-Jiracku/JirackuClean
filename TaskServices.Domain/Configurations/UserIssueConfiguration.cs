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
    public class UserIssueConfiguration : IEntityTypeConfiguration<UserIssue>
    {
        public void Configure(EntityTypeBuilder<UserIssue> builder)
        {
            builder.ToTable("UserIssue");
            builder.HasKey(x => new { x.IssueId, x.UserId });
            builder.HasOne(x => x.Issue).WithMany(x => x.UserIssues).HasForeignKey(x => x.IssueId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User).WithMany(x => x.UserIssues).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.JoinDate).IsRequired(false);
        }
    }
}

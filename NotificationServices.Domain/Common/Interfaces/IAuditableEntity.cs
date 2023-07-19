using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Domain.Common.Interfaces
{
    public interface IAuditableEntity : IEntity
    {
        string? CreatedBy { get; set; }
        DateTimeOffset? CreatedAt { get; set; }
        string? UpdatedBy { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
    }
}
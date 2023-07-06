using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServices.Domain.Common.Interfaces
{
    public interface IAuditableEntity : IEntity
    {
        DateTimeOffset? CreatedAt { get; set; }
    }
}

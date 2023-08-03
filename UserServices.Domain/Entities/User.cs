using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Domain.Common;

namespace UserServices.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public string? RefreshToken { get; set; }
        public string? CustomerId { get; set; }
        public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
    }
}

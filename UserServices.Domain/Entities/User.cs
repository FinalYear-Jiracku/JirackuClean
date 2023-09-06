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
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }
        public string? SecretKey { get; set; }
        public bool? IsOtp { get; set; } = false;
        public string? CodeSmS { get; set; }
        public bool? IsSms { get; set; } = false;
        public DateTimeOffset? ExpiredCodeSms { get; set; }
        public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
    }
}

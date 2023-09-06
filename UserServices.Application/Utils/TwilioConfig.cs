using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServices.Application.Utils
{
    public class TwilioConfig
    {
        public string? AccountSid { get; set; }
        public string? AuthToken { get; set; }
        public string? TwilioPhoneNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServices.Application.DTOs
{
    public class QRCodeDTO
    {
        public string? Secret { get; set; }
        public string? QrCodeUrl { get; set; }
    }
}

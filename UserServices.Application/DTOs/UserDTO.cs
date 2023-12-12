﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServices.Application.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public bool? IsOtp { get; set; }
        public bool? IsSms { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

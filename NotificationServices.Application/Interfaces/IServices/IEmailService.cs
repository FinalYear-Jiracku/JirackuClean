﻿using NotificationServices.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Interfaces.IServices
{
    public interface IEmailService
    {
        Task SendEmail(EmailDTO request);
    }
}

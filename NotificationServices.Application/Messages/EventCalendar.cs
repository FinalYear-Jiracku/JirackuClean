﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Messages
{
    public class EventCalendar
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public int ProjectId { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
    }
}

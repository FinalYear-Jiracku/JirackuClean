﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Messages
{
    public class DeadlineIssue
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? StoryPoint { get; set; }
        public string? Status { get; set; }
        public string? Sprint { get; set; }
        public string? User { get; set; }
        public int ProjectId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
    }
}

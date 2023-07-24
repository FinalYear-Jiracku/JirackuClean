﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Message
{
    public class DeadlineSubIssue
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? StoryPoint { get; set; }
        public string? Status { get; set; }
        public string? Issue { get; set; }
        public string? User { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
    }
}

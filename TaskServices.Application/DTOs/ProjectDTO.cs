﻿namespace TaskServices.Application.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsUpgraded { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Sprints { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}

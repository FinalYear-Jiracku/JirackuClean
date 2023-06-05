using TaskServices.Domain.Entities.Enums;

namespace TaskServices.Shared.Pagination.Filter
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
        public IssueType? Type { get; set; }
        public IssuePriority? Priority { get; set; }
        public int? StatusId { get; set; }
        public int? UserId { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = PageSize == 0 ? 8 : PageSize;
        }
        public PaginationFilter(int pageNumber, int pageSize, string? search, IssueType? type, IssuePriority? priority, int? statusId, int? userId)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize == 0 ? 8 : pageSize;
            this.Search = search;
            this.Type = type;
            this.Priority = priority;
            this.StatusId = statusId;
            this.UserId = userId;
        }
    }
}

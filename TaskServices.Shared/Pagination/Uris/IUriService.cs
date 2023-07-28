using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Shared.Pagination.Uris
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}

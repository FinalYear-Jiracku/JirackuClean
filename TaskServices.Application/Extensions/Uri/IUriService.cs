using KPTMockProject.Common.Filter;

namespace KPTMockProject.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);

    }
}

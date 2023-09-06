using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Shared.Pagination.Filter;
using UserServices.Shared.Pagination.Uris;
using UserServices.Shared.Pagination.Wrapper;

namespace UserServices.Shared.Pagination.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilter validFilter, int totalRecords, IUriService uriService, string route)
        {
            var respose = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            int totalRecordsOfCurrentPage = totalRecords % validFilter.PageSize;
            respose.NextUrl = validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                               ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.Search), route)
                               : null;
            respose.NextPage = validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages ? validFilter.PageNumber + 1 : 0;
            respose.PreviousUrl = validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                                   ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize, validFilter.Search), route)
                                   : uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize, validFilter.Search), route);
            respose.PreviousPage = validFilter.PageNumber - 1;
            respose.FirstUrl = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize, validFilter.Search), route);
            respose.LastUrl = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize, validFilter.Search), route);
            respose.FirstPage = 1;
            respose.LastPage = roundedTotalPages;
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            respose.TotalRecordsOfCurrentPage = totalRecordsOfCurrentPage == 0 ? roundedTotalPages - 1 : totalRecordsOfCurrentPage;
            return respose;
        }

    }
}

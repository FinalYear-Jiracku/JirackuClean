using TaskServices.Domain.Entities;
using TaskServices.Shared.Pagination.Filter;
using TaskServices.Shared.Pagination.Wrapper;

namespace TaskServices.Persistence.Services
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key, T data, DateTimeOffset expirationTime);
        object RemoveData<T>(string key);
    }
}

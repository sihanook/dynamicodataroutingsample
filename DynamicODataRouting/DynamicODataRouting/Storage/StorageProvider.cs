using DynamicODataRouting.Models;

namespace DynamicODataRouting.Storage
{
    public abstract class StorageProvider<T>
        where T : ICommonEntity
    {
        public abstract Task<IQueryable<T>> GetAllDataAsync();
    }
}

using DynamicODataRouting.Models;

namespace DynamicODataRouting.Storage
{
    public class TwoStorageProvider : StorageProvider<Two>
    {
        private readonly List<Two> _two = new List<Two>
        {
            new Two
            {
                Id = 1,
                IsValid = true,
            },
            new Two
            {
                Id = 2,
                IsValid = false,
            }
        };

        public override Task<IQueryable<Two>> GetAllDataAsync()
        {
            return Task.FromResult(_two.AsQueryable());
        }
    }
}

using DynamicODataRouting.Models;

namespace DynamicODataRouting.Storage
{
    public class OneStorageProvider : StorageProvider<One>
    {
        private readonly List<One> _one = new List<One>
        {
            new One
            {
                Id = 1,
                Name = "A",
                Description = "Description X",
            },
            new One
            {
                Id = 2,
                Name = "B",
                Description = "Description Y",
            }
        };

        public override Task<IQueryable<One>> GetAllDataAsync()
        {
            return Task.FromResult(_one.AsQueryable());
        }
    }
}

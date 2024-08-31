using DynamicODataRouting.Models;
using DynamicODataRouting.Storage;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DynamicODataRouting.Controllers
{
    public class CommonController<T> : ODataController
        where T : ICommonEntity
    {
        private readonly OneStorageProvider oneStorageProvider;
        private readonly TwoStorageProvider twoStorageProvider;

        public CommonController(
            OneStorageProvider oneStorageProvider,
            TwoStorageProvider twoStorageProvider)
        {
            this.oneStorageProvider = oneStorageProvider;
            this.twoStorageProvider = twoStorageProvider;
        }

        public async Task<IActionResult> GetAsync(ODataQueryOptions<T> queryOptions)
        {
            Response response = new Response();
            response.Metadata = new List<string>()
            {
                "One",
                "Two",
            };

            if (typeof(T) == typeof(One))
            {
                IQueryable<One> result = await oneStorageProvider.GetAllDataAsync();
                response.Content = queryOptions.ApplyTo(result);
            }
            else if (typeof(T) == typeof(Two))
            {
                IQueryable<Two> result = await twoStorageProvider.GetAllDataAsync();
                response.Content = queryOptions.ApplyTo(result);
            }
            else
            {
                return BadRequest();
            }

            return Ok(response);
        }
    }
}

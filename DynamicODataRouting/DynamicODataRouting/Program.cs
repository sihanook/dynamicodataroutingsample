using DynamicODataRouting.Models;
using DynamicODataRouting.Routing;
using DynamicODataRouting.Storage;

using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace DynamicODataRouting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<One>("One");
            modelBuilder.EntitySet<Two>("Two");
            IEdmModel edmModel = modelBuilder.GetEdmModel();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddMvc(o => o.Conventions.Add(new CommonControllerModelConvention(edmModel)))
                    .ConfigureApplicationPartManager(manager =>
                    {
                        manager.FeatureProviders.Add(new CommonControllerFeatureProvider());
                    })
                    .AddOData(options =>
                    {
                        options.Filter().Count().Select().OrderBy()
                               .AddRouteComponents(
                                   "api",
                                   edmModel);
                    });

            builder.Services.AddSingleton<OneStorageProvider>();
            builder.Services.AddSingleton<TwoStorageProvider>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}

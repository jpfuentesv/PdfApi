using Microsoft.OpenApi.Models;
using PdfApi.Services;
using PdfApi.Services.Interface;
using PdfApi.Swagger.Examples;
using Swashbuckle.AspNetCore.Filters;

namespace PdfApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.PropertyNamingPolicy = null
                );

            services.AddScoped<IPdfService, PdfService>();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "PdfApi", Version = "v1" });
                options.ExampleFilters();
            });

            services.AddSwaggerExamplesFromAssemblyOf<PdfHtmlRequestExample>();

            return services;
        }
    }
}

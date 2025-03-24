using Microsoft.OpenApi.Models;
using PdfApi.Services;
using PdfApi.SwaggerExamples;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder
    .Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddEndpointsApiExplorer();

// Documentación Swagger/OpenAPI
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "PdfApi", Version = "v1" });
    opt.ExampleFilters(); // habilita ejemplos personalizados
});

// Registrar los ejemplos para Swagger
builder.Services.AddSwaggerExamplesFromAssemblyOf<HtmlRequestExample>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "PdfApi v1");
        opt.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();
app.Run();

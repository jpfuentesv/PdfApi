using PdfApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices();

WebApplication app = builder.Build();

app.ConfigureSwaggerAndMiddleware();

await app.RunAsync();

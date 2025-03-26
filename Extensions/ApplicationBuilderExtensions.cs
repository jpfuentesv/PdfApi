namespace PdfApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication ConfigureSwaggerAndMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "PdfApi v1");
                    options.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}

using PdfApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace PdfApi.SwaggerExamples;

public class HtmlRequestExample : IExamplesProvider<HtmlRequest>
{
    public HtmlRequest GetExamples() =>
        new(
            """
            <!DOCTYPE html>
            <html>
            <head><title>Demo</title></head>
            <body>
                <h1 style='color: green;'>PDF generado desde Swagger</h1>
                <p>Este contenido viene del ejemplo configurado.</p>
            </body>
            </html>
            """,
            "oficio",
            "pdf-demo-swagger"
        );
}

using PdfApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace PdfApi.Swagger.Examples
{
    public class PdfHtmlRequestExample : IExamplesProvider<PdfHtmlRequest>
    {
        public PdfHtmlRequest GetExamples() =>
            new PdfHtmlRequest
            {
                Html = """
                    <!DOCTYPE html>
                    <html>
                    <head><title>Demo</title></head>
                    <body>
                        <h1 style='color: green;'>PDF generado desde Swagger</h1>
                        <p>Este contenido viene del ejemplo configurado.</p>
                    </body>
                    </html>
                    """,
                PageSize = "oficio",
                FileName = "pdf-demo-swagger",
            };
    }
}

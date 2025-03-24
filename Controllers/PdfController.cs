using Microsoft.AspNetCore.Mvc;
using PdfApi.Models;
using PdfApi.Services;
using PdfApi.SwaggerExamples;
using PdfApi.Utils;
using Swashbuckle.AspNetCore.Filters;

namespace PdfApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PdfController(IPdfService pdfService) : ControllerBase
{
    [HttpPost("html-json")]
    [SwaggerRequestExample(typeof(HtmlRequest), typeof(HtmlRequestExample))]
    public IActionResult GenerarPdfDesdeHtml([FromBody] HtmlRequest request)
    {
        var validationResult = RequestValidator.Validar(request);
        if (validationResult is not null)
            return validationResult;

        var pdf = pdfService.CrearPdfDesdeHtml(request.Html, request.PageSize);
        return File(pdf, "application/pdf", $"{request.FileName}.pdf");
    }
}

using Microsoft.AspNetCore.Mvc;
using PdfApi.Models;
using PdfApi.Services;
using PdfApi.SwaggerExamples;
using PdfApi.Utils;
using Swashbuckle.AspNetCore.Filters;

namespace PdfApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PdfController : ControllerBase
{
    private readonly IPdfService _pdfService;

    public PdfController(IPdfService pdfService)
    {
        _pdfService = pdfService;
    }

    // Endpoint para generar un PDF a partir de un string HTML
    [HttpPost("html-json")]
    [SwaggerRequestExample(typeof(HtmlRequest), typeof(HtmlRequestExample))]
    [Produces("application/pdf")]
    [Consumes("application/json")]
    public IActionResult GenerarPdfDesdeHtml([FromBody] HtmlRequest request)
    {
        IActionResult? validationResult = RequestValidator.Validar(request);
        if (validationResult is not null)
            return validationResult;

        byte[] pdfBytes = _pdfService.CrearPdfDesdeHtml(request.Html, request.PageSize);
        string fileName = $"{request.FileName}.pdf";

        return File(pdfBytes, "application/pdf", fileName);
    }

    // Endpoint para generar un PDF a partir de un archivo HTML
    [HttpPost("html-file")]
    [Produces("application/pdf")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> GenerarPdfDesdeArchivo(
        IFormFile htmlFile,
        [FromForm] string pageSize,
        [FromForm] string fileName
    )
    {
        var (htmlContent, error) = await RequestValidator.ValidarArchivo(
            htmlFile,
            fileName,
            pageSize
        );
        if (error is not null)
            return error;

        byte[] pdf = _pdfService.CrearPdfDesdeHtml(htmlContent!, pageSize);
        return File(pdf, "application/pdf", $"{fileName}.pdf");
    }
}

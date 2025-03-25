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

    [HttpPost("html-json")]
    [SwaggerRequestExample(typeof(HtmlRequest), typeof(HtmlRequestExample))]
    [Produces("application/pdf")]
    public IActionResult GenerarPdfDesdeHtml([FromBody] HtmlRequest request)
    {
        IActionResult? validationResult = RequestValidator.Validar(request);
        if (validationResult is not null)
            return validationResult;

        byte[] pdfBytes = _pdfService.CrearPdfDesdeHtml(request.Html, request.PageSize);
        string fileName = $"{request.FileName}.pdf";

        return File(pdfBytes, "application/pdf", fileName);
    }
}

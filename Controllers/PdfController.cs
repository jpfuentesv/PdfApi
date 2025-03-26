using Microsoft.AspNetCore.Mvc;
using PdfApi.Models;
using PdfApi.Services.Interface;
using PdfApi.Swagger.Examples;
using Swashbuckle.AspNetCore.Filters;

namespace PdfApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly IPdfService _pdfService;

        public PdfController(IPdfService pdfService) => _pdfService = pdfService;

        // Endpoint para recibir HTML como string en JSON
        [HttpPost("html-json")]
        [SwaggerRequestExample(typeof(PdfHtmlRequest), typeof(PdfHtmlRequestExample))]
        [Produces("application/pdf")]
        [Consumes("application/json")]
        public IActionResult GenerarPdfDesdeHtml([FromBody] PdfHtmlRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            byte[] pdfBytes = _pdfService.CrearPdfDesdeHtml(request.Html, request.PageSize);
            return File(pdfBytes, "application/pdf", $"{request.FileName}.pdf");
        }

        // Endpoint para recibir un archivo HTML mediante multipart/form-data
        [HttpPost("html-file")]
        [Produces("application/pdf")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GenerarPdfDesdeArchivo([FromForm] PdfFileRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (StreamReader reader = new StreamReader(request.HtmlFile.OpenReadStream()))
            {
                string content = await reader.ReadToEndAsync();
                byte[] pdfBytes = _pdfService.CrearPdfDesdeHtml(content, request.PageSize);
                return File(pdfBytes, "application/pdf", $"{request.FileName}.pdf");
            }
        }
    }
}

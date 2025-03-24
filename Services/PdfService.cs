using iText.Html2pdf;
using iText.Kernel.Pdf;
using PdfApi.Models;

namespace PdfApi.Services;

public class PdfService : IPdfService
{
    public byte[] CrearPdfDesdeHtml(string html, string pageSize)
    {
        using var ms = new MemoryStream();
        var writer = new PdfWriter(ms);
        var pdf = new PdfDocument(writer);
        pdf.SetDefaultPageSize(PageSizeOption.ObtenerTamaño(pageSize));

        var props = new ConverterProperties();
        HtmlConverter.ConvertToPdf(html, pdf, props);

        return ms.ToArray();
    }
}

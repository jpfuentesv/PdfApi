using iText.Html2pdf;
using iText.Kernel.Pdf;
using PdfApi.Models;

namespace PdfApi.Services;

public class PdfService : IPdfService
{
    public byte[] CrearPdfDesdeHtml(string html, string pageSize)
    {
        using MemoryStream ms = new MemoryStream();
        PdfWriter writer = new PdfWriter(ms);
        PdfDocument pdf = new PdfDocument(writer);
        pdf.SetDefaultPageSize(PageSizeOption.ObtenerTamaño(pageSize));

        ConverterProperties props = new ConverterProperties();
        HtmlConverter.ConvertToPdf(html, pdf, props);

        return ms.ToArray();
    }
}

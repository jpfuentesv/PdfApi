namespace PdfApi.Services;

public interface IPdfService
{
    byte[] CrearPdfDesdeHtml(string html, string pageSize);
}

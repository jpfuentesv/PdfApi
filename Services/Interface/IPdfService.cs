namespace PdfApi.Services.Interface;

public interface IPdfService
{
    byte[] CrearPdfDesdeHtml(string html, string pageSize);
}

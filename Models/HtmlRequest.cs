namespace PdfApi.Models;

public record HtmlRequest(string Html, string PageSize = "carta", string? FileName = null);

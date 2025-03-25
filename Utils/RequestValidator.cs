using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfApi.Models;

namespace PdfApi.Utils;

public static class RequestValidator
{
    public static IActionResult? Validar(HtmlRequest req)
    {
        if (!req.Html.TrimStart().StartsWith("<!DOCTYPE html", StringComparison.OrdinalIgnoreCase))
            return Error("El contenido no parece ser un HTML válido.");

        if (string.IsNullOrWhiteSpace(req.FileName))
            return Error("El campo 'fileName' es obligatorio y no puede estar vacío.");

        if (!PageSizeOption.EsValido(req.PageSize))
            return Error("El campo 'pageSize' debe ser 'carta', 'oficio' o 'a4'.");

        return null;
    }

    public static async Task<(string? Html, IActionResult? Error)> ValidarArchivo(
        IFormFile file,
        string fileName,
        string pageSize
    )
    {
        if (file == null || file.Length == 0)
            return (null, Error("Debe adjuntar un archivo HTML."));

        if (string.IsNullOrWhiteSpace(fileName))
            return (null, Error("El nombre del archivo (fileName) es obligatorio."));

        if (!PageSizeOption.EsValido(pageSize))
            return (null, Error("El tamaño de página debe ser 'carta', 'oficio' o 'a4'."));

        using StreamReader reader = new(file.OpenReadStream());
        string content = await reader.ReadToEndAsync();

        if (!content.TrimStart().StartsWith("<!DOCTYPE html", StringComparison.OrdinalIgnoreCase))
            return (null, Error("El archivo no contiene un HTML válido."));

        return (content, null);
    }

    private static IActionResult Error(string mensaje) =>
        new BadRequestObjectResult(new { error = mensaje });
}

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

    private static IActionResult Error(string mensaje) =>
        new ContentResult
        {
            StatusCode = StatusCodes.Status400BadRequest,
            ContentType = "application/json",
            Content = $"{{ \"error\": \"{mensaje}\" }}",
        };
}

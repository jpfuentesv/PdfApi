using iText.Kernel.Geom;

namespace PdfApi.Models;

public static class PageSizeOption
{
    public const string Carta = "carta";
    public const string Oficio = "oficio";
    public const string A4 = "a4";

    private static readonly HashSet<string> _valid = new(StringComparer.OrdinalIgnoreCase)
    {
        Carta,
        Oficio,
        A4,
    };

    public static bool EsValido(string? valor) =>
        !string.IsNullOrWhiteSpace(valor) && _valid.Contains(valor);

    public static PageSize ObtenerTamaño(string valor) =>
        valor.ToLower() switch
        {
            Carta => PageSize.LETTER,
            A4 => PageSize.A4,
            _ => PageSize.LEGAL,
        };
}

using System.ComponentModel.DataAnnotations;

namespace PdfApi.Models
{
    public class PdfHtmlRequest
    {
        [Required(ErrorMessage = "El campo 'html' es obligatorio")]
        public string Html { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'pageSize' es obligatorio")]
        [RegularExpression(
            "(?i)^(carta|oficio|a4)$",
            ErrorMessage = "El campo 'pageSize' debe ser 'carta', 'oficio' o 'a4'"
        )]
        public string PageSize { get; set; } = "oficio";

        [Required(ErrorMessage = "El campo 'fileName' es obligatorio")]
        public string FileName { get; set; } = default!;
    }
}

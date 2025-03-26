using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PdfApi.Models
{
    public class PdfFileRequest
    {
        [Required(ErrorMessage = "El campo 'htmlFile' es obligatorio")]
        public IFormFile HtmlFile { get; set; } = default!;

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

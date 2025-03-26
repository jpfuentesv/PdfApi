# PdfApi - Generación de PDF desde HTML en ASP.NET Core

Este proyecto es una API REST construida en **ASP.NET Core 8.0** que permite generar documentos PDF a partir de contenido HTML enviado en una petición `POST`. Utiliza **iText 7** para la conversión de HTML a PDF y **Swagger** para documentar y probar los endpoints.

---

## Características principales

- Generación de PDF desde HTML con estilos CSS.
- Elección de tamaño de página: `carta`, `oficio` o `a4`.
- Nombre de archivo personalizable.
- Validación del contenido HTML.
- Soporte para envío de HTML como archivo `.html` o como string.
- Documentación interactiva con Swagger UI.

---

## Requisitos

- .NET 8 SDK
- Postman o Swagger para pruebas

---

## Instalación

```bash
# Clonar el repositorio
git clone https://github.com/jpfuentesv/PdfApi
cd PdfApi

# Restaurar dependencias y compilar
dotnet restore
dotnet run
```

Una vez iniciado, accede a Swagger UI en:

```
http://localhost:5158/
```

---

## Endpoints disponibles

### 🧾 POST `/api/pdf/html-json`
**Envía HTML como string dentro de un JSON.**

#### Body JSON esperado:
```json
{
  "html": "<!DOCTYPE html><html>...</html>",
  "pageSize": "oficio",
  "fileName": "demo-certificado"
}
```

#### Validaciones:
- `html`: debe ser un documento HTML válido (`<!DOCTYPE html>` obligatorio).
- `fileName`: no puede estar vacío.
- `pageSize`: debe ser `carta`, `oficio` o `a4`.

---

### 📎 POST `/api/pdf/html-file`
**Envía un archivo HTML (`.html`) junto con los parámetros.**

#### Formulario esperado (`multipart/form-data`):

| Campo      | Tipo   | Descripción                                |
|------------|--------|--------------------------------------------|
| htmlFile   | File   | Archivo `.html` con contenido válido.      |
| fileName   | Text   | Nombre del archivo PDF resultante.         |
| pageSize   | Text   | Tamaño de página: `carta`, `oficio`, `a4`. |

---

## Estructura del Proyecto

```
PdfApi/
│
├── Controllers/
│   └── PdfController.cs           # Controlador principal para generar PDFs
│
├── Extensions/
│   ├── ApplicationBuilderExtensions.cs   # Configuración de middleware (Swagger, etc.)
│   └── ServiceExtensions.cs              # Registro y configuración de servicios
│
├── Models/
│   ├── PdfFileRequest.cs          # Modelo para recibir archivo HTML vía multipart/form-data
│   ├── PdfHtmlRequest.cs          # Modelo para recibir HTML como string en JSON
│   └── PageSizeOption.cs          # Lógica para validar y obtener el tamaño de página (A4, oficio, etc.)
│
├── Services/
│   ├── Interface/
│   │   └── IPdfService.cs         # Interfaz del servicio de generación de PDF
│   └── PdfService.cs              # Implementación del servicio usando iText
│
├── Swagger/
│   └── Examples/
│       └── HtmlRequestExample.cs  # Ejemplo de request para Swagger UI
│
├── Program.cs                     # Punto de entrada de la aplicación (con minimal hosting)
├── appsettings.json
├── appsettings.Development.json
├── openapi.yaml                   # Definición OpenAPI (si la usas para documentar la API)
├── PdfApi.csproj                  # Archivo de proyecto de .NET
├── PdfApi.sln                     # Solución (si se usa para agrupar proyectos)
├── README.md                      # Documentación principal del proyecto
└── .gitignore                     # Lista de archivos y carpetas que Git debe ignorar
```

---

## Extensiones utilizadas

- `iText7` para generación de PDF.
- `Swashbuckle.AspNetCore` para Swagger UI.
- `Swashbuckle.AspNetCore.Filters` para incluir ejemplos.

---

## Ejemplo en Postman

### Opción 1: HTML embebido
- **Tipo**: `POST`
- **URL**: `http://localhost:5158/api/pdf/html-json`
- **Headers**: `Content-Type: application/json`
- **Body**:
```json
{
  "html": "<!DOCTYPE html><html><body><h1>Demo</h1></body></html>",
  "pageSize": "carta",
  "fileName": "demo-pdf"
}
```

### Opción 2: Archivo HTML
- **Tipo**: `POST`
- **URL**: `http://localhost:5158/api/pdf/html-file`
- **Body**: `form-data`

| Key      | Tipo | Valor                  |
|----------|------|------------------------|
| htmlFile | File | `demo_certificado.html`|
| fileName | Text | `certificado-final`    |
| pageSize | Text | `oficio`               |

---

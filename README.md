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
│   └── PdfController.cs         # Controlador principal
│
├── Models/
│   ├── HtmlRequest.cs          # Modelo de solicitud con HTML, tamaño y nombre
│   └── PageSizeOption.cs       # Validación y selección de tamaño de página
│
├── Services/
│   ├── IPdfService.cs          # Interfaz del servicio
│   └── PdfService.cs           # Implementación de PDF con iText
│
├── SwaggerExamples/
│   └── HtmlRequestExample.cs   # Ejemplo para Swagger UI
│
├── Utils/
│   └── RequestValidator.cs     # Validaciones de la solicitud
│
├── Program.cs                  # Configuración de la app
├── appsettings.json
├── appsettings.Development.json
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

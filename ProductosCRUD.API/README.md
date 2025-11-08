# Productos CRUD API

API REST para la gestión de productos construida con ASP.NET Core 9.0.

## 🚀 Características

- CRUD completo de productos
- Búsqueda de productos por nombre o descripción
- Eliminación lógica (soft delete)
- Documentación interactiva con Swagger
- Base de datos SQLite
- CORS habilitado para desarrollo

## 📋 Requisitos

- .NET 9.0 SDK o superior
- Visual Studio 2022 / VS Code / Rider (opcional)

## 🔧 Instalación y Ejecución

### 1. Restaurar dependencias

```bash
cd ProductosCRUD.API
dotnet restore
```

### 2. Ejecutar la aplicación

```bash
dotnet run
```

La API estará disponible en:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001
- **Swagger UI**: https://localhost:5001 (o http://localhost:5000)

## 📚 Endpoints

### Productos

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/productos` | Obtener todos los productos activos |
| GET | `/api/productos/{id}` | Obtener un producto por ID |
| POST | `/api/productos` | Crear un nuevo producto |
| PUT | `/api/productos/{id}` | Actualizar un producto existente |
| DELETE | `/api/productos/{id}` | Eliminar (desactivar) un producto |
| GET | `/api/productos/buscar?termino={texto}` | Buscar productos por término |

## 📝 Ejemplos de Uso

### Obtener todos los productos

```bash
curl -X GET "https://localhost:5001/api/productos"
```

### Crear un nuevo producto

```bash
curl -X POST "https://localhost:5001/api/productos" \
  -H "Content-Type: application/json" \
  -d '{
    "nombre": "Laptop HP",
    "descripcion": "Laptop HP 15 pulgadas",
    "precio": 15000.00,
    "stock": 10
  }'
```

### Actualizar un producto

```bash
curl -X PUT "https://localhost:5001/api/productos/1" \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "nombre": "Laptop HP Actualizada",
    "descripcion": "Laptop HP 15 pulgadas con SSD",
    "precio": 16000.00,
    "stock": 8
  }'
```

### Eliminar un producto

```bash
curl -X DELETE "https://localhost:5001/api/productos/1"
```

### Buscar productos

```bash
curl -X GET "https://localhost:5001/api/productos/buscar?termino=laptop"
```

## 🗂️ Estructura del Proyecto

```
ProductosCRUD.API/
├── Controllers/
│   └── ProductosController.cs    # Controlador de la API
├── DTOs/
│   └── ProductoDto.cs            # Data Transfer Objects
├── Program.cs                     # Configuración de la aplicación
├── appsettings.json              # Configuración
└── README.md                      # Este archivo
```

## 🔐 Modelos de Datos

### ProductoDto (Respuesta)

```json
{
  "id": 1,
  "nombre": "Laptop HP",
  "descripcion": "Laptop HP 15 pulgadas",
  "precio": 15000.00,
  "stock": 10,
  "fechaCreacion": "2024-11-08T15:30:00",
  "activo": true
}
```

### CrearProductoDto (Crear)

```json
{
  "nombre": "Laptop HP",
  "descripcion": "Laptop HP 15 pulgadas",
  "precio": 15000.00,
  "stock": 10
}
```

### ActualizarProductoDto (Actualizar)

```json
{
  "id": 1,
  "nombre": "Laptop HP",
  "descripcion": "Laptop HP 15 pulgadas",
  "precio": 15000.00,
  "stock": 10
}
```

## 🛠️ Tecnologías Utilizadas

- **ASP.NET Core 9.0** - Framework web
- **Entity Framework Core** - ORM
- **SQLite** - Base de datos
- **Swashbuckle** - Documentación Swagger/OpenAPI
- **C# 12** - Lenguaje de programación

## 🔄 Arquitectura

El proyecto sigue una arquitectura en capas:

- **ProductosCRUD.API** - Capa de presentación (API REST)
- **ProductosCRUD.Business** - Capa de lógica de negocio
- **ProductosCRUD.Data** - Capa de acceso a datos

## 📦 Base de Datos

La aplicación utiliza SQLite con el archivo `productos.db` que se crea automáticamente en la primera ejecución.

### Esquema de la tabla Productos

| Campo | Tipo | Descripción |
|-------|------|-------------|
| Id | INTEGER | Clave primaria |
| Nombre | TEXT | Nombre del producto (max 100 caracteres) |
| Descripcion | TEXT | Descripción del producto (max 500 caracteres) |
| Precio | DECIMAL | Precio del producto |
| Stock | INTEGER | Cantidad en inventario |
| FechaCreacion | DATETIME | Fecha de creación |
| Activo | BOOLEAN | Estado del producto |

## 🌐 CORS

Por defecto, CORS está configurado para permitir todas las solicitudes en desarrollo. Para producción, ajusta la política en `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("Production", policy =>
    {
        policy.WithOrigins("https://tu-dominio.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

## 🐛 Solución de Problemas

### Error de puerto en uso

Si el puerto está ocupado, puedes cambiar el puerto en `Properties/launchSettings.json` o usar:

```bash
dotnet run --urls "http://localhost:5050"
```

### Error de base de datos

Si hay problemas con la base de datos, elimina el archivo `productos.db` y reinicia la aplicación.

## 📄 Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.

## 👤 Autor

Tu Nombre - [tu@email.com](mailto:tu@email.com)

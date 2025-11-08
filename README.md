# 🛒 Sistema CRUD de Productos con Autenticación JWT

Sistema completo de gestión de productos desarrollado con .NET y Windows Forms, que incluye autenticación JWT, API REST y una interfaz de escritorio.

## 🚀 Características

- ✅ **API REST** con .NET 9.0
- ✅ **Autenticación JWT** (usuario y contraseña)
- ✅ **CRUD completo** de productos
- ✅ **Windows Forms** como frontend
- ✅ **SQLite** como base de datos
- ✅ **Arquitectura en capas** (API, Business, Data)
- ✅ **Swagger** para documentación de API
- ✅ **Eliminación lógica** (soft delete)

## 📋 Requisitos

- .NET 8.0 SDK o superior
- Visual Studio 2022 o VS Code
- Windows (para la aplicación Windows Forms)

## 🏗️ Estructura del Proyecto

```
ProjectCRUD/
├── ProductosCRUD.API/          # API REST
├── ProductosCRUD.Business/     # Lógica de negocio
├── ProductosCRUD.Data/         # Acceso a datos
└── ProyectoCRUD/              # Aplicación Windows Forms
```

## 🔧 Instalación

### 1. Clonar el repositorio

```bash
git clone https://github.com/TU_USUARIO/ProjectCRUD.git
cd ProjectCRUD
```

### 2. Restaurar dependencias

```bash
dotnet restore
```

### 3. Compilar el proyecto

```bash
dotnet build
```

## 🚀 Ejecución

### Iniciar la API

```bash
cd ProductosCRUD.API
dotnet run
```

La API estará disponible en: **http://localhost:5290**

### Iniciar la aplicación Windows Forms

```bash
cd ProyectoCRUD
dotnet run
```

## 🔐 Credenciales por Defecto

Al iniciar la API por primera vez, se crea automáticamente un usuario:

- **Usuario**: `admin`
- **Contraseña**: `admin123`

## 📚 Endpoints de la API

### Autenticación

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/auth/register` | Registrar nuevo usuario |
| POST | `/api/auth/login` | Iniciar sesión |

### Productos (Requieren autenticación)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/productos` | Listar todos los productos |
| GET | `/api/productos/{id}` | Obtener producto por ID |
| POST | `/api/productos` | Crear nuevo producto |
| PUT | `/api/productos/{id}` | Actualizar producto |
| DELETE | `/api/productos/{id}` | Eliminar producto (soft delete) |
| GET | `/api/productos/buscar?termino={termino}` | Buscar productos |

## 🧪 Probar la API

### Con Swagger

1. Abre http://localhost:5290 en tu navegador
2. Usa el botón "Authorize" para autenticarte
3. Prueba los endpoints

### Con PowerShell

```powershell
# Login
$body = @{
    nombreUsuario = "admin"
    password = "admin123"
} | ConvertTo-Json

$response = Invoke-RestMethod -Uri "http://localhost:5290/api/auth/login" -Method Post -Body $body -ContentType "application/json"
$token = $response.token

# Listar productos
$headers = @{
    Authorization = "Bearer $token"
}

Invoke-RestMethod -Uri "http://localhost:5290/api/productos" -Headers $headers
```

## 🎨 Aplicación Windows Forms

La aplicación incluye:

- **Login** con validación
- **CRUD de productos** con interfaz gráfica
- **Búsqueda** de productos
- **Validaciones** en tiempo real

## 🗄️ Base de Datos

- **Motor**: SQLite
- **Archivo**: `productos.db` (se crea automáticamente)
- **Tablas**:
  - `Productos`: Gestión de productos
  - `Usuarios`: Autenticación

## 🔒 Seguridad

- Contraseñas hasheadas con SHA256
- Tokens JWT con expiración de 60 minutos
- Todos los endpoints de productos protegidos
- Validación de datos en API y frontend

## 🛠️ Tecnologías Utilizadas

### Backend
- ASP.NET Core 9.0
- Entity Framework Core
- SQLite
- JWT Authentication
- Swagger/OpenAPI

### Frontend
- Windows Forms (.NET 8.0)
- HttpClient para consumo de API

## 📝 Notas

- La base de datos se crea automáticamente al iniciar la API
- El usuario `admin` se crea automáticamente si no existe
- Los tokens JWT expiran después de 60 minutos
- La eliminación de productos es lógica (soft delete)

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.

## 👤 Autor

Tu Nombre - [@tu_usuario](https://github.com/TU_USUARIO)

## 🙏 Agradecimientos

- Documentación de ASP.NET Core
- Comunidad de .NET

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductosCRUD.Business.Services;
using ProductosCRUD.Data.Context;
using ProductosCRUD.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext con SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=productos.db"));

// Registrar servicios y repositorios
builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<AuthService>();

// Configurar JWT Service
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? "MiClaveSecretaSuperSeguraDeAlMenos32Caracteres!";
var issuer = jwtSettings["Issuer"] ?? "ProductosCRUD.API";
var audience = jwtSettings["Audience"] ?? "ProductosCRUD.Client";

builder.Services.AddSingleton(new JwtService(secretKey, issuer, audience, 60));

// Agregar controladores
builder.Services.AddControllers();

// Configurar CORS para permitir acceso desde cualquier origen (ajustar en producción)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configurar autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddAuthorization();

// Configurar Swagger/OpenAPI con soporte para JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Productos CRUD API",
        Version = "v1",
        Description = "API REST para gestión de productos con autenticación JWT",
        Contact = new OpenApiContact
        {
            Name = "Tu Nombre",
            Email = "tu@email.com"
        }
    });

    // Configurar JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Crear la base de datos si no existe y agregar usuario por defecto
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    
    // Crear usuario admin por defecto si no existe usando SQL directo
    try
    {
        var connection = context.Database.GetDbConnection();
        await connection.OpenAsync();
        
        var checkCommand = connection.CreateCommand();
        checkCommand.CommandText = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = 'admin'";
        var count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
        
        if (count == 0)
        {
            var passwordHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes("admin123")));
            var insertCommand = connection.CreateCommand();
            insertCommand.CommandText = $"INSERT INTO Usuarios (NombreUsuario, PasswordHash, FechaCreacion, Activo) VALUES ('admin', '{passwordHash}', datetime('now'), 1)";
            await insertCommand.ExecuteNonQueryAsync();
            Console.WriteLine("✅ Usuario admin creado exitosamente (usuario: admin, contraseña: admin123)");
        }
        
        await connection.CloseAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ Error al crear usuario admin: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Productos CRUD API v1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz
    });
}

app.UseHttpsRedirection();

// Habilitar CORS
app.UseCors("AllowAll");

// Habilitar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

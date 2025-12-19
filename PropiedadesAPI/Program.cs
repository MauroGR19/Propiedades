/// <summary>
/// Archivo de configuración principal de la aplicación PropiedadesAPI
/// </summary>
/// <remarks>
/// Configura todos los servicios necesarios para la aplicación:
/// - Base de datos con Entity Framework
/// - Autenticación JWT
/// - Inyección de dependencias
/// - AutoMapper
/// - Swagger/OpenAPI
/// - Logging con Serilog
/// - Validaciones con FluentValidation
/// </remarks>

using Aplicacion.Interfaces;
using Aplicacion.Servicios.Interfaces;
using Aplicacion.Servicios.Servicio;
using Aplicacion.UseCase;
using AutoMapper;
using Datos.Contexto;
using Datos.Mapper;
using Datos.Operacion;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using DTO.Mapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PropiedadesAPI.Filtros;
using PropiedadesAPI.Middleware;
using PropiedadesAPI.Validators;
using Serilog;
using System.Text;

// Configuración de Serilog para logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/propiedades-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog como proveedor de logging
builder.Host.UseSerilog();

// ---------------------- Configuración de Servicios ----------------------
// Controladores MVC
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AutenticacionValidator>();

// Configuración de Base de Datos
var connectionString = builder.Configuration.GetConnectionString("PropiedadesConnection");
builder.Services.AddDbContext<PropiedadesContexto>(opt =>
{
    opt.UseSqlServer(connectionString);
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // Optimización para consultas
});

// Inyección de Dependencias - Repositorios (Capa de Datos)
builder.Services.AddTransient<IRepositorioAutenticacion<Autenticacion, string>, AutenticacionOperacion>();
builder.Services.AddTransient<IRepositorioPropiedad<Propiedad, int>, PropiedadOperacion>();
builder.Services.AddTransient<IRepositorioImagenPropiedad<ImagenPropiedad, int>, ImagenPropiedadOperacion>();
builder.Services.AddTransient<IRepositorioHistorialPropiedad<HistorialPropiedad, int>, HistorialPropiedadOperacion>();
builder.Services.AddTransient<IRepositorioPropietario<Propietario, int>, PropietarioOperacion>();

// Inyección de Dependencias - Casos de Uso (Capa de Aplicación)
builder.Services.AddTransient<IUseCaseAutenticacion<Autenticacion, string>, AutenticacionUseCase>();
builder.Services.AddTransient<IUseCasePropiedad<Propiedad, int>, PropiedadesUseCase>();
builder.Services.AddTransient<IUseCaseImagenPropiedad<ImagenPropiedad, int>, ImagenPropiedadUseCase>();
builder.Services.AddTransient<IUseCaseBase<HistorialPropiedad, int>, HistorialPropiedadUseCase>();
builder.Services.AddTransient<IUseCaseBase<Propietario, int>, PropietarioUseCase>();

// Configuración de AutoMapper para mapeo de objetos
builder.Services.AddAutoMapper(
    typeof(MappingProfileDTO).Assembly,   // Mapeo DTO <-> Dominio
    typeof(MappingProfile).Assembly       // Mapeo Dominio <-> Entidades EF
);

// Configurar Cache
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IServicioCache, ServicioCache>();


// Configuración de Autenticación JWT Bearer
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,           // Validar emisor del token
            ValidateAudience = true,         // Validar audiencia del token
            ValidateLifetime = true,         // Validar expiración del token
            ValidateIssuerSigningKey = true, // Validar firma del token
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// Configuración de Swagger/OpenAPI para documentación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "PropiedadesAPI", Version = "v1" });

    // Seguridad: Bearer
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Usa: Bearer {tu_token_jwt}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Header global opcional
    options.OperationFilter<AcepteLenguajeHeader>();
    
    // Incluir comentarios XML
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Inicialización de Base de Datos (solo en desarrollo)
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<PropiedadesContexto>();
    ctx.Database.EnsureCreated(); // Crea la BD si no existe
}

// ---------------------- Pipeline de Middleware ----------------------
// IMPORTANTE: Middleware de excepciones debe ir PRIMERO
app.UseMiddleware<MiddlewareExcepcionGlobal>();

// Configuración específica para entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    // Usamos nuestro middleware personalizado en lugar del por defecto
    // app.UseDeveloperExceptionPage();
    app.UseSwagger();   // Habilitar generación de documentación
    app.UseSwaggerUI(); // Habilitar interfaz de usuario de Swagger
}

// Configuración del pipeline de solicitudes HTTP
app.UseHttpsRedirection(); // Redirección HTTPS
app.UseStaticFiles();      // Archivos estáticos

// Orden importante: Autenticación antes que Autorización
app.UseAuthentication();   // Verificar identidad del usuario
app.UseAuthorization();    // Verificar permisos del usuario

// Mapeo de controladores
app.MapControllers();

// Iniciar la aplicación
app.Run();

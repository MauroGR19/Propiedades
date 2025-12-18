using System.Text;
using Aplicacion.Interfaces;
using Aplicacion.UseCase;
using AutoMapper;
using Datos.Contexto;
using Datos.Mapper;
using Datos.Operacion;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using DTO.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PropiedadesAPI.Filtros;
using PropiedadesAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ---------------------- Services ----------------------
builder.Services.AddControllers();

// DB
var connectionString = builder.Configuration.GetConnectionString("PropiedadesConnection");
builder.Services.AddDbContext<PropiedadesContexto>(opt =>
{
    opt.UseSqlServer(connectionString);
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Repositorios
builder.Services.AddTransient<IRepositorioAutenticacion<Autenticacion, string>, AutenticacionOperacion>();
builder.Services.AddTransient<IRepositorioPropiedad<Propiedad, int>, PropiedadOperacion>();
builder.Services.AddTransient<IRepositorioImagenPropiedad<ImagenPropiedad, int>, ImagenPropiedadOperacion>();
builder.Services.AddTransient<IRepositorioBase<HistorialPropiedad, int>, HistorialPropiedadOperacion>();
builder.Services.AddTransient<IRepositorioBase<Propietario, int>, PropietarioOperacion>();

// UseCases
builder.Services.AddTransient<IUseCaseAutenticacion<Autenticacion, string>, AutenticacionUseCase>();
builder.Services.AddTransient<IUseCasePropiedad<Propiedad, int>, PropiedadesUseCase>();
builder.Services.AddTransient<IUseCaseImagenPropiedad<ImagenPropiedad, int>, ImagenPropiedadUseCase>();
builder.Services.AddTransient<IUseCaseBase<HistorialPropiedad, int>, HistorialPropiedadUseCase>();
builder.Services.AddTransient<IUseCaseBase<Propietario, int>, PropietarioUseCase>();

// AutoMapper
builder.Services.AddAutoMapper(
    typeof(MappingProfileDTO).Assembly,   // DTO <-> Dominio
    typeof(MappingProfile).Assembly       // Dominio <-> Entidades EF
);

// Auth: JWT Bearer
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// Swagger
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
});

var app = builder.Build();

// Crear DB si no existe (dev)
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<PropiedadesContexto>();
    ctx.Database.EnsureCreated();
}

// ---------------------- Pipline ----------------------
// IMPORTANTE: Middleware de excepciones PRIMERO
app.UseMiddleware<MiddlewareExcepcionGlobal>();

if (app.Environment.IsDevelopment())
{
    // Comentamos DeveloperExceptionPage para que use nuestro middleware
    // app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();   // <-- Primero autenticación
app.UseAuthorization();    // <-- Luego autorización

app.MapControllers();

app.Run();

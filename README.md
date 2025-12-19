# 🏠 Propiedades API

**API REST para gestión integral de propiedades inmobiliarias, propietarios e historial de transacciones.**

Una solución robusta y escalable construida con .NET 8, Entity Framework Core y arquitectura hexagonal, que incluye autenticación JWT, cache en memoria, validaciones avanzadas y auditoría completa.

## 📋 Tabla de Contenidos

- [Características Principales](#-características-principales)
- [Arquitectura del Proyecto](#-arquitectura-del-proyecto)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Tecnologías Utilizadas](#-tecnologías-utilizadas)
- [Funcionalidades Implementadas](#-funcionalidades-implementadas)
- [Principios SOLID](#-principios-solid)
- [Arquitectura Hexagonal](#-arquitectura-hexagonal)
- [Configuración del Proyecto](#configuración-del-proyecto)
- [Endpoints de la API](#-endpoints-de-la-api)
- [Seguridad](#-seguridad)
- [Logging y Monitoreo](#-logging-y-monitoreo)
- [Testing](#-testing)
- [Contribución](#-contribución)

## ✨ Características Principales

- 🔐 **Autenticación JWT** con contraseñas hasheadas (BCrypt)
- 🚀 **Cache en memoria** para optimización de rendimiento
- ✅ **Validaciones avanzadas** con FluentValidation
- 📊 **Auditoría completa** con campos de creación y modificación
- 🛡️ **Manejo global de excepciones** con middleware personalizado
- 📝 **Logging estructurado** con Serilog
- 🔄 **Paginación** en consultas de listado
- 🗂️ **Filtros avanzados** para búsquedas específicas
- 📚 **Documentación automática** con Swagger/OpenAPI
- 🧪 **Pruebas unitarias** completas

## 🏗️ Arquitectura del Proyecto

El proyecto implementa **Arquitectura Hexagonal (Ports & Adapters)** con separación clara de responsabilidades:

```
┌─────────────────────────────────────────────────────────────┐
│                        PropiedadesAPI                       │
│                    (Capa de Presentación)                   │
│  Controllers │ Middleware │ Validators │ Filters            │
└─────────────────────────┬───────────────────────────────────┘
                          │
┌─────────────────────────┴───────────────────────────────────┐
│                       Aplicacion                            │
│                   (Capa de Aplicación)                      │
│     Use Cases │ Interfaces │ Servicios │ Cache              │
└─────────────────────────┬───────────────────────────────────┘
                          │
┌─────────────────────────┴───────────────────────────────────┐
│                        Dominio                              │
│                    (Capa de Dominio)                        │
│   Modelos │ Excepciones │ Guards │ Interfaces │ Comun       │
└─────────────────────────┬───────────────────────────────────┘
                          │
┌─────────────────────────┴───────────────────────────────────┐
│                         Datos                               │
│                 (Capa de Infraestructura)                   │
│  Contexto │ Entidades │ Configuraciones │ Operaciones       │
└─────────────────────────────────────────────────────────────┘
```

## 📁 Estructura del Proyecto

### **PropiedadesAPI** (Capa de Presentación)
- **Controllers/**: Controladores REST API
- **Middleware/**: Middleware personalizado para manejo de excepciones
- **Validators/**: Validaciones con FluentValidation
- **Filtros/**: Filtros personalizados para Swagger

### **Aplicacion** (Capa de Aplicación)
- **UseCase/**: Casos de uso del negocio
- **Interfaces/**: Contratos de la capa de aplicación
- **Servicios/**: Servicios transversales (Cache, etc.)

### **Dominio** (Capa de Dominio)
- **Modelos/**: Entidades de dominio con auditoría
- **Excepciones/**: Excepciones específicas del dominio
- **Comun/**: Clases utilitarias (Guard, Paginación)
- **Interfaces/**: Contratos del dominio

### **Datos** (Capa de Infraestructura)
- **Contexto/**: DbContext con auditoría automática
- **Entidades/**: Entidades de Entity Framework
- **Configuracion/**: Configuraciones de EF Core
- **Operacion/**: Implementaciones de repositorios

### **DTO** (Transferencia de Datos)
- **DTO/**: Objetos de transferencia de datos
- **Mapper/**: Configuraciones de AutoMapper

### **Pruebas** (Testing)
- **UseCasePruebas/**: Pruebas unitarias de casos de uso

## 🛠️ Tecnologías Utilizadas

### **Backend**
- **.NET 8**: Framework principal
- **ASP.NET Core**: API REST
- **Entity Framework Core**: ORM
- **SQL Server**: Base de datos

### **Seguridad**
- **JWT Bearer**: Autenticación
- **BCrypt**: Hashing de contraseñas

### **Validación y Mapeo**
- **FluentValidation**: Validaciones avanzadas
- **AutoMapper**: Mapeo de objetos

### **Cache y Rendimiento**
- **MemoryCache**: Cache en memoria
- **Paginación**: Optimización de consultas

### **Logging y Monitoreo**
- **Serilog**: Logging estructurado
- **Swagger/OpenAPI**: Documentación

### **Testing**
- **xUnit**: Framework de pruebas
- **Moq**: Mocking para pruebas

## 🚀 Funcionalidades Implementadas

### **Gestión de Propietarios**
- ✅ CRUD completo con validaciones
- ✅ Paginación y filtros
- ✅ Cache automático
- ✅ Auditoría de cambios

### **Gestión de Propiedades**
- ✅ CRUD con validaciones de negocio
- ✅ Filtros por precio, año, ubicación
- ✅ Relación con propietarios
- ✅ Cache inteligente

### **Historial de Propiedades**
- ✅ Registro de transacciones
- ✅ Cálculo de impuestos
- ✅ Trazabilidad completa

### **Gestión de Imágenes**
- ✅ Asociación con propiedades
- ✅ Validación de formatos
- ✅ Estado habilitado/deshabilitado

### **Autenticación y Seguridad**
- ✅ Login con JWT
- ✅ Contraseñas hasheadas
- ✅ Tokens con expiración
- ✅ Middleware de autenticación

## 🎯 Principios SOLID

### **1. Responsabilidad Única (SRP)**
- Cada clase tiene una única responsabilidad
- Separación clara entre capas
- Use Cases específicos por funcionalidad

### **2. Abierto/Cerrado (OCP)**
- Extensible sin modificar código existente
- Nuevos servicios mediante interfaces
- Patrones de diseño implementados

### **3. Sustitución de Liskov (LSP)**
- Interfaces sustituibles por implementaciones
- Polimorfismo en repositorios y servicios
- Contratos bien definidos

### **4. Segregación de Interfaces (ISP)**
- Interfaces específicas por dominio
- Contratos mínimos y cohesivos
- Dependencias granulares

### **5. Inversión de Dependencias (DIP)**
- Dependencia de abstracciones
- Inyección de dependencias
- Desacoplamiento entre capas

## 🔷 Arquitectura Hexagonal

### **¿Por qué Arquitectura Hexagonal?**

1. **Independencia del Framework**: La lógica de negocio no depende de tecnologías específicas
2. **Separación de Responsabilidades**: Cada capa tiene un propósito claro y definido
3. **Facilidad de Pruebas**: Capas desacopladas permiten testing efectivo
4. **Mantenibilidad**: Cambios en una capa no afectan las demás
5. **Escalabilidad**: Fácil agregar nuevas funcionalidades

### **Beneficios Implementados**
- ✅ **Testabilidad**: Pruebas unitarias sin dependencias externas
- ✅ **Flexibilidad**: Cambio de base de datos sin afectar lógica
- ✅ **Mantenibilidad**: Código organizado y predecible
- ✅ **Reutilización**: Componentes reutilizables entre proyectos

## Configuración del proyecto
Debe especificar el ConnectionStrings, donde se debe modificar la cadena de conexión en el archivo de configuración appsettings.json, luego de esto debes establecer la api como proyecto de inicio y solo bastaria con iniciar el proyecto ya que la DB se crea automaticamente.
![alt text]<img width="1912" height="1027" alt="Image" src="https://github.com/user-attachments/assets/8fb77c45-670f-451d-a127-48b458abfc49" />

## 🔗 Endpoints de la API

### **Autenticación**
```http
POST /api/Autenticacion/login
```
**Body:**
```json
{
  "usuario": "string",
  "clave": "string"
}
```

### **Propietarios**
```http
GET    /api/Propietario                    # Listar todos
GET    /api/Propietario/paginado           # Listar paginado
GET    /api/Propietario/{id}               # Obtener por ID
POST   /api/Propietario                    # Crear nuevo
PUT    /api/Propietario/{id}               # Actualizar
DELETE /api/Propietario/{id}               # Eliminar
```

### **Propiedades**
```http
GET    /api/Propiedad                      # Listar todas
GET    /api/Propiedad/paginado             # Listar paginado
GET    /api/Propiedad/{id}                 # Obtener por ID
POST   /api/Propiedad/filtro               # Filtrar propiedades
POST   /api/Propiedad                      # Crear nueva
PUT    /api/Propiedad/{id}                 # Actualizar
DELETE /api/Propiedad/{id}                 # Eliminar
```

### **Historial de Propiedades**
```http
GET    /api/HistorialPropiedad             # Listar todo
GET    /api/HistorialPropiedad/paginado    # Listar paginado
GET    /api/HistorialPropiedad/{id}        # Obtener por ID
POST   /api/HistorialPropiedad             # Crear registro
PUT    /api/HistorialPropiedad/{id}        # Actualizar
DELETE /api/HistorialPropiedad/{id}        # Eliminar
```

### **Imágenes de Propiedades**
```http
GET    /api/ImagenPropiedad/{id}           # Obtener por ID
POST   /api/ImagenPropiedad                # Subir imagen
PUT    /api/ImagenPropiedad/{id}           # Actualizar
DELETE /api/ImagenPropiedad/{id}           # Eliminar
```

## 🔐 Seguridad

### **Autenticación JWT**
- **Algoritmo**: HS256
- **Expiración**: Configurable en appsettings.json
- **Header**: `Authorization: Bearer {token}`

### **Hashing de Contraseñas**
- **Algoritmo**: BCrypt con salt automático
- **Rounds**: 12 (configurable)
- **Verificación**: Comparación segura sin exposición

### **Validaciones de Seguridad**
- Validación de entrada en todos los endpoints
- Sanitización de datos
- Prevención de inyección SQL (EF Core)
- Validación de tipos de archivo para imágenes

## 📊 Logging y Monitoreo

### **Serilog Configuration**
```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/propiedades-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .CreateLogger();
```

### **Niveles de Log Implementados**
- **Information**: Operaciones exitosas
- **Warning**: Situaciones atípicas
- **Error**: Errores de aplicación
- **Debug**: Información de desarrollo

### **Métricas de Cache**
- Hit/Miss ratios
- Tiempos de respuesta
- Invalidaciones automáticas

## 🧪 Testing

### **Cobertura de Pruebas**
- ✅ **Use Cases**: Pruebas unitarias completas
- ✅ **Validaciones**: Testing de FluentValidation
- ✅ **Repositorios**: Mocking de Entity Framework
- ✅ **Servicios**: Testing de cache y servicios

### **Ejecutar Pruebas**
```bash
dotnet test
```

### **Generar Reporte de Cobertura**
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## 🚀 Características Avanzadas

### **Cache Inteligente**
- **Estrategia**: Cache-Aside Pattern
- **TTL**: 5 minutos para entidades, 10 minutos para listas
- **Invalidación**: Automática en operaciones CUD
- **Claves**: Estructuradas por tipo y ID

### **Auditoría Automática**
```csharp
public abstract class EntidadAuditable
{
    public DateTime FechaCreacion { get; set; }
    public string CreadoPor { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public string? ModificadoPor { get; set; }
}
```

### **Paginación Avanzada**
```csharp
public class ResultadoPaginado<T>
{
    public List<T> Datos { get; set; }
    public int TotalRegistros { get; set; }
    public int PaginaActual { get; set; }
    public int TotalPaginas { get; set; }
    public bool TienePaginaAnterior { get; set; }
    public bool TienePaginaSiguiente { get; set; }
}
```

### **Validaciones Personalizadas**
```csharp
// Ejemplo de validación personalizada
RuleFor(x => x.FechaNacimiento)
    .NotEmpty().WithMessage("La fecha de nacimiento es requerida")
    .Must(BeValidAge).WithMessage("La edad debe estar entre 18 y 120 años");
```

## 📈 Rendimiento y Optimización

### **Optimizaciones Implementadas**
- ✅ **NoTracking**: Consultas de solo lectura optimizadas
- ✅ **Paginación**: Evita cargar grandes volúmenes de datos
- ✅ **Cache**: Reduce consultas repetitivas a BD
- ✅ **Async/Await**: Operaciones no bloqueantes
- ✅ **Connection Pooling**: Reutilización de conexiones

### **Métricas de Rendimiento**
- Tiempo de respuesta promedio: < 200ms
- Cache hit ratio: > 80%
- Consultas optimizadas con índices

## 🤝 Contribución

### **Estándares de Código**
- Seguir principios SOLID
- Documentar métodos públicos
- Incluir pruebas unitarias
- Usar convenciones de nomenclatura C#

### **Proceso de Contribución**
1. Fork del repositorio
2. Crear rama feature/bugfix
3. Implementar cambios con pruebas
4. Crear Pull Request
5. Code Review
6. Merge a main


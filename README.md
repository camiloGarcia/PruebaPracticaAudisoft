# Sistema de Gestión Académica - Prueba Técnica .NET

Aplicación web completa para la gestión de estudiantes, profesores y notas, desarrollada con **.NET 8** (Backend) y **Angular 20** (Frontend).

## 🚀 Características

- ✅ **Backend API REST** con ASP.NET Core 8
- ✅ **Frontend SPA** con Angular 20
- ✅ **Base de datos** SQL Server con Entity Framework Core
- ✅ **CRUD completo** para Estudiantes, Profesores y Notas
- ✅ **Filtrado y ordenamiento** en todas las entidades
- ✅ **Logging** con Serilog
- ✅ **Migraciones automáticas** al iniciar la aplicación
- ✅ **Documentación API** con Swagger
- ✅ **Interfaz responsive** con Bootstrap 5

---

## 📋 Requisitos Previos

### Backend
- **.NET 8 SDK** - [Descargar aquí](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server** (LocalDB o instancia completa)
- **Visual Studio 2022** o **Visual Studio Code** (opcional)

### Frontend
- **Node.js 18+** - [Descargar aquí](https://nodejs.org/)
- **npm 8+** (incluido con Node.js)
- **Angular CLI 20** - `npm install -g @angular/cli`

---

## 🗄️ Estructura del Proyecto

```
PruebaPracticaAudisoft/
├── Backend/
│   ├── API/                      # Controllers, Program.cs, appsettings.json
│   ├── Application/              # DTOs, Services, Interfaces
│   ├── Domain/                   # Entidades (Estudiante, Profesor, Nota)
│   └── Infrastructure/           # DbContext, Migrations
├── Frontend/
│   └── src/
│       ├── app/
│       │   ├── components/       # Componentes de Estudiantes, Profesores, Notas
│       │   ├── models/           # Interfaces TypeScript
│       │   └── services/         # Servicios HTTP
│       └── ...
└── README.md
```

---

## ⚙️ Instalación y Configuración

### 1. Clonar el Repositorio

```bash
git clone <url-del-repositorio>
cd PruebaPracticaAudisoft
```

### 2. Configurar el Backend

#### 2.1 Configurar la Cadena de Conexión

Edita el archivo `Backend/API/appsettings.json` y actualiza la cadena de conexión según tu servidor SQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=PruebaPracticaAudisoft;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
  }
}
```

**Ejemplos:**
- **SQL Server Local:** `Server=cagv\\MSSQLSERVER01;Database=...`
- **LocalDB:** `Server=(localdb)\\mssqllocaldb;Database=...`

#### 2.2 Restaurar Paquetes y Compilar

```bash
cd Backend
dotnet restore
dotnet build
```

#### 2.3 Ejecutar la API

```bash
cd Backend/API
dotnet run
```

La API estará disponible en: **http://localhost:5009**  
Swagger UI: **http://localhost:5009/swagger**

> **Nota:** Las migraciones se aplican automáticamente al iniciar la aplicación. La base de datos se creará si no existe.

---

### 3. Configurar el Frontend

#### 3.1 Instalar Dependencias

```bash
cd Frontend
npm install
```

#### 3.2 Ejecutar la Aplicación Angular

```bash
ng serve
```

La aplicación estará disponible en: **http://localhost:4200**

---

## 🎯 Uso de la Aplicación

### Navegación

1. **Inicio** - Página principal con acceso rápido a todas las secciones
2. **Estudiantes** - Gestión completa de estudiantes
3. **Profesores** - Gestión completa de profesores
4. **Notas** - Gestión completa de notas (vinculadas a estudiantes y profesores)

### Funcionalidades Disponibles

#### En Cada Módulo:
- ✅ **Listar** todos los registros
- ✅ **Crear** nuevos registros
- ✅ **Editar** registros existentes
- ✅ **Eliminar** registros (con confirmación)
- ✅ **Buscar/Filtrar** por nombre u otros campos
- ✅ **Ordenar** por diferentes columnas

---

## 🛠️ Tecnologías Utilizadas

### Backend
- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core 8.0** - ORM para acceso a datos
- **SQL Server** - Base de datos relacional
- **Serilog** - Logging estructurado
- **Swashbuckle** - Generación de documentación Swagger
- **System.Linq.Dynamic.Core** - Consultas LINQ dinámicas

### Frontend
- **Angular 20** - Framework SPA
- **TypeScript** - Lenguaje tipado
- **Bootstrap 5** - Framework CSS
- **RxJS** - Programación reactiva
- **HttpClient** - Cliente HTTP

---

## 📡 API Endpoints

### Estudiantes
- `GET /api/Estudiantes` - Listar todos (con filtros opcionales)
- `GET /api/Estudiantes/{id}` - Obtener por ID
- `POST /api/Estudiantes` - Crear nuevo
- `PUT /api/Estudiantes/{id}` - Actualizar
- `DELETE /api/Estudiantes/{id}` - Eliminar

### Profesores
- `GET /api/Profesores` - Listar todos (con filtros opcionales)
- `GET /api/Profesores/{id}` - Obtener por ID
- `POST /api/Profesores` - Crear nuevo
- `PUT /api/Profesores/{id}` - Actualizar
- `DELETE /api/Profesores/{id}` - Eliminar

### Notas
- `GET /api/Notas` - Listar todas (con filtros opcionales)
- `GET /api/Notas/{id}` - Obtener por ID
- `POST /api/Notas` - Crear nueva
- `PUT /api/Notas/{id}` - Actualizar
- `DELETE /api/Notas/{id}` - Eliminar

#### Parámetros de Query para Filtrado y Ordenamiento:
- `orderBy` - Campo por el cual ordenar (ej: "nombre", "id")
- `filterBy` - Campo por el cual filtrar (ej: "nombre", "id")
- `filterValue` - Valor a buscar

**Ejemplo:**
```
GET /api/Estudiantes?orderBy=nombre&filterBy=nombre&filterValue=Juan
```

---

## 📁 Base de Datos

### Esquema

#### Tabla: Estudiantes
| Campo  | Tipo         | Descripción            |
|--------|--------------|------------------------|
| Id     | int (PK)     | Identificador único    |
| Nombre | nvarchar(200)| Nombre del estudiante  |

#### Tabla: Profesores
| Campo  | Tipo         | Descripción            |
|--------|--------------|------------------------|
| Id     | int (PK)     | Identificador único    |
| Nombre | nvarchar(200)| Nombre del profesor    |

#### Tabla: Notas
| Campo         | Tipo         | Descripción                    |
|---------------|--------------|--------------------------------|
| Id            | int (PK)     | Identificador único            |
| Nombre        | nvarchar(200)| Nombre/descripción de la nota  |
| IdProfesor    | int (FK)     | Referencia a Profesor          |
| IdEstudiante  | int (FK)     | Referencia a Estudiante        |
| Valor         | decimal(5,2) | Valor de la nota (0-100)       |

---

## 🔧 Configuración Adicional

### Logging

Los logs se guardan automáticamente en la carpeta `Backend/API/Logs/` con rotación diaria.

**Archivo:** `log-YYYYMMDD.txt`

### CORS

El backend está configurado para aceptar peticiones desde `http://localhost:4200`. Si necesitas cambiar el puerto del frontend, actualiza la configuración CORS en `Backend/API/Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // Cambiar aquí
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

---

## 🐛 Solución de Problemas

### Error de Conexión a la Base de Datos
- Verifica que SQL Server esté corriendo
- Revisa la cadena de conexión en `appsettings.json`
- Asegúrate de tener permisos en la base de datos

### Error CORS en el Frontend
- Verifica que el backend esté corriendo en el puerto correcto (5009)
- Revisa la configuración CORS en `Program.cs`

### Errores de Compilación en Angular
```bash
cd Frontend
rm -rf node_modules package-lock.json
npm install
```

---

## 👨‍💻 Desarrollo

### Comandos Útiles

**Backend:**
```bash
# Compilar
dotnet build

# Ejecutar
dotnet run --project Backend/API

# Crear migración
dotnet ef migrations add NombreMigracion --project Backend/Infrastructure --startup-project Backend/API

# Ejecutar tests (si existen)
dotnet test
```

**Frontend:**
```bash
# Desarrollo
ng serve

# Build para producción
ng build --configuration production

# Ejecutar tests
ng test

# Generar componente
ng generate component components/nombre-componente
```

---

## 📝 Notas del Desarrollador

- ✅ Las migraciones se aplican **automáticamente** al iniciar la API
- ✅ El sistema de logging captura todos los errores en archivos
- ✅ La arquitectura utiliza **Clean Architecture** con separación de capas
- ✅ Todos los endpoints incluyen manejo de errores y validaciones
- ✅ El frontend utiliza componentes **standalone** de Angular 20
- ✅ Bootstrap proporciona una interfaz responsive sin configuración adicional

---

## 📄 Licencia

Este proyecto fue desarrollado como prueba técnica.

---

## ✉️ Contacto

Para consultas sobre este proyecto, contactar al desarrollador.


# Documentacion tecnica:

## Estructura del proyecto basado en capas


```
Coney.Backend/
│
├── .docker/
│       ├── cs.dev.dockerfile
│       ├── entrypoint.sh
│
├── Controllers/
│   └── Users/
│       └── UserController.cs
│
├── Data/
│   ├── ConeyDbContext.cs
│   └── Repositories/
│       └── Users/
│           └── UserRepository.cs
│
├── Models/
│   └── Entities/
│       └── User.cs
│
├── DTOs/
│   └── Users/
│       ├── CreateUserDto.cs
│       ├── UpdateUserDto.cs
│       └── UserDto.cs
│
├── Services/
│   └── Users/
│       └── UserService.cs
│
├── Interfaces/
│   └── Users/
│       └── (Future interfaces for typing objects, arrays, etc.)
│
├── Migrations/
│   └── Files related to Entity Framework migrations
│
├── Coney.Backend.csproj
└── Program.cs

``` 


### Descripción de las Carpetas

- **.docker/**: 
  - **cs.dev.dockerfile**: Este archivo es un Dockerfile personalizado que define cómo se construye la imagen Docker para el entorno de desarrollo.
  - **entrypoint.sh**: Este es un script de shell que actúa como punto de entrada para el contenedor,Antes de ejecutar la aplicación, este script intenta aplicar las migraciones de la base de datos utilizando dotnet ef database update. Si la base de datos no está lista, el script espera y reintenta hasta que la base de datos esté disponible.

- **Controllers/**: 
  - **Users/**: Contiene el controlador UserController.cs, que maneja las solicitudes HTTP relacionadas con los usuarios, incluyendo las operaciones CRUD (Crear, Leer, Actualizar, Eliminar).

- **Data/**:
  - **ConeyDbContext.cs**: Este archivo contiene la configuración de Entity Framework Core y define los DbSet para las entidades.
  - **Repositories/**: 
    - **Users/**: Contiene UserRepository.cs, que implementa el acceso a los datos específicamente para la entidad User. Aquí es donde reside la lógica de acceso a la base de datos.

- **Models/**:
  - **Entities/**: Contiene las clases que representan las entidades del modelo de datos. `User.cs` define la estructura de la tabla de usuarios.

- **DTOs/**:
  - **Users/**: Contiene los Data Transfer Objects (DTOs) que se utilizan para transferir datos entre las capas de la aplicación de manera segura y controlada. Incluye CreateUserDto.cs, UpdateUserDto.cs, y UserDto.cs.

- **Services/**:
  - **Users/**: Contiene UserService.cs, que implementa la lógica de negocios para la entidad User y actúa como intermediario entre el controlador y el repositorio.

- **Interfaces/**:
  - **Users/**: Esta carpeta está preparada para contener futuras interfaces que servirán para tipar objetos, arrays, etc., en el módulo de usuarios.

- **Migrations/**:
  - Carpeta generada por Entity Framework Core para manejar las migraciones de la base de datos. Aquí se almacenan los archivos que describen los cambios en el esquema de la base de datos a lo largo del tiempo.

- **ConeyBackend.csproj**:
  - Archivo de proyecto que contiene la configuración del proyecto C#.

- **Program.cs**:
  - Punto de entrada principal de la aplicación, donde se configura el pipeline de la aplicación y los servicios (como la configuración de Entity Framework y el contenedor de dependencia).

## Instalación para desarrollo

### 1.Restauración - instalación de Dependencias NuGet

Para este proceso de instalación de dependencias existen dos metodos comunes: 

  - **Automáticamente**: Si estás utilizando Visual Studio, las dependencias de NuGet se restaurarán automáticamente al abrir la solución o el proyecto.

  - **Manual**: Si estás utilizando una terminal, puedes restaurar manualmente las dependencias ejecutando el siguiente comando en la raíz del proyecto:

``` dotnet restore Coney.Backend.csproj ```

### 2. Disponibilizar la BD: Ejecutar la imagen del contenedor de Docker para PostgreSQL:
  - Si solo se quiere crear la BD de postgreSQL para trabajar el entorno de desarrollo en la maquina local, sin dockerizar el entorno .net pararse en la raíz del proyecto y ejecutar:
   ```docker compose -f docker-compose-devBD.yml up -d```
   (hasta este punto solo se creo el contenedor con la imagen de la BD, pero se deben crear las migraciones, leer apartado "Migraciones y Actualización de Base de Datos")

  - Pero si se quiere dockerizar todo el ambiente, tanto backend como BD(automaticamente se generan las migraciones) ejecutar: 
   ```docker compose -f docker-compose-dev.yml up -d```

### 3. Migraciones y Actualización de Base de Datos
#### Usando Terminales Estándar (cmd, PowerShell, Visual Studio Code):

1. Verificar tener instalado dotnet ef ejecutar en consola: ``` dotnet ef ``` Si no lo tienes instalado, ejecuta: ```dotnet tool install --global dotnet-ef ```

2. Después de estar seguros de que se tiene instalado el dotnet, Crear una nueva migración ejecutando: ```dotnet ef migrations add NombreMigration``` Esto creará una migración con el nombre: "NombreMigration" (reemplázalo por el nombre que desees). Incluirá todas las tablas y columnas definidas en tus entidades (User, etc.).

3. Aplicar la migración a la base de datos ejecutando: ```dotnet ef database update```  Esto ejecutará la migración y creará la base de datos y las tablas correspondientes en PostgreSQL.


#### Usando Visual Studio:

1. Abre la Consola del Administrador de Paquetes en Visual Studio (tools-> NuGet Package Manager -> Package Manager Console).
2. Ya estando en la consola, crear una nueva migración: ```add-migration InitialDb``` 
3. Aplicar la migración a la base de datos: ```update-database```


### 4. Proceso de compilación

#### Para compilar usando Terminales Estándar (cmd, PowerShell, Visual Studio Code):

1. Ejecutar en la terminal:
``` 
dotnet build Coney.Backend.generated.sln
dotnet run --project Coney.Backend.csproj

```
#### Para compilar usando Visual Studio:

1. Oprimir la tecla F5.

# Pruebas en POSTMAN
Para probar las apis en postman, se debe descargar el archivo llamado collection.json e importarlo en el postman, de esta manera ya se tendra configurado el entorno de prueba


# Despliegue para desarrollo

Para ambientes de desarrollo ya se encuentra dockerizado el proyecto, se puede realizar un despliegue ejecutando:
   ```docker compose -f docker-compose-dev.yml up -d```
# Despliegue para producción

*Aún en construcción*
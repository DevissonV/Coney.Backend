
# Documentacion tecnica:

## Estructura del proyecto basado en capas


   ```
Coney.Backend/
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
├── ConeyBackend.csproj
└── Program.cs

``` 


### Descripción de las Carpetas

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

### Ejecutar la imagen del contenedor de Docker para PostgreSQL:

1. Pararse en la raíz del proyecto y ejecutar:
   ```docker compose -f docker-compose-dev.yml up -d```
   

### Migraciones y Actualización de Base de Datos
#### Usando Terminales Estándar (cmd, PowerShell, Visual Studio Code):

1. Verificar tener instalado dotnet ef ejecutar en consola: ``` dotnet ef ``` Si no lo tienes instalado, ejecuta: ```dotnet tool install --global dotnet-ef ```

2. Crear una nueva migración: ```dotnet ef migrations add NombreMigration``` Esto creará una migración con el nombre NombreMigration (reemplázalo por el nombre que desees). Incluirá todas las tablas y columnas definidas en tus entidades (User, etc.).

3. Aplicar la migración a la base de datos: ```dotnet ef database update```  Esto ejecutará la migración y creará la base de datos y las tablas correspondientes en PostgreSQL.


#### Usando Visual Studio:

1. Abre la Consola del Administrador de Paquetes en Visual Studio (tools-> NuGet Package Manager -> Package Manager Console).
2. Crear una nueva migración: ```add-migration InitialDb``` 
3. Aplicar la migración a la base de datos: ```update-database```


# Para compilar usando Terminales Estándar (cmd, PowerShell, Visual Studio Code):

1. Ejecutar en la terminal: ``` dotnet run ```


# Pruebas en POSTMAN
Para probar las apis en postman, se debe descargar el archivo llamado collection.json e importarlo en el postman, de esta manera ya se tendra configurado el entorno de prueba
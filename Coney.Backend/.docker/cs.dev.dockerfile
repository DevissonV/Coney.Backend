# Use the official .NET SDK image with all the necessary development tools
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev

# Set the working directory inside the container
WORKDIR /app

# Install Entity Framework tools globally
RUN dotnet tool install --global dotnet-ef

# Ensure that the dotnet tools path is in the PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy the rest of the application code
COPY . ./

# Permissions to the entry point file for migrations and running the application
RUN chmod +x /app/.docker/entrypoint.sh

# restore dependencies
RUN dotnet restore Coney.Backend.csproj
RUN dotnet build Coney.Backend.sln

# Expose the port on which the application will run
EXPOSE 5293

ENTRYPOINT ["/app/.docker/entrypoint.sh"]
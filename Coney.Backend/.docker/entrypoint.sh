#!/bin/bash

# Wait for the database to be available
until dotnet ef database update; do
  >&2 echo "Postgres is unavailable - sleeping"
  sleep 2
done

>&2 echo "Postgres is up - executing command"

# Start the application
exec dotnet run --project Coney.Backend.csproj

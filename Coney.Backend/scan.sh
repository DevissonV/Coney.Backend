#!/usr/bin/env bash
set -euo pipefail

# Carga .env si existe (SONAR_TOKEN/SONAR_HOST_URL)
if [ -f .env ]; then
  set -a; source .env; set +a
fi

: "${SONAR_TOKEN:?Falta SONAR_TOKEN}"
SONAR_HOST_URL="${SONAR_HOST_URL:-http://localhost:9000}"
DOTNET_IMAGE="mcr.microsoft.com/dotnet/sdk:8.0"

# Normaliza la URL para que funcione *dentro* del contenedor
TARGET_URL="$SONAR_HOST_URL"
case "$TARGET_URL" in
  http://localhost:*)  TARGET_URL="${TARGET_URL/localhost/host.docker.internal}";;
  https://localhost:*) TARGET_URL="${TARGET_URL/localhost/host.docker.internal}";;
esac
echo "Usando SonarQube desde el contenedor en: $TARGET_URL"

# --- NUEVO: asegurar la imagen, evitando el helper de credenciales ---
echo "Verificando imagen $DOTNET_IMAGE ..."
if ! docker image inspect "$DOTNET_IMAGE" >/dev/null 2>&1; then
  echo "Descargando $DOTNET_IMAGE (pull sin credenciales del sistema)..."
  # DOCKER_CONFIG temporal para ignorar credsStore y evitar libsecret
  tmpcfg="$(mktemp -d)"
  DOCKER_CONFIG="$tmpcfg" docker pull "$DOTNET_IMAGE"
  rm -rf "$tmpcfg"
fi

docker run --rm -t \
  -e SONAR_HOST_URL="$TARGET_URL" \
  -e SONAR_TOKEN="$SONAR_TOKEN" \
  -e DOTNET_CLI_TELEMETRY_OPTOUT=1 \
  -v "$PWD:/work" -w /work \
  --add-host=host.docker.internal:host-gateway \
  "$DOTNET_IMAGE" bash -lc '
    set -euo pipefail
    export PATH="$PATH:$HOME/.dotnet/tools"
    dotnet tool install --global dotnet-sonarscanner --version 11.0.0 || true

    dotnet sonarscanner begin \
      /k:"coney-backend-cs" \
      /n:"Coney.Backend" \
      /v:"1.0.0" \
      /d:sonar.host.url="$SONAR_HOST_URL" \
      /d:sonar.token="$SONAR_TOKEN" \
      /d:sonar.exclusions="**/bin/**,**/obj/**,**/Migrations/**,**/*.Designer.cs,**/*.g.cs,**/*.g.i.cs,**/wwwroot/**,.github/**,.docker/**,**/*.md,**/docker-compose*.yml,**/collection.json" \
      /d:sonar.coverage.exclusions="**/Migrations/**,Program.cs,**/DTOs/**,**/Properties/**,**/*.g.cs,**/*.Designer.cs"

    dotnet build Coney.Backend.csproj -c Release

    # (Opcional) aquí van tests + cobertura

    dotnet sonarscanner end /d:sonar.token="$SONAR_TOKEN"
  '

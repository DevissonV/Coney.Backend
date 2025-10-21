#!/bin/bash
set -euo pipefail

# ------------------------------------------------------------------------------
# Levanta un servidor SonarQube Community en Docker.
# - Si el contenedor existe, solo lo inicia.
# - Si no existe, lo crea automáticamente con volúmenes persistentes.
# Compatible con macOS, Linux y Windows (WSL / Git Bash).
# ------------------------------------------------------------------------------

CONTAINER_NAME="sonarqube"
IMAGE_NAME="sonarqube:community"
PORT="${SONARQUBE_PORT:-9000}"

# Asegura que la imagen esté disponible
docker pull "${IMAGE_NAME}"

# Verifica si el contenedor ya existe
if docker ps -a --format '{{.Names}}' | grep -q "^${CONTAINER_NAME}$"; then
  echo "El contenedor '${CONTAINER_NAME}' ya existe. Iniciando..."
  docker start "${CONTAINER_NAME}"
else
  echo "Creando y levantando SonarQube (${IMAGE_NAME}) en puerto ${PORT}..."
  docker run -d \
    --name "${CONTAINER_NAME}" \
    -p "${PORT}:9000" \
    -e SONAR_ES_BOOTSTRAP_CHECKS_DISABLE=true \
    -v sonarqube_data:/opt/sonarqube/data \
    -v sonarqube_extensions:/opt/sonarqube/extensions \
    -v sonarqube_logs:/opt/sonarqube/logs \
    --restart unless-stopped \
    "${IMAGE_NAME}"
fi

echo "SonarQube está corriendo en: http://localhost:${PORT}"
echo "   Usuario: admin | Contraseña: admin (cámbiala en el primer inicio)"
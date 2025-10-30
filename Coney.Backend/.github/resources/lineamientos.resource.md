# 📐 Lineamientos del Proyecto para C#

## ✅ Convenciones de Código
- Usa **PascalCase** para nombres de clases, métodos y propiedades.
- Usa **camelCase** para variables locales y parámetros.
- Evita nombres abreviados; los identificadores deben ser descriptivos.
- Limita la longitud de las líneas a **máximo 120 caracteres**.

## ✅ Organización del Código
- Cada clase debe estar en su propio archivo.
- Agrupa clases relacionadas en **namespaces coherentes**.
- Evita métodos con más de **50 líneas**; divide en métodos más pequeños.

## ✅ Manejo de Excepciones
- Nunca uses `catch (Exception)` sin manejar el error.
- Implementa logs en cada bloque `catch`.
- Usa excepciones específicas en lugar de genéricas.

## ✅ Buenas Prácticas
- Implementa **async/await** para operaciones I/O.
- Evita el uso de `var` cuando el tipo no sea obvio.
- Usa **interfaces** para dependencias externas (principio de inversión).

## ✅ Seguridad
- Nunca expongas credenciales en el código.
- Usa `SecureString` para datos sensibles.
- Valida todas las entradas externas para evitar inyecciones.

## ✅ Estándares de Comentarios
- Documenta métodos públicos con **XML Comments** (`///`).
- Incluye descripción, parámetros y valor de retorno.
- No uses comentarios redundantes que repitan el código.

## ✅ Testing
- Cada método público debe tener al menos **una prueba unitaria**.
- Usa **xUnit** o **NUnit** como framework de pruebas.
- Evita dependencias externas en pruebas (usa mocks).

## ✅ Performance
- Evita consultas LINQ dentro de bucles.
- Usa `ConfigureAwait(false)` en librerías.
- Prefiere `StringBuilder` para concatenación en bucles.

---
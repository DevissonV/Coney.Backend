using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Coney.Backend.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            _logger.LogError(exception, "Error no manejado.");

            if (exception is InvalidOperationException)
            {
                context.Result = new BadRequestObjectResult(new { message = exception.Message });
            }
            else if (exception is KeyNotFoundException)
            {
                context.Result = new NotFoundObjectResult(new { message = exception.Message });
            }
            else if (exception is ApplicationException)
            {
                // Maneja las excepciones personalizadas del servicio
                context.Result = new BadRequestObjectResult(new { message = exception.Message });
            }
            else
            {
                context.Result = new ObjectResult(new { message = "Se produjo un error inesperado." })
                {
                    StatusCode = 500
                };
            }

            context.ExceptionHandled = true;  // Evita que la excepción se propague más allá de este punto
        }
    }
}

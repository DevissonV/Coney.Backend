using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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

            var response = new
            {
                status = false,
                code = 500,
                message = "Se produjo un error inesperado.",
                data = (object)null
            };

            if (exception is InvalidOperationException)
            {
                response = new
                {
                    status = false,
                    code = 400,
                    message = exception.Message,
                    data = (object)null
                };
                context.Result = new BadRequestObjectResult(response);
            }
            else if (exception is KeyNotFoundException)
            {
                response = new
                {
                    status = false,
                    code = 404,
                    message = exception.Message,
                    data = (object)null
                };
                context.Result = new NotFoundObjectResult(response);
            }
            else if (exception is ApplicationException)
            {
                response = new
                {
                    status = false,
                    code = 400,
                    message = exception.Message,
                    data = (object)null
                };
                context.Result = new BadRequestObjectResult(response);
            }
            else
            {
                context.Result = new ObjectResult(response)
                {
                    StatusCode = 500
                };
            }

            context.ExceptionHandled = true;
        }
    }
}

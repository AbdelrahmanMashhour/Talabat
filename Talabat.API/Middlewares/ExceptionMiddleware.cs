using System.Net;
using Talabat.API.Errors;

namespace Talabat.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);//Development
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment() ? new ApiExceptionResponse(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiExceptionResponse(context.Response.StatusCode);
                await context.Response.WriteAsJsonAsync(response);
            }

        }
    }
}

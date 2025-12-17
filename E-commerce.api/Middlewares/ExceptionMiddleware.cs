using System.ComponentModel.DataAnnotations;

namespace E_commerce.api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ValidationException:
                        _logger.LogWarning(ex,
                            "Validation error. Method: {Method}, Path: {Path}",
                            context.Request.Method,
                            context.Request.Path);
                        break;

                    default:
                        _logger.LogError(ex,
                            "Unhandled exception. Method: {Method}, Path: {Path}",
                            context.Request.Method,
                            context.Request.Path);
                        break;
                }

                var response = new
                {
                    statusCode = context.Response.StatusCode,
                    message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }

}

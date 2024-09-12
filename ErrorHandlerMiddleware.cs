using Newtonsoft.Json;

namespace YandexApiWeb
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            {
                var statusCode = StatusCodes.Status500InternalServerError;
                var message = "Введены неверные данные";

                var result = JsonConvert.SerializeObject(new { error = message });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                return context.Response.WriteAsync(result);
            }
        }
    }
}

using BookStore.Models;

namespace BookStore.Middlewares
{
    public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (KeyNotFoundException ex)
            {
                var response = new Models.Dto.CustomResponseDto<object>
                {
                    Success = false,
                    Message = "Entity not found",
                    Data = null,
                };
                var responseText = System.Text.Json.JsonSerializer.Serialize(response);
                context.Response.WriteAsync(responseText);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, $"Entity not found: {ex.Message}");
                await HandleExceptionAsync(
                    context,
                    ex,
                    "Entity not found.",
                    StatusCodes.Status404NotFound
                );
            }
            catch (InvalidOperationException ex)
            {
                logger.LogWarning(ex, $"An invalid operation occurred: {ex.Message}");
                await HandleExceptionAsync(context, ex, "An invalid operation occurred.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An unhandled exception has occurred: {ex.Message}");
                await HandleExceptionAsync(context, ex, "An unhandled exception has occurred.");
            }
        }

        private static Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            string message,
            int statusCode = StatusCodes.Status500InternalServerError
        )
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new Models.Dto.CustomResponseDto<object>
            {
                Success = false,
                Message = message,
                Data = null,
            };
            var responseText = System.Text.Json.JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(responseText);
        }
    }
}

namespace Test.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Test.DTO;
    using System.Net;
    using System.Text.Json;
    using Test.DTO;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excessao {ex.Message}");
                _logger.LogError(ex, "Unhandled exception occurred.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var _genericResultDTO = new GenericResultDTO<string>();
                _genericResultDTO.Content = $"StatusCode = {(int)HttpStatusCode.InternalServerError} Error = {ex.Message}";

                _genericResultDTO.AddErrorMessage($"Error Message = {ex.Message}");
                if (ex.Source != null)
                    _genericResultDTO.AddErrorMessage($"Source = {ex.Source}");
                if (ex.StackTrace != null)
                    _genericResultDTO.AddErrorMessage($"StackTrace = {ex.StackTrace}");

                var result = JsonSerializer.Serialize(_genericResultDTO);

                await context.Response.WriteAsync(result);
            }
        }
    }
}

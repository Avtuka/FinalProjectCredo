using Newtonsoft.Json;
using System.Text;

namespace BankingManagement.InsideSystem.API.Infrastucture.Middlewares
{
    public class MyExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MyExceptionHandlerMiddleware> _logger;

        public MyExceptionHandlerMiddleware(RequestDelegate next, ILogger<MyExceptionHandlerMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var error = new APIError(context, ex);
            var result = JsonConvert.SerializeObject(error);
            _logger.LogError(JsonConvert.SerializeObject(ex).Replace(",", ",\n"));

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.Status!.Value;
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(result));
        }
    }
}
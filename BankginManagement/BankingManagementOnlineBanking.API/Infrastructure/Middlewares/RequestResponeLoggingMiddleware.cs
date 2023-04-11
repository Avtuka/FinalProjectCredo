using System.Text;

namespace BankingManagementOnlineBanking.API.Infrastructure.Middlewares
{
    public class RequestResponeLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponeLoggingMiddleware> _logger;

        public RequestResponeLoggingMiddleware(RequestDelegate next, ILogger<RequestResponeLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var method = request.Method;
            var userId = context.User.FindFirst("UserId")?.Value ?? "Unauthorized User";
            var IP = context.Connection.RemoteIpAddress!.ToString() ?? "Unknow IP";
            var requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var requestBody = await ReadRequestBodyAsync(request);
            var querry = string.Join(",", request.Query.ToList());
            var path = request.Path.ToString();

            _logger.LogInformation($"Request information\nUserId: {userId}\nIP: {IP}\nMethod: {method}\n" +
                    $"Request Time: {requestTime}\nRequest body: {requestBody}\nQuerry: {querry}\nPath: {path}");

            await _next(context);

            var responseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var status = context.Response.StatusCode;

            _logger.LogInformation($"Response information\nResponse Time: {responseTime}\nStatus: {status}");
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();

            var buffer = new byte[request.ContentLength ?? 0];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;

            return bodyAsText;
        }
    }
}
using BankingManagementOnlineBanking.API.Infrastructure.Middlewares;

namespace BankingManagementOnlineBanking.API.Infrastructure.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<MyExceptionHandlerMiddleware>();
            app.UseMiddleware<CultureMiddleware>();
            app.UseMiddleware<RequestResponeLoggingMiddleware>();

            return app;
        }
    }
}

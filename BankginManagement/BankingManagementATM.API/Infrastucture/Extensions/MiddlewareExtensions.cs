using BankingManagementATM.API.Infrastucture.Middlewares;

namespace BankingManagementATM.API.Infrastucture.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<MyExceptionHandlerMiddleware>();
            app.UseMiddleware<CultureMiddleware>();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            return app;
        }
    }
}
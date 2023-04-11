using BankingManagement.InsideSystem.API.Infrastucture.Middlewares;

namespace BankingManagement.InsideSystem.API.Infrastucture.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<MyExceptionHandlerMiddleware>();
            app.UseMiddleware<Culturemiddleware>();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            return app;
        }
    }
}
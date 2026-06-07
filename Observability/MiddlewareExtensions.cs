namespace Observability
{
    public static class MiddlewareExtensions
    {
        // public static WebApplicationBuilder UseRequestContextLogging(this WebApplicationBuilder app)
        // {
        //     _ = app.UseMiddleware<RequestContextLoggingMiddleware>();

        //     return app;
        // }

        public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
        {
            _ = app.UseMiddleware<RequestContextLoggingMiddleware>();

            return app;
        }
    }
}

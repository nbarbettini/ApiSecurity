using Microsoft.AspNetCore.Builder;

namespace Recaffeinate.SecureApi
{
    public static class AbortIfNotHttpsApplicationBuilderExtensions
    {
        public static IApplicationBuilder AbortIfNotHttps(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (!context.Request.IsHttps)
                {
                    context.Abort();
                }

                await next();
            });

            return app;
        }
    }
}

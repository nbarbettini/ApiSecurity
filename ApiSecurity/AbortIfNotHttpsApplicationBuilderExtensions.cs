using Microsoft.AspNetCore.Builder;

namespace Recaffeinate.ApiSecurity
{
    public static class AbortIfNotHttpsApplicationBuilderExtensions
    {
        /// <summary>
        /// Closes the request immediately if it is not secure (HTTPS).
        /// Be aware that sensitive information sent by the client WILL be visible!
        /// Whenever possible, reject connections at the server or reverse proxy layer instead.
        /// </summary>
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

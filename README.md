# API security extensions

:package: On NuGet: [Recaffeinate.ApiSecurity](https://www.nuget.org/packages/Recaffeinate.ApiSecurity)

## Background

It's easy to enforce HTTPS (with automatic redirects) in browser apps using the `[RequireHttps]` attribute. However, the [ASP.NET Core docs](https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl) have this to say about using the attribute in API projects:

> Do **not** use `RequireHttpsAttribute` on Web APIs that receive sensitive information. `RequireHttpsAttribute` uses HTTP status codes to redirect browsers from HTTP to HTTPS. API clients may not understand or obey redirects from HTTP to HTTPS. Such clients may send information over HTTP.

Unfortunately there isn't a version of the attribute that closes or rejects the connection without redirecting. You can always enforce HTTPS at the API gateway or reverse proxy layer, but sometimes you want more control.

:earth_americas: Read more in my blog post: [Enforce HTTPS correctly in ASP.NET Core APIs](https://www.recaffeinate.co/post/enforce-https-aspnetcore-api/)

## Usage

Use `[RequireHttpsOrClose]` on controllers or actions to return `400 Bad Request` for insecure requests:

```csharp
[RequireHttpsOrClose]
public class HomeController
```

Or, use the `AbortIfNotHttps()` middleware if to reject all insecure requests across your entire application (more secure). Please it at the top of your `Configure` method:

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.AbortIfNotHttps();

    if (env.IsDevelopment())
    // The rest of your pipeline...
}
```

## Feedback

Questions, comments, and PRs are welcome! Feel free to post an [issue](https://github.com/nbarbettini/ApiSecurity/issues) or ask me questions on [Twitter](https://twitter.com/nbarbettini).

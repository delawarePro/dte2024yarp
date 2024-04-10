/*
Show how you can use YARP to apply policies.
Authentication will fail, because it is not configured in the demo.

Steps
1. Start from Demo1
2. Add authorization policy in config.
3. Add authorized route in appsettings.json
*/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .ConfigureHttpClient((context, handler) =>
    {
        // Let YARP follow redirects.
        handler.AllowAutoRedirect = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("authenticated", policy =>
        policy.RequireAuthenticatedUser());
});

var app = builder.Build();

app.MapReverseProxy();

app.Run();

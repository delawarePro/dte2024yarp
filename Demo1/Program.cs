/*
Show how easy it is to use YARP to create a reverse proxy in ASP.NET Core.

Steps
1. Install Yarp.ReverseProxy 2.1.0 (nuget)
2. Code changes in Program.cs
3. Configuration in appsettings.json
*/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .ConfigureHttpClient((context, handler) =>
    {
        // Let YARP follow redirects.
        handler.AllowAutoRedirect = true;
    });

var app = builder.Build();

app.MapReverseProxy();

app.Run();

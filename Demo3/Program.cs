/*
Show how easy it is to add YARP to an ASP.NET Core web app.

Steps
1. Install Yarp.ReverseProxy 2.1.0 (nuget)
2. Code changes in Program.cs
3. Configuration in appsettings.json
4. Run the app and goto '/api/resources'
*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add YARP services.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapReverseProxy();

app.Run();

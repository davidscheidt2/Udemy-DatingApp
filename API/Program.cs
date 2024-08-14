using API.Extensions;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddCors();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(
    "http://localhost:5000", "https://localhost:5001", "http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(
    "http://localhost:5000", "https://localhost:5001"));

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

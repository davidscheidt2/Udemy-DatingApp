using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

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

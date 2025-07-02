using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Configuration des fichiers appsettings
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables(); // 👉 variables d'environnement

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// ------------------------------------------------------------
// Enregistrement des services utilisés par l'application
// ------------------------------------------------------------

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// 🔐 Configuration de la politique CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins(
                "https://localhost:5172", // dev local
                "https://portfolio-sachamachadoalbino.netlify.app",
                "https://portfolio-machado.up.railway.app"
            )
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// ------------------------------------------------------------
// Création de l'application ASP.NET Core
// ------------------------------------------------------------
var app = builder.Build();

// ------------------------------------------------------------
// Configuration du pipeline HTTP
// ------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseHsts();
}

// ✅ Middleware
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowFrontend"); // 👉 ici après UseRouting
app.UseAuthorization(); // si jamais tu en ajoutes

// ------------------------------------------------------------
// Routage
// ------------------------------------------------------------
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Urls.Add("http://+:8080");

app.Run();

using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Voir les erreurs dans la console
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// ------------------------------------------------------------
// Enregistrement des services utilisés par l'application
// ------------------------------------------------------------

// Ajoute le support des contrôleurs d'API + vues Razor (MVC)
builder.Services.AddControllersWithViews();

// Ajoute le support des Razor Pages (pages .cshtml)
builder.Services.AddRazorPages();

// 🔐 Configuration de la politique CORS
// Cela permet au front-end (ex: Blazor WebAssembly) d'accéder à l'API
// depuis un domaine différent (localhost ou Netlify)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
                "https://localhost:5172",         // Front local Blazor WebAssembly
                "https://tonsite.netlify.app"     // Domaine de production (Netlify)
            )
            .AllowAnyMethod()    // Autorise toutes les méthodes HTTP : GET, POST, etc.
            .AllowAnyHeader();   // Autorise tous les headers (utile pour JSON, Auth...)
    });
});

// ------------------------------------------------------------
// Création de l'application ASP.NET Core
// ------------------------------------------------------------
var app = builder.Build();

// ------------------------------------------------------------
// Configuration du middleware HTTP
// ------------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    // En mode développement : active les outils de débogage WebAssembly
    app.UseWebAssemblyDebugging();
}
else
{
    // En production : active la politique HSTS (sécurité HTTPS)
    app.UseHsts();
}

// Active le CORS pour permettre les appels cross-origin du client
app.UseCors();

// Redirige automatiquement HTTP → HTTPS
app.UseHttpsRedirection();

// Configure les fichiers nécessaires pour faire tourner une app Blazor WebAssembly
app.UseBlazorFrameworkFiles();

// Sert les fichiers statiques (CSS, JS, images, etc.)
app.UseStaticFiles();

// Active le système de routage (URLs vers les bons contrôleurs/pages)
app.UseRouting();

// ------------------------------------------------------------
// Mappage des routes vers les pages / API
// ------------------------------------------------------------

// Active les Razor Pages (si tu en as)
app.MapRazorPages();

// Active les contrôleurs API (comme ton ContactController)
app.MapControllers();

// Redirige toute URL inconnue vers index.html (pour Blazor SPA)
app.MapFallbackToFile("index.html");

// Démarre l'application ASP.NET Core
app.Run();

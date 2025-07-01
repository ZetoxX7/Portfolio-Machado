using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Portfolio.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://portfolio-machado-production.up.railway.app/api/") });

// 🔌 Lien vers ton composant racine Blazor
builder.RootComponents.Add<App>("#app");

// 💡 Permet d’ajouter des balises dans <head> dynamiquement
builder.RootComponents.Add<HeadOutlet>("head::after");

// ------------------------------------------------------------
// Configuration du HttpClient pour consommer l'API ASP.NET Core
// ------------------------------------------------------------

// Tu peux supprimer ce bloc si tu ne fais pas d’authentification spéciale :
/*
builder.Services.AddHttpClient("Portfolio.ServerAPI", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

// Fournit un HttpClient "Portfolio.ServerAPI" avec token si besoin
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("Portfolio.ServerAPI"));
*/

// ✅ HttpClient utilisé pour appeler ton API backend
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7224/") // Remplace par le port de ton Portfolio.Server
    });

// ------------------------------------------------------------
// Lancer l'application
// ------------------------------------------------------------
await builder.Build().RunAsync();

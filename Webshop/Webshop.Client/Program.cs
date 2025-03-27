using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add Http Client so that FE can call API on server side
var apiBaseUrl = builder.Configuration["ApiBaseUrl"];

if (string.IsNullOrEmpty(apiBaseUrl))
{
    throw new InvalidOperationException("API Base URL is not configured in the appsettings.json.");
}

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});


//builder.Services.AddHttpClient("API", client =>
//{
//    // Ange bas-URL för API-servern
//    client.BaseAddress = new Uri("https://localhost:7070/");
//});

await builder.Build().RunAsync();

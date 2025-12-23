using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RestaurantManager.Interface;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var restaurantManagerApiAddress = builder.Configuration["RestaurantManagerApiAddress"];
if (string.IsNullOrEmpty(restaurantManagerApiAddress))
    throw new Exception("Please configure the RestaurantManagerApiAddress setting in your configuration.");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(restaurantManagerApiAddress!) });

builder.Services.AddMudServices();

await builder.Build().RunAsync();

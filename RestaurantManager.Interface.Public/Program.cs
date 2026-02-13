using MudBlazor.Services;
using RestaurantManager.Interface.Public.Views;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var restaurantManagerApiAddress = builder.Configuration["RestaurantManagerApiAddress"];
if (string.IsNullOrEmpty(restaurantManagerApiAddress))
    throw new Exception("Please configure the RestaurantManagerApiAddress setting in your configuration.");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(restaurantManagerApiAddress!) });

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

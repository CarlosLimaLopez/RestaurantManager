using MudBlazor.Services;
using RestaurantManager.Interface.Public.Views;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Configurar logging más detallado
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var restaurantManagerApiAddress = builder.Configuration["RestaurantManagerApiAddress"];
if (string.IsNullOrEmpty(restaurantManagerApiAddress))
    throw new Exception("Please configure the RestaurantManagerApiAddress setting in your configuration.");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(restaurantManagerApiAddress!) });

builder.Services.AddMudServices();

var app = builder.Build();

// Log para verificar archivos estáticos en producción
var logger = app.Services.GetRequiredService<ILogger<Program>>();
var webRoot = app.Environment.WebRootPath;
logger.LogInformation("WebRootPath: {WebRootPath}", webRoot);
if (Directory.Exists(webRoot))
{
    var appCssPath = Path.Combine(webRoot, "app.css");
    logger.LogInformation("app.css exists: {Exists}", File.Exists(appCssPath));
    if (File.Exists(appCssPath))
    {
        logger.LogInformation("app.css size: {Size} bytes", new FileInfo(appCssPath).Length);
    }
}

// UseStaticFiles DEBE ir antes del ExceptionHandler para evitar que los archivos estáticos pasen por el manejador de errores
// Middleware de diagnóstico para archivos estáticos
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value?.EndsWith(".css") == true || 
        context.Request.Path.Value?.EndsWith(".js") == true)
    {
        logger.LogInformation("Static file request: {Path}", context.Request.Path);
        try
        {
            await next();
            logger.LogInformation("Static file response: {StatusCode}", context.Response.StatusCode);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error serving static file: {Path}", context.Request.Path);
            throw;
        }
    }
    else
    {
        await next();
    }
});

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// HTTPS redirection is handled by nginx reverse proxy
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// UseRouting debe estar aquí para que Antiforgery solo se aplique a endpoints, no a archivos estáticos
app.UseRouting();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

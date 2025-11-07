using AppDocentes.Data;
using AppDocentes.Servicios.Contrato;
using AppDocentes.Servicios.Implementacion;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------
// Cultura global (Chile) - se aplica al proceso y hilos
// ----------------------------------------------------
var cultureInfo = new CultureInfo("es-CL")
{
    NumberFormat =
    {
        NumberDecimalSeparator = ",",
        NumberGroupSeparator = "."
    }
};
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// ----------------------------------------------------
// Servicios
// ----------------------------------------------------
var services = builder.Services;
var configuration = builder.Configuration;

// Forzar culture para ModelBinder/validaciones
services.Configure<RequestLocalizationOptions>(options =>
{
    var culture = new CultureInfo("es-CL")
    {
        NumberFormat =
        {
            NumberDecimalSeparator = ",",
            NumberGroupSeparator = "."
        }
    };
    options.DefaultRequestCulture = new RequestCulture(culture);
    options.SupportedCultures = new[] { culture };
    options.SupportedUICultures = new[] { culture };
});

// MVC + Razor Pages (priorizar compatibilidad con Razor Pages)
services.AddControllersWithViews(options =>
{
    // Evitar cache en todas las respuestas por defecto (si es lo que se desea)
    options.Filters.Add(new ResponseCacheAttribute
    {
        NoStore = true,
        Location = ResponseCacheLocation.None
    });
});
services.AddRazorPages();

// DbContext con pool y reintentos frente a fallos transitorios
var connectionString = configuration.GetConnectionString("CadenaSQL");
services.AddDbContextPool<DocentesDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    });

    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});


// Servicios de aplicación
services.AddScoped<IUsuarioService, UsuarioService>();

// Autenticación por cookies
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inicio/IniciarSesion";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
    });

// Autorización (ejemplo de política)
services.AddAuthorization(options =>
{
    options.AddPolicy("RequireLoggedInUser", policy => policy.RequireAuthenticatedUser());
});

// Session
services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Response caching (opcional)
services.AddResponseCaching();

var app = builder.Build();

// Asegurar cultura global en runtime también
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// ----------------------------------------------------
// Middleware pipeline
// ----------------------------------------------------

// Cabeceras para evitar cache (global)
app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";
    await next();
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseMigrationsEndPoint(); // si AddDatabaseDeveloperPageExceptionFilter está activo
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Aplicar RequestLocalization con las opciones configuradas
var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>()?.Value;
if (locOptions != null)
{
    app.UseRequestLocalization(locOptions);
}
else
{
    // Fallback mínimo
    app.UseRequestLocalization(new RequestLocalizationOptions().SetDefaultCulture("es-CL"));
}

app.UseRouting();

// Session antes de autenticación/autorización si se usa dentro de esos middlewares
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCaching();

// Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

app.MapRazorPages();

app.Run();

using AppDocentes.Data;
using AppDocentes.Servicios.Contrato;
using AppDocentes.Servicios.Implementacion;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Globalization;

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
// Se aplica autorización global: todas las rutas requieren autenticación salvo las marcadas con [AllowAnonymous]
services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter());
    // Evitar cache en todas las respuestas por defecto
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
Rotativa.AspNetCore.RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");



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
    app.UseRequestLocalization(new RequestLocalizationOptions().SetDefaultCulture("es-CL"));
}

app.UseRouting();

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

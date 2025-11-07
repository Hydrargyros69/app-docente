using AppDocentes.Models;
using AppDocentes.Recursos;
using AppDocentes.Servicios.Contrato;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppDocentes.Controllers
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;

        public InicioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        public IActionResult Index()
        {
            // Redirige explícitamente a la página de login para evitar buscar una vista inexistente.
            return RedirectToAction("IniciarSesion", "Inicio");
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrarse(Usuario modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            // Encriptar la contraseña antes de guardar
            modelo.PassUsuario = Utilidades.EncriptarClave(modelo.PassUsuario);

            Usuario usuarioCreado = await _usuarioServicio.SaveUsuario(modelo);

            if (usuarioCreado != null && usuarioCreado.IdUsuario > 0)
                return RedirectToAction("IniciarSesion", "Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View(modelo);
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IniciarSesion(string nombre, string clave)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(clave))
            {
                ViewData["Mensaje"] = "Nombre y clave son requeridos";
                return View();
            }

            Usuario usuarioEncontrado = await _usuarioServicio.GetUsuario(nombre, Utilidades.EncriptarClave(clave));
            if (usuarioEncontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuarioEncontrado.NomUsuario ?? string.Empty),
                new Claim("UsuarioID", usuarioEncontrado.IdUsuario.ToString())
                // Si el modelo Usuario tuviera rol, añadir aquí: new Claim(ClaimTypes.Role, ...)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties { AllowRefresh = true };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            HttpContext.Response.Headers["Pragma"] = "no-cache";
            HttpContext.Response.Headers["Expires"] = "0";

            return Redirect("/Inicio/IniciarSesion"); // redirección absoluta para evitar ambigüedades
        }
    }
}





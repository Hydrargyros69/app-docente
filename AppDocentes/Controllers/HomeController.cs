using AppDocentes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppDocentes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            try
            {
                // Log request and authentication details to help debug post-login issues
                _logger.LogInformation("Home/Index called. RequestPath={Path}, Method={Method}", Request.Path, Request.Method);
                _logger.LogInformation("Cookies count={Count}", Request.Cookies.Count);
                var hasAuthCookie = Request.Cookies.ContainsKey(".AspNetCore.Cookies");
                _logger.LogInformation("HasAuthCookie={HasAuthCookie}", hasAuthCookie);
                _logger.LogInformation("User.Identity.IsAuthenticated={IsAuthenticated}", User?.Identity?.IsAuthenticated ?? false);

                if (User?.Identity?.IsAuthenticated == true)
                {
                    foreach (var claim in User.Claims)
                    {
                        _logger.LogDebug("User claim: {Type} = {Value}", claim.Type, claim.Value);
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in Home/Index");
                // Redirect to error page which shows request id
                return RedirectToAction("Error");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

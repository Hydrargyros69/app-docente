using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppDocentes.Data;
using AppDocentes.Models;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using System.Data.SqlTypes;
using NuGet.Packaging;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AppDocentes.Controllers
{
    public class ModulosController : Controller
    {
        private readonly DocentesDbContext _context;

        public ModulosController(DocentesDbContext context)
        {
            _context = context;
        }

        // GET: Modulos
        public async Task<IActionResult> Index(string searchModulo, int? idCarrera)
        {
            //Consulta base con relación a Carrera incluida
            var modulos = _context.Modulos
                .Include(m => m.IdCarreraNavigation)
                .AsQueryable();

            //Si no hay carrera seleccionada, no mostrar ningún registro
            if (!idCarrera.HasValue || idCarrera == 0)
            {
                //Cargar el combo de carreras para la vista
                ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "NomCarrera");
                ViewData["CurrentFilter"] = searchModulo;

                //Retorna la vista con una lista vacía
                return View(new List<Modulo>());
            }

            //Si hay una carrera seleccionada, filtra los módulos de esa carrera
            modulos = modulos.Where(m => m.IdCarrera == idCarrera);

            //Si además se escribió texto de búsqueda, aplica el filtro adicional
            if (!string.IsNullOrEmpty(searchModulo))
            {
                modulos = modulos.Where(m => m.NomModulo.Contains(searchModulo));
            }

            //Cargar lista de carreras para el select (manteniendo la seleccionada)
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "NomCarrera", idCarrera);
            ViewData["CurrentFilter"] = searchModulo;

            //Devuelve la vista con los módulos filtrados
            return View(await modulos.ToListAsync());
        }

        // [NUEVA ACCIÓN] GET: Modulos/GeneratePdf
        // Genera PDF con la lista de módulos (acepta filtros opcionales por query string)
        [HttpGet]
        public IActionResult GeneratePdf(int? idCarrera, string? searchModulo)
        {
            var carreras = _context.Carreras
                .Include(c => c.Modulos)
                .OrderBy(c => c.NomCarrera)
                .AsQueryable();

            if (idCarrera.HasValue && idCarrera.Value != 0)
            {
                carreras = carreras.Where(c => c.IdCarrera == idCarrera.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchModulo))
            {
                // Filtra carreras que tengan al menos un módulo cuyo nombre contenga el texto
                carreras = carreras.Where(c => c.Modulos.Any(m => m.NomModulo.Contains(searchModulo)));
            }

            var lista = carreras.ToList();

            var pdf = new ViewAsPdf("~/Views/Modulos/Report.cshtml", lista)
            {
                FileName = "Modulos_Por_Carrera.pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                PageMargins = new Margins(20, 20, 20, 20)
            };

            return pdf;
        }

        // GET: Modulos/Create
        // Acción GET: Modulos/Create
        public IActionResult Create()
        {
            // Cargar lista de carreras para el Select en la vista
            // Se crea un SelectList con los registros de la tabla "Carreras"
            // - El primer parámetro (_context.Carreras): indica la fuente de datos
            // - El segundo ("IdCarrera"): será el valor del <option> en el select
            // - El tercero ("NomCarrera"): será el texto visible en el combo desplegable
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "NomCarrera");

            // Retorna la vista "Create", que mostrará el formulario para crear un nuevo módulo
            // y usará la lista cargada en ViewData["IdCarrera"] para el select de carreras
            return View();
        }

        // POST: Modulos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Acción POST: Modulos/Create
        // Se ejecuta cuando el usuario envía el formulario de creación del módulo
        public async Task<IActionResult> Create([Bind("IdModulo,NomModulo,Horas,IdCarrera")] Modulo modulo)
        {
            // Verifica si el modelo cumple con todas las validaciones definidas en la clase "Modulo"
            // (por ejemplo: campos requeridos, tipos de datos, longitudes, etc.)
            if (ModelState.IsValid)
            {
                // Agrega el nuevo objeto "modulo" al contexto de base de datos
                _context.Add(modulo);

                // Guarda los cambios de manera asincrónica en la base de datos
                await _context.SaveChangesAsync();

                // Si todo salió bien, redirige a la acción Index para mostrar la lista actualizada de módulos
                return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido (por ejemplo, falta un campo o hay un error),
            // se recarga la lista de carreras para que el combo <select> vuelva a funcionar en la vista
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "NomCarrera", modulo.IdCarrera);

            // Retorna la vista "Create" con el objeto "modulo" cargado, mostrando los errores de validación
            return View(modulo);
        }


        // GET: Modulos/Edit/5
        // Acción GET: Modulos/Edit/5
        // Muestra el formulario de edición de un módulo específico
        public async Task<IActionResult> Edit(int? id)
        {
            // Verifica si el parámetro "id" llegó nulo en la URL
            // Si no se envió un id válido, devuelve un resultado 404 (no encontrado)
            if (id == null)
            {
                return NotFound();
            }

            // Busca en la base de datos el módulo cuyo Id coincida con el parámetro recibido
            // FindAsync permite buscar por clave primaria de forma asincrónica
            var modulo = await _context.Modulos.FindAsync(id);

            // Si no se encuentra ningún módulo con ese Id, retorna 404 (no encontrado)
            if (modulo == null)
            {
                return NotFound();
            }

            // Cargar la lista de carreras para el <select> en la vista
            // - _context.Carreras: obtiene todas las carreras desde la base de datos
            // - "IdCarrera": valor que se enviará en el formulario
            // - "NomCarrera": texto visible en el combo
            // - modulo.IdCarrera: valor actualmente seleccionado del módulo
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "NomCarrera", modulo.IdCarrera);

            // Devuelve la vista Edit con el objeto "modulo" cargado,
            // permitiendo que el formulario se rellene con los datos actuales
            return View(modulo);
        }


        // POST: Modulos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Acción POST: Modulos/Edit/5
        // Se ejecuta cuando el usuario envía el formulario de edición de un módulo
        public async Task<IActionResult> Edit(int id, [Bind("IdModulo,NomModulo,Horas,IdCarrera")] Modulo modulo)
        {
            // Verifica que el id recibido por parámetro (de la URL) coincida
            // con el IdModulo que viene desde el formulario. Si no coinciden,
            // se retorna un error 404 (no encontrado) por seguridad.
            if (id != modulo.IdModulo)
            {
                return NotFound();
            }

            // Valida que el modelo cumpla con las reglas definidas
            // en la clase Modulo (por ejemplo: campos requeridos, tipos, etc.)
            if (ModelState.IsValid)
            {
                try
                {
                    // Marca el objeto "modulo" como modificado dentro del contexto
                    // para que EF Core actualice sus valores en la base de datos.
                    _context.Update(modulo);

                    // Guarda los cambios en la base de datos de forma asincrónica
                    await _context.SaveChangesAsync();
                }
                // Captura excepciones relacionadas con concurrencia (cuando dos usuarios
                // intentan editar el mismo registro al mismo tiempo).
                catch (DbUpdateConcurrencyException)
                {
                    // Verifica si el módulo todavía existe en la base de datos
                    if (!_context.Modulos.Any(e => e.IdModulo == modulo.IdModulo))
                    {
                        // Si ya no existe, devuelve un 404
                        return NotFound();
                    }
                    else
                    {
                        // Si el error es por otra causa, vuelve a lanzar la excepción
                        throw;
                    }
                }

                // Si la edición fue exitosa, redirige a la vista Index
                // para mostrar la lista actualizada de módulos
                return RedirectToAction(nameof(Index));
            }

            // Si hay errores de validación o de modelo:
            // se vuelve a cargar el combo <select> de carreras
            // para que la vista Edit no quede vacía al recargar
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "NomCarrera", modulo.IdCarrera);

            // Devuelve la vista "Edit" with el objeto módulo cargado,
            // mostrando los mensajes de error correspondientes
            return View(modulo);
        }


        // GET: Modulos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modulo = await _context.Modulos
                .Include(m => m.IdCarreraNavigation)
                .FirstOrDefaultAsync(m => m.IdModulo == id);
            if (modulo == null)
            {
                return NotFound();
            }

            return View(modulo);
        }

        // POST: Modulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modulo = await _context.Modulos.FindAsync(id);
            if (modulo != null)
            {
                _context.Modulos.Remove(modulo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GeneratePdfByCarrera()
        {
            var CarrerasConModulos = await _context.Carreras
                .Include(c => c.Modulos)
                .OrderBy(c => c.NomCarrera)
                .ToListAsync();
            var pdf = new ViewAsPdf("~/views/Modulos/Report.cshtml", CarrerasConModulos)
            { 
                FileName = "Informe_Modulos_Por_Carrera.pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                PageMargins = new Margins(20, 20, 20, 20)
            };
            return pdf;
        }

        private bool ModuloExists(int id)
        {
            return _context.Modulos.Any(e => e.IdModulo == id);
        }
    }
}

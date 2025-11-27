using AppDocentes.Data;
using AppDocentes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using System.Linq;
using System.Threading.Tasks;

namespace AppDocentes.Controllers
{
    // Controlador para informes/export (PDF)
    public class ReportsController : Controller
    {
        private readonly DocentesDbContext _context;

        public ReportsController(DocentesDbContext context)
        {
            _context = context;
        }

        // GET: Reports/Filter
        // Muestra el formulario para filtrar por docente y carrera y opcionalmente los resultados
        public async Task<IActionResult> Filter(int? docenteId, int? carreraId)
        {
            var docentes = _context.Docentes
                .AsNoTracking()
                .OrderBy(d => d.NomDocente)
                .Select(d => new { d.IdDocente, d.NomDocente })
                .ToList();

            var carreras = _context.Carreras
                .AsNoTracking()
                .OrderBy(c => c.NomCarrera)
                .Select(c => new { c.IdCarrera, c.NomCarrera })
                .ToList();

            ViewData["Docentes"] = new SelectList(docentes, "IdDocente", "NomDocente", docenteId);
            ViewData["Carreras"] = new SelectList(carreras, "IdCarrera", "NomCarrera", carreraId);

            // Ejecutar la consulta agrupada siempre (si no hay filtros, devuelve todos)
            var query = from mv in _context.MovDocentes.AsNoTracking()
                        join d in _context.Docentes.AsNoTracking() on mv.IdDocente equals d.IdDocente
                        join mod in _context.Modulos.AsNoTracking() on mv.IdModulo equals mod.IdModulo
                        join c in _context.Carreras.AsNoTracking() on mod.IdCarrera equals c.IdCarrera
                        where (docenteId == null || d.IdDocente == docenteId)
                           && (carreraId == null || c.IdCarrera == carreraId)
                        group mv by new
                        {
                            d.IdDocente,
                            d.NomDocente,
                            c.IdCarrera,
                            c.NomCarrera,
                            mod.IdModulo,
                            mod.NomModulo
                        } into g
                        orderby g.Key.NomDocente, g.Key.NomCarrera, g.Key.NomModulo
                        select new DocenteHorasDto
                        {
                            IdDocente = g.Key.IdDocente,
                            NomDocente = g.Key.NomDocente,
                            IdCarrera = g.Key.IdCarrera,
                            NomCarrera = g.Key.NomCarrera,
                            IdModulo = g.Key.IdModulo,
                            NomModulo = g.Key.NomModulo,
                            TotalHoras = g.Sum(x => x.Horas)
                        };

            var resultados = await query.ToListAsync();

            // Pasar los resultados como modelo a la vista (la vista usa @model)
            return View("~/Views/Docentes/FilterHorasDocentes.cshtml", resultados);
        }

        // GET: Reports/DocentesHorasPdf
        // Genera un PDF con la cantidad de horas por docente => carrera => mÃ³dulo
        // Acepta filtros opcionales: docenteId y carreraId (si son null todos)
        public async Task<IActionResult> DocentesHorasPdf(int? docenteId, int? carreraId)
        {
            var query = from mv in _context.MovDocentes.AsNoTracking()
                        join d in _context.Docentes.AsNoTracking() on mv.IdDocente equals d.IdDocente
                        join mod in _context.Modulos.AsNoTracking() on mv.IdModulo equals mod.IdModulo
                        join c in _context.Carreras.AsNoTracking() on mod.IdCarrera equals c.IdCarrera
                        where (docenteId == null || d.IdDocente == docenteId)
                           && (carreraId == null || c.IdCarrera == carreraId)
                        group mv by new
                        {
                            d.IdDocente,
                            d.NomDocente,
                            c.IdCarrera,
                            c.NomCarrera,
                            mod.IdModulo,
                            mod.NomModulo
                        } into g
                        orderby g.Key.NomDocente, g.Key.NomCarrera, g.Key.NomModulo
                        select new DocenteHorasDto
                        {
                            IdDocente = g.Key.IdDocente,
                            NomDocente = g.Key.NomDocente,
                            IdCarrera = g.Key.IdCarrera,
                            NomCarrera = g.Key.NomCarrera,
                            IdModulo = g.Key.IdModulo,
                            NomModulo = g.Key.NomModulo,
                            TotalHoras = g.Sum(x => x.Horas)
                        };

            var lista = await query.ToListAsync();

            var pdf = new ViewAsPdf("~/Views/Docentes/DocenteHorasPorCarreraModulo.cshtml", lista)
            {
                FileName = "Informe_Horas_Por_Docente_Carrera_Modulo.pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                PageMargins = new Margins { Top = 15, Right = 15, Bottom = 15, Left = 15 }
            };

            return pdf;
        }
    }
}
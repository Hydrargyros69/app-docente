using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppDocentes.Data;
using AppDocentes.Models;

namespace AppDocentes.Controllers
{
    public class DatosController : Controller
    {
        private readonly DocentesDbContext _context;

        public DatosController(DocentesDbContext context)
        {
            _context = context;
        }

        // GET: Datos
        public async Task<IActionResult> Index()
        {
            var docentesDbContext = _context.Datos.Include(d => d.IdDocentesNavigation).Include(d => d.IdGradosNavigation);
            return View(await docentesDbContext.ToListAsync());
        }

        // GET: Datos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dato = await _context.Datos
                .Include(d => d.IdDocentesNavigation)
                .Include(d => d.IdGradosNavigation)
                .FirstOrDefaultAsync(m => m.IdDatos == id);
            if (dato == null)
            {
                return NotFound();
            }

            return View(dato);
        }

        // GET: Datos/Create
        public IActionResult Create()
        {
            ViewData["IdDocentes"] = new SelectList(_context.Docentes, "IdDocente", "IdDocente");
            ViewData["IdGrados"] = new SelectList(_context.Grados, "IdGrado", "IdGrado");
            return View();
        }

        // POST: Datos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDatos,IdGrados,IdDocentes,DatosAcademicos")] Dato dato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDocentes"] = new SelectList(_context.Docentes, "IdDocente", "IdDocente", dato.IdDocentes);
            ViewData["IdGrados"] = new SelectList(_context.Grados, "IdGrado", "IdGrado", dato.IdGrados);
            return View(dato);
        }

        // GET: Datos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dato = await _context.Datos.FindAsync(id);
            if (dato == null)
            {
                return NotFound();
            }
            ViewData["IdDocentes"] = new SelectList(_context.Docentes, "IdDocente", "IdDocente", dato.IdDocentes);
            ViewData["IdGrados"] = new SelectList(_context.Grados, "IdGrado", "IdGrado", dato.IdGrados);
            return View(dato);
        }

        // POST: Datos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDatos,IdGrados,IdDocentes,DatosAcademicos")] Dato dato)
        {
            if (id != dato.IdDatos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatoExists(dato.IdDatos))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDocentes"] = new SelectList(_context.Docentes, "IdDocente", "IdDocente", dato.IdDocentes);
            ViewData["IdGrados"] = new SelectList(_context.Grados, "IdGrado", "IdGrado", dato.IdGrados);
            return View(dato);
        }

        // GET: Datos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dato = await _context.Datos
                .Include(d => d.IdDocentesNavigation)
                .Include(d => d.IdGradosNavigation)
                .FirstOrDefaultAsync(m => m.IdDatos == id);
            if (dato == null)
            {
                return NotFound();
            }

            return View(dato);
        }

        // POST: Datos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dato = await _context.Datos.FindAsync(id);
            if (dato != null)
            {
                _context.Datos.Remove(dato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatoExists(int id)
        {
            return _context.Datos.Any(e => e.IdDatos == id);
        }
    }
}

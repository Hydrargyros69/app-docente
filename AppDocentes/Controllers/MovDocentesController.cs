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
    public class MovDocentesController : Controller
    {
        private readonly DocentesDbContext _context;

        public MovDocentesController(DocentesDbContext context)
        {
            _context = context;
        }

        // GET: MovDocentes
        public async Task<IActionResult> Index()
        {
            var docentesDbContext = _context.MovDocentes.Include(m => m.IdDocenteNavigation).Include(m => m.IdHorarioNavigation).Include(m => m.IdModuloNavigation).Include(m => m.IdSede1).Include(m => m.IdSedeNavigation).Include(m => m.IdSemestreNavigation);
            return View(await docentesDbContext.ToListAsync());
        }

        // GET: MovDocentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movDocente = await _context.MovDocentes
                .Include(m => m.IdDocenteNavigation)
                .Include(m => m.IdHorarioNavigation)
                .Include(m => m.IdModuloNavigation)
                .Include(m => m.IdSede1)
                .Include(m => m.IdSedeNavigation)
                .Include(m => m.IdSemestreNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movDocente == null)
            {
                return NotFound();
            }

            return View(movDocente);
        }

        // GET: MovDocentes/Create
        public IActionResult Create()
        {
            ViewData["IdDocente"] = new SelectList(_context.Docentes, "IdDocente", "IdDocente");
            ViewData["IdHorario"] = new SelectList(_context.Horarios, "IdHorario", "IdHorario");
            ViewData["IdModulo"] = new SelectList(_context.Modulos, "IdModulo", "IdModulo");
            ViewData["IdSede"] = new SelectList(_context.Sedes, "IdSede", "IdSede");
            ViewData["IdSede"] = new SelectList(_context.Escuelas, "IdEscuela", "IdEscuela");
            ViewData["IdSemestre"] = new SelectList(_context.Semestres, "IdSemestre", "IdSemestre");
            return View();
        }

        // POST: MovDocentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdDocente,IdCarrera,IdModulo,IdEscuela,IdSede,IdHorario,IdSemestre,Horas,Fecha")] MovDocente movDocente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movDocente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDocente"] = new SelectList(_context.Docentes, "IdDocente", "IdDocente", movDocente.IdDocente);
            ViewData["IdHorario"] = new SelectList(_context.Horarios, "IdHorario", "IdHorario", movDocente.IdHorario);
            ViewData["IdModulo"] = new SelectList(_context.Modulos, "IdModulo", "IdModulo", movDocente.IdModulo);
            ViewData["IdSede"] = new SelectList(_context.Sedes, "IdSede", "IdSede", movDocente.IdSede);
            ViewData["IdSede"] = new SelectList(_context.Escuelas, "IdEscuela", "IdEscuela", movDocente.IdSede);
            ViewData["IdSemestre"] = new SelectList(_context.Semestres, "IdSemestre", "IdSemestre", movDocente.IdSemestre);
            return View(movDocente);
        }

        // GET: MovDocentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movDocente = await _context.MovDocentes.FindAsync(id);
            if (movDocente == null)
            {
                return NotFound();
            }
            ViewData["IdDocente"] = new SelectList(_context.Docentes, "IdDocente", "IdDocente", movDocente.IdDocente);
            ViewData["IdHorario"] = new SelectList(_context.Horarios, "IdHorario", "IdHorario", movDocente.IdHorario);
            ViewData["IdModulo"] = new SelectList(_context.Modulos, "IdModulo", "IdModulo", movDocente.IdModulo);
            ViewData["IdSede"] = new SelectList(_context.Sedes, "IdSede", "IdSede", movDocente.IdSede);
            ViewData["IdSede"] = new SelectList(_context.Escuelas, "IdEscuela", "IdEscuela", movDocente.IdSede);
            ViewData["IdSemestre"] = new SelectList(_context.Semestres, "IdSemestre", "IdSemestre", movDocente.IdSemestre);
            return View(movDocente);
        }

        // POST: MovDocentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdDocente,IdCarrera,IdModulo,IdEscuela,IdSede,IdHorario,IdSemestre,Horas,Fecha")] MovDocente movDocente)
        {
            if (id != movDocente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movDocente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovDocenteExists(movDocente.Id))
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
            ViewData["IdDocente"] = new SelectList(_context.Docentes, "IdDocente", "IdDocente", movDocente.IdDocente);
            ViewData["IdHorario"] = new SelectList(_context.Horarios, "IdHorario", "IdHorario", movDocente.IdHorario);
            ViewData["IdModulo"] = new SelectList(_context.Modulos, "IdModulo", "IdModulo", movDocente.IdModulo);
            ViewData["IdSede"] = new SelectList(_context.Sedes, "IdSede", "IdSede", movDocente.IdSede);
            ViewData["IdSede"] = new SelectList(_context.Escuelas, "IdEscuela", "IdEscuela", movDocente.IdSede);
            ViewData["IdSemestre"] = new SelectList(_context.Semestres, "IdSemestre", "IdSemestre", movDocente.IdSemestre);
            return View(movDocente);
        }

        // GET: MovDocentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movDocente = await _context.MovDocentes
                .Include(m => m.IdDocenteNavigation)
                .Include(m => m.IdHorarioNavigation)
                .Include(m => m.IdModuloNavigation)
                .Include(m => m.IdSede1)
                .Include(m => m.IdSedeNavigation)
                .Include(m => m.IdSemestreNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movDocente == null)
            {
                return NotFound();
            }

            return View(movDocente);
        }

        // POST: MovDocentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movDocente = await _context.MovDocentes.FindAsync(id);
            if (movDocente != null)
            {
                _context.MovDocentes.Remove(movDocente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovDocenteExists(int id)
        {
            return _context.MovDocentes.Any(e => e.Id == id);
        }
    }
}

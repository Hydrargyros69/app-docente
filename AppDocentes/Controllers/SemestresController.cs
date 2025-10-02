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
    public class SemestresController : Controller
    {
        private readonly DocentesDbContext _context;

        public SemestresController(DocentesDbContext context)
        {
            _context = context;
        }

        // GET: Semestres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Semestres.ToListAsync());
        }

        // GET: Semestres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestre = await _context.Semestres
                .FirstOrDefaultAsync(m => m.IdSemestre == id);
            if (semestre == null)
            {
                return NotFound();
            }

            return View(semestre);
        }

        // GET: Semestres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Semestres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSemestre,NomSemestre")] Semestre semestre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(semestre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(semestre);
        }

        // GET: Semestres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestre = await _context.Semestres.FindAsync(id);
            if (semestre == null)
            {
                return NotFound();
            }
            return View(semestre);
        }

        // POST: Semestres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSemestre,NomSemestre")] Semestre semestre)
        {
            if (id != semestre.IdSemestre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semestre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemestreExists(semestre.IdSemestre))
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
            return View(semestre);
        }

        // GET: Semestres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestre = await _context.Semestres
                .FirstOrDefaultAsync(m => m.IdSemestre == id);
            if (semestre == null)
            {
                return NotFound();
            }

            return View(semestre);
        }

        // POST: Semestres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var semestre = await _context.Semestres.FindAsync(id);
            if (semestre != null)
            {
                _context.Semestres.Remove(semestre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SemestreExists(int id)
        {
            return _context.Semestres.Any(e => e.IdSemestre == id);
        }
    }
}

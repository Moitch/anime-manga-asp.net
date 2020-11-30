using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP2084_Assignment1.Data;
using COMP2084_Assignment1.Models;
using Microsoft.AspNetCore.Authorization;

namespace COMP2084_Assignment1.Controllers
{
    public class AnimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Animes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Animes.Include(a => a.Genres);
            return View("Index",await applicationDbContext.ToListAsync());
        }

        // GET: Animes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var anime = await _context.Animes
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (anime == null)
            {
                return View("Error");
            }

            return View("Details",anime);
        }

        // GET: Animes/Create
        public IActionResult Create()
        {
            ViewData["GenreID"] = new SelectList(_context.Genres, "ID", "Name");
            return View("Create");
        }

        // POST: Animes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,GenreID,Name,Episodes,Status,AirStart,AirEnd,Studios")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["GenreID"] = new SelectList(_context.Genres, "ID", "Name", anime.GenreID);
            return View("Create",anime);
        }

        // GET: Animes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var anime = await _context.Animes.FindAsync(id);
            if (anime == null)
            {
                return View("Error");
            }
            ViewData["GenreID"] = new SelectList(_context.Genres, "ID", "Name", anime.GenreID);
            return View("Edit",anime);
        }

        // POST: Animes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,GenreID,Name,Episodes,Status,AirStart,AirEnd,Studios")] Anime anime)
        {
            if (id != anime.ID)
            {
                return View("Error"); ;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeExists(anime.ID))
                    {
                        return View("Error");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreID"] = new SelectList(_context.Genres, "ID", "Name", anime.GenreID);
            return View(anime);
        }

        // GET: Animes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var anime = await _context.Animes
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (anime == null)
            {
                return View("Error");
            }

            return View("Delete",anime);
        }

        // POST: Animes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anime = await _context.Animes.FindAsync(id);
            _context.Animes.Remove(anime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeExists(int id)
        {
            return _context.Animes.Any(e => e.ID == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRex.Models;

namespace MovieRex.Controllers
{
    public class GenreController : BaseController
    {
        private readonly MovieRexDBContext _context;

        public GenreController(MovieRexDBContext context)
        {
            _context = context;
        }

        // GET: Genre
        public async Task<IActionResult> Index()
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                return _context.Genres != null ?
                          View(await _context.Genres.ToListAsync()) :
                          Problem("Entity set 'MovieRexDBContext.Genres'  is null.");
            }
            return RedirectToAction("Index", "Home");
            
        }

        // GET: Genre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.Genres == null)
                {
                    return NotFound();
                }

                var genre = await _context.Genres
                    .FirstOrDefaultAsync(m => m.GenreID == id);
                if (genre == null)
                {
                    return NotFound();
                }

                return View(genre);
            }
            return RedirectToAction("Index", "Home");
            
        }

        // GET: Genre/Create
        public IActionResult Create()
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
            
        }

        // POST: Genre/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreID,GenreTitle")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.Genres == null)
                {
                    return NotFound();
                }

                var genre = await _context.Genres.FindAsync(id);
                if (genre == null)
                {
                    return NotFound();
                }
                return View(genre);
            }
            return RedirectToAction("Index", "Home");            
        }

        // POST: Genre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GenreID,GenreTitle")] Genre genre)
        {
            if (id != genre.GenreID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.GenreID))
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
            return View(genre);
        }

        // GET: Genre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.Genres == null)
                {
                    return NotFound();
                }

                var genre = await _context.Genres
                    .FirstOrDefaultAsync(m => m.GenreID == id);
                if (genre == null)
                {
                    return NotFound();
                }

                return View(genre);
            }
            return RedirectToAction("Index", "Home");
            
        }

        // POST: Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Genres == null)
            {
                return Problem("Entity set 'MovieRexDBContext.Genres'  is null.");
            }
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(int id)
        {
          return (_context.Genres?.Any(e => e.GenreID == id)).GetValueOrDefault();
        }
    }
}

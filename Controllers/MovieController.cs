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
    public class MovieController : BaseController
    {
        private readonly MovieRexDBContext _context;

        public MovieController(MovieRexDBContext context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                return _context.Movies != null ?
                          View(await _context.Movies.ToListAsync()) :
                          Problem("Entity set 'MovieRexDBContext.Movies'  is null.");
            }
            return RedirectToAction("Index", "Home");
            
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.Movies == null)
                {
                    return NotFound();
                }

                var movie = await _context.Movies
                    .FirstOrDefaultAsync(m => m.MovieID == id);
                if (movie == null)
                {
                    return NotFound();
                }

                return View(movie);
            }
            return RedirectToAction("Index", "Home");
            
        }

        // GET: Movie/Create
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

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieID,Title,OverView,Backdrop_Path,Release_Date,Vote,Country,Limit_Age,Duration")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.Movies == null)
                {
                    return NotFound();
                }

                var movie = await _context.Movies.FindAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }
                return View(movie);
            }
            return RedirectToAction("Index", "Home");
            
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MovieID,Title,OverView,Backdrop_Path,Release_Date,Vote,Country,Limit_Age,Duration")] Movie movie)
        {
            if (id != movie.MovieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieID))
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
            return View(movie);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.Movies == null)
                {
                    return NotFound();
                }

                var movie = await _context.Movies
                    .FirstOrDefaultAsync(m => m.MovieID == id);
                if (movie == null)
                {
                    return NotFound();
                }

                return View(movie);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'MovieRexDBContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(string id)
        {
          return (_context.Movies?.Any(e => e.MovieID == id)).GetValueOrDefault();
        }
    }
}

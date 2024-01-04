using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRex.Models;

namespace MovieRex.Controllers
{
    public class MovieGenreController : BaseController
    {
        private readonly MovieRexDBContext _context;

        public MovieGenreController(MovieRexDBContext context)
        {
            _context = context;
        }

        // GET: MovieGenre
        public async Task<IActionResult> Index()
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                var movieRexDBContext = _context.MovieGenres.Include(m => m.Genre).Include(m => m.Movie);
                return View(await movieRexDBContext.ToListAsync());
            }
            return RedirectToAction("Index", "Home");
            
        }

        // GET: MovieGenre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.MovieGenres == null)
                {
                    return NotFound();
                }

                var movieGenre = await _context.MovieGenres
                    .Include(m => m.Genre)
                    .Include(m => m.Movie)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (movieGenre == null)
                {
                    return NotFound();
                }

                return View(movieGenre);
            }
            return RedirectToAction("Index", "Home");            
        }

        // GET: MovieGenre/Create
        public IActionResult Create()
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreTitle");
                ViewData["MovieID"] = new SelectList(_context.Movies, "MovieID", "Title");
                return View();
            }
            return RedirectToAction("Index", "Home");            
        }

        // POST: MovieGenre/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MovieID,GenreID")] MovieGenre movieGenre)
        {
            try
            {
                var MovieGenre = await _context.MovieGenres
                    .Where(m => m.MovieID == movieGenre.MovieID && m.GenreID == movieGenre.GenreID)
                    .FirstOrDefaultAsync();

                if (MovieGenre != null)
                {
                    Console.WriteLine(MovieGenre);
                }
                else
                {
                    ModelState["Movie"].ValidationState = ModelValidationState.Valid;
                    ModelState["Genre"].ValidationState = ModelValidationState.Valid;
                    if (ModelState.IsValid)
                    {
                        _context.Add(movieGenre);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreTitle", movieGenre.GenreID);
                    ViewData["MovieID"] = new SelectList(_context.Movies, "MovieID", "Title", movieGenre.MovieID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn cơ sở dữ liệu: " + ex.Message);
            }            
            
            return View(movieGenre);
        }

        // GET: MovieGenre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.MovieGenres == null)
                {
                    return NotFound();
                }

                var movieGenre = await _context.MovieGenres.FindAsync(id);
                if (movieGenre == null)
                {
                    return NotFound();
                }
                ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreTitle", movieGenre.GenreID);
                ViewData["MovieID"] = new SelectList(_context.Movies, "MovieID", "Title", movieGenre.MovieID);
                return View(movieGenre);
            }
            return RedirectToAction("Index", "Home");            
        }

        // POST: MovieGenre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MovieID,GenreID")] MovieGenre movieGenre)
        {
            if (id != movieGenre.ID)
            {
                return NotFound();
            }
            ModelState["Movie"].ValidationState = ModelValidationState.Valid;
            ModelState["Genre"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieGenreExists(movieGenre.ID))
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
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreTitle", movieGenre.GenreID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "MovieID", "Title", movieGenre.MovieID);
            return View(movieGenre);
        }

        // GET: MovieGenre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.MovieGenres == null)
                {
                    return NotFound();
                }

                var movieGenre = await _context.MovieGenres
                    .Include(m => m.Genre)
                    .Include(m => m.Movie)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (movieGenre == null)
                {
                    return NotFound();
                }

                return View(movieGenre);
            }
            return RedirectToAction("Index", "Home");            
        }

        // POST: MovieGenre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MovieGenres == null)
            {
                return Problem("Entity set 'MovieRexDBContext.MovieGenres'  is null.");
            }
            var movieGenre = await _context.MovieGenres.FindAsync(id);
            if (movieGenre != null)
            {
                _context.MovieGenres.Remove(movieGenre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieGenreExists(int id)
        {
          return (_context.MovieGenres?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}

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
    public class MovieActorController : BaseController
    {
        private readonly MovieRexDBContext _context;

        public MovieActorController(MovieRexDBContext context)
        {
            _context = context;
        }

        // GET: MovieActor
        public async Task<IActionResult> Index()
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                var movieRexDBContext = _context.MovieActors.Include(m => m.Actor).Include(m => m.Movie);
                return View(await movieRexDBContext.ToListAsync());
            }
            return RedirectToAction("Index", "Home");            
        }

        // GET: MovieActor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.MovieActors == null)
                {
                    return NotFound();
                }

                var movieActor = await _context.MovieActors
                    .Include(m => m.Actor)
                    .Include(m => m.Movie)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (movieActor == null)
                {
                    return NotFound();
                }

                return View(movieActor);
            }
            return RedirectToAction("Index", "Home");
            
        }

        // GET: MovieActor/Create
        public IActionResult Create()
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                ViewData["ActorID"] = new SelectList(_context.Actors, "ActorID", "Name");
                ViewData["MovieID"] = new SelectList(_context.Movies, "MovieID", "Title");
                return View();
            }
            return RedirectToAction("Index", "Home");            
        }

        // POST: MovieActor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MovieID,ActorID")] MovieActor movieActor)
        {            
            try
            {
                var MovieActor = await _context.MovieActors
                    .Where(m => m.MovieID == movieActor.MovieID && m.ActorID == movieActor.ActorID)
                    .FirstOrDefaultAsync();

                if (MovieActor != null)
                {
                    Console.WriteLine(MovieActor);
                }
                else
                {                    
                    ModelState["Movie"].ValidationState = ModelValidationState.Valid;
                    ModelState["Actor"].ValidationState = ModelValidationState.Valid;
                    if (ModelState.IsValid)
                    {
                        _context.Add(movieActor);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["ActorID"] = new SelectList(_context.Actors, "ActorID", "Name", movieActor.ActorID);
                    ViewData["MovieID"] = new SelectList(_context.Movies, "MovieID", "Title", movieActor.MovieID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn cơ sở dữ liệu: " + ex.Message);
            }            
            return View(movieActor);
        }

        // GET: MovieActor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.MovieActors == null)
                {
                    return NotFound();
                }

                var movieActor = await _context.MovieActors.FindAsync(id);
                if (movieActor == null)
                {
                    return NotFound();
                }
                ViewData["ActorID"] = new SelectList(_context.Actors, "ActorID", "Name", movieActor.ActorID);
                ViewData["MovieID"] = new SelectList(_context.Movies, "MovieID", "Title", movieActor.MovieID);
                return View(movieActor);
            }
            return RedirectToAction("Index", "Home");            
        }

        // POST: MovieActor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MovieID,ActorID")] MovieActor movieActor)
        {
            if (id != movieActor.ID)
            {
                return NotFound();
            }
            ModelState["Movie"].ValidationState = ModelValidationState.Valid;
            ModelState["Actor"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieActor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieActorExists(movieActor.ID))
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
            ViewData["ActorID"] = new SelectList(_context.Actors, "ActorID", "Name", movieActor.ActorID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "MovieID", "Title", movieActor.MovieID);
            return View(movieActor);
        }

        // GET: MovieActor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.MovieActors == null)
                {
                    return NotFound();
                }

                var movieActor = await _context.MovieActors
                    .Include(m => m.Actor)
                    .Include(m => m.Movie)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (movieActor == null)
                {
                    return NotFound();
                }

                return View(movieActor);
            }
            return RedirectToAction("Index", "Home");            
        }

        // POST: MovieActor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MovieActors == null)
            {
                return Problem("Entity set 'MovieRexDBContext.MovieActors'  is null.");
            }
            var movieActor = await _context.MovieActors.FindAsync(id);
            if (movieActor != null)
            {
                _context.MovieActors.Remove(movieActor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieActorExists(int id)
        {
          return (_context.MovieActors?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}

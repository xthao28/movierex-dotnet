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
    public class ActorController : BaseController
    {
        private readonly MovieRexDBContext _context;

        public ActorController(MovieRexDBContext context)
        {
            _context = context;
        }

        // GET: Actor
        public async Task<IActionResult> Index()
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                return _context.Actors != null ?
                              View(await _context.Actors.ToListAsync()) :
                              Problem("Entity set 'MovieRexDBContext.Actors'  is null.");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Actor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.Actors == null)
                {
                    return NotFound();
                }

                var actor = await _context.Actors
                    .FirstOrDefaultAsync(m => m.ActorID == id);
                if (actor == null)
                {
                    return NotFound();
                }

                return View(actor);
            }
            return RedirectToAction("Index", "Home");
            
        }

        // GET: Actor/Create
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

        // POST: Actor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorID,Name,Gender")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.Actors == null)
                {
                    return NotFound();
                }

                var actor = await _context.Actors.FindAsync(id);
                if (actor == null)
                {
                    return NotFound();
                }
                return View(actor);
            }
            return RedirectToAction("Index", "Home");            
        }

        // POST: Actor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActorID,Name,Gender")] Actor actor)
        {
            if (id != actor.ActorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.ActorID))
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
            return View(actor);
        }

        // GET: Actor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                if (id == null || _context.Actors == null)
                {
                    return NotFound();
                }

                var actor = await _context.Actors
                    .FirstOrDefaultAsync(m => m.ActorID == id);
                if (actor == null)
                {
                    return NotFound();
                }

                return View(actor);
            }
            return RedirectToAction("Index", "Home");            
        }

        // POST: Actor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Actors == null)
            {
                return Problem("Entity set 'MovieRexDBContext.Actors'  is null.");
            }
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
          return (_context.Actors?.Any(e => e.ActorID == id)).GetValueOrDefault();
        }
    }
}

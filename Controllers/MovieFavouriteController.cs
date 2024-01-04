using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRex.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRex.Controllers
{
    public class MovieFavouriteController : BaseController
    {
        private readonly MovieRexDBContext _context;

        public MovieFavouriteController(MovieRexDBContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            var movieFavourite = _context.MovieFavourites.Where(m => m.UserName == CurrentUser).Include(m => m.Movie);            
            return View(await movieFavourite.ToListAsync());
        }
    }
}


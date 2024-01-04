using System.Collections;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRex.Models;

namespace MovieRex.Controllers;

public class HomeController : BaseController
{
    private readonly MovieRexDBContext _context;

    public HomeController(MovieRexDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        if (!IsLogin)
        {
            return RedirectToAction("Login", "Account");
        }
        return _context.Movies != null ?
                          View(await _context.Movies.ToListAsync()) :
                          Problem("Entity set 'MovieRexDBContext.Movies'  is null.");        
    }

    public async Task<IActionResult> SearchMV(IFormCollection collect)
    {        
        if (collect["submit"].ToString() == "Search")
        {
            string keyword = collect["txtTimKiem"].ToString().ToLower();
            var model = await _context.MovieGenres
                .Where(m => m.Genre.GenreTitle == "Phim" && m.Movie.Title.ToLower().Contains(keyword))
                .Include(m => m.Movie)                
                .ToListAsync();            
            return View("Movie", model);
        }
        else
        {
            return RedirectToAction("Movie");
        }
    }

    public async Task<IActionResult> Details(string id)
    {
        if (!IsLogin)
        {
            return RedirectToAction("Login", "Account");
        }
        if (id == null || _context.Movies == null)
        {
            return NotFound();
        }
        //Movie Actor ->
        var movieActor = await _context.MovieActors
            .Where(m => m.MovieID == id)
            .Include(m => m.Actor)
            .ToListAsync();       
        string actor = string.Join(",", movieActor.Select(m => m.Actor.Name));
        ViewBag.MovieActor = actor;
        //<- Movie Actor

        //Movie Genre ->
        var movieGenre = await _context.MovieGenres
            .Where(m => m.MovieID == id)
            .Include(m => m.Genre).ToListAsync();
        string genre = string.Join(",", movieGenre.Select(m => m.Genre.GenreTitle));
        ViewBag.MovieGenre = genre;
        //<- Movie Genre

        // Movie Similar ->
        var listGenreID = movieGenre.Select(m => m.Genre.GenreID);
        var movieSimilar = await _context.MovieGenres
            .Where(m => listGenreID.Contains(m.GenreID)).Include(m => m.Movie).ToListAsync();
        movieSimilar = movieSimilar.GroupBy(m => m.MovieID)
                .Select(m => m.First()).ToList();
        ViewBag.MovieSimilar = movieSimilar.Skip(1);
        //<- Movie Similar

        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.MovieID == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    public async Task<IActionResult> Movie()
    {
        if (!IsLogin)
        {
            return RedirectToAction("Login", "Account");
        }

        var movie = await _context.MovieGenres
            .Where(m => m.Genre.GenreTitle == "Phim")
            .Include(m => m.Movie).ToListAsync();

        return View(movie);
    }

        public async Task<IActionResult> TV()
    {
        if (!IsLogin)
        {
            return RedirectToAction("Login", "Account");
        }

        var tv = await _context.MovieGenres
            .Where(m => m.Genre.GenreTitle == "Phim Truyền Hình")
            .Include(m => m.Movie).ToListAsync();

        return View(tv);
    }
    public async Task<IActionResult> SearchTV(IFormCollection collect)
    {
        if (collect["submit"].ToString() == "Search")
        {
            string keyword = collect["txtTimKiem"].ToString().ToLower();
            var model = await _context.MovieGenres
                .Where(m => m.Genre.GenreTitle == "Phim Truyền Hình" && m.Movie.Title.ToLower().Contains(keyword))
                .Include(m => m.Movie)
                .ToListAsync();
            return View("TV", model);
        }
        else
        {
            return RedirectToAction("TV");
        }
    }

    public async Task<IActionResult> AddMovieFavourite(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        try
        {
            var movieFavourite = await _context.MovieFavourites
                .Where(m => m.MovieID == id && m.UserName == CurrentUser)
                .FirstOrDefaultAsync();
            
            if (movieFavourite != null)
            {
                Console.WriteLine(movieFavourite);
            }
            else
            {
                var mF = new MovieFavourite()
                {
                    MovieID = id,
                    UserName = CurrentUser
                };
                _context.MovieFavourites.Add(mF);
                _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {            
            Console.WriteLine("Đã xảy ra lỗi khi truy vấn cơ sở dữ liệu: " + ex.Message);            
        }                             
        return RedirectToAction("Index", "MovieFavourite");
    }

    public async Task<IActionResult> RemoveMovieFavourite(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        try
        {
            var movieFavourite = await _context.MovieFavourites
                .Where(m => m.MovieID == id && m.UserName == CurrentUser)
                .FirstOrDefaultAsync();

            if (movieFavourite != null)
            {               
                _context.MovieFavourites.Remove(movieFavourite);
                _context.SaveChangesAsync();                
            }
            else
            {
                Console.WriteLine(movieFavourite);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Đã xảy ra lỗi khi truy vấn cơ sở dữ liệu: " + ex.Message);
        }
        return RedirectToAction("Index", "MovieFavourite");
    }

    public async Task<IActionResult> Video(string id)
    {
        if (!IsLogin)
        {
            return RedirectToAction("Login", "Account");
        }
        if (id == null)
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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


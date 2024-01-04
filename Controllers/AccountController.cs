using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRex.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRex.Controllers
{
    public class AccountController : BaseController
    {
        private readonly MovieRexDBContext _context;

        public AccountController(MovieRexDBContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("ID, UserName, Password")] Account model)
        {
            if (ModelState.IsValid)
            {
                var loginUser = await _context.Accounts.FirstOrDefaultAsync(m => m.UserName == model.UserName);
                if (loginUser == null)
                {
                    ModelState.AddModelError("", "Dang nhap that bai");
                    return View(model);
                }
                else
                { 
                    SHA256 hashMenthod = SHA256.Create();
                    if (Util.Cryptography.VerifyHash(hashMenthod, model.Password, loginUser.Password))
                    {
                        CurrentUser = loginUser.UserName;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {                    
                        ModelState.AddModelError("", "Dang nhap that bai");
                        return View(model);
                    }
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName,Password")] Account model)
        {
            if (ModelState.IsValid)
            {
                SHA256 hashMenthod = SHA256.Create();
                model.Password = Util.Cryptography.GetHash(hashMenthod, model.Password);

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(model);
        }
        public IActionResult Logout()
        {
            CurrentUser = "";
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Index()
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (CurrentUser == "admin")
            {
                return _context.Accounts != null ?
                              View(await _context.Accounts.ToListAsync()) :
                              Problem("Entity set 'MovieRexDBContext.Actors'  is null.");
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Details(string id)
        {
            if (!IsLogin)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
    }
}


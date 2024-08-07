using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionSchool.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using BCrypt.Net;

namespace GestionSchool.Controllers
{
    public class LoginsController : Controller
    {
        private readonly SchoolCrudContext _context;

        public LoginsController(SchoolCrudContext context)
        {
            _context = context;
        }

        // GET: Logins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Logins.ToListAsync());
        }

        // GET: UsuarioLogins
        public IActionResult InicioSesion()
        {
            return View();
        }

        // GET: Registro
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InicioSesion(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Logins
                    .FirstOrDefaultAsync(u => u.NombreUsuario == model.NombreUsuario);

                if (user != null && BCrypt.Net.BCrypt.Verify(model.Contrasena, user.Contrasena))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.NombreUsuario),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("Contrasena", "Nombre de usuario o contraseña incorrectos");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registro(Login model)
        {
            if (ModelState.IsValid)
            {
                model.Contrasena = BCrypt.Net.BCrypt.HashPassword(model.Contrasena, BCrypt.Net.BCrypt.GenerateSalt());
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("InicioSesion");
            }
            return View(model);
        }
    }
}

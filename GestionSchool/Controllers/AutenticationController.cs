using GestionSchool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace GestionSchool.Controllers
{
    public class AutenticationController : Controller
    {
        private readonly SchoolCrudContext _context;

        public AutenticationController(SchoolCrudContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Logins.FirstOrDefaultAsync(u => u.NombreUsuario == model.NombreUsuario);

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

                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Login
                {
                    NombreUsuario = model.NombreUsuario,
                    Contrasena = BCrypt.Net.BCrypt.HashPassword(model.Contrasena, BCrypt.Net.BCrypt.GenerateSalt())
                };

                _context.Logins.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Autentication");
        }
    }
}

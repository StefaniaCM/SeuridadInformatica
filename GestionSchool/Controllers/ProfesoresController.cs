using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionSchool.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestionSchool.Controllers
{
    [Authorize]
    public class ProfesoresController : Controller
    {
        private readonly SchoolCrudContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProfesoresController(SchoolCrudContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }   

        // GET: Profesores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profesores.ToListAsync());
        }

        // GET: Profesores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesore = await _context.Profesores
                .FirstOrDefaultAsync(m => m.DocumentoIdentidad == id);
            if (profesore == null)
            {
                return NotFound();
            }

            return View(profesore);
        }

        // GET: Profesores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Profesore profesore)
        {
            if (ModelState.IsValid)
            {
                string uFileName = UploadFile(profesore);
                profesore.ImagenUrl = uFileName;
                _context.Add(profesore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profesore);
        }

        // GET: Profesores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesore = await _context.Profesores.FindAsync(id);
            if (profesore == null)
            {
                return NotFound();
            }
            return View(profesore);
        }

        // POST: Profesores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Profesores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocumentoIdentidad,Nombres,Apellidos,CorreoElectronico,Profesion,ImagenUrl,ImagenFile")] Profesore profesore)
        {
            if (id != profesore.DocumentoIdentidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (profesore.ImagenFile != null)
                    {
                        string uFileName = UploadFile(profesore);
                        profesore.ImagenUrl = uFileName;
                    }
                    _context.Update(profesore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesoreExists(profesore.DocumentoIdentidad))
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
            return View(profesore);
        }


        // GET: Profesores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesore = await _context.Profesores
                .FirstOrDefaultAsync(m => m.DocumentoIdentidad == id);
            if (profesore == null)
            {
                return NotFound();
            }

            return View(profesore);
        }

        // POST: Profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesore = await _context.Profesores
                
                .FirstOrDefaultAsync(p => p.DocumentoIdentidad == id);
                
            if (profesore != null)
            {
               
                _context.Profesores.Remove(profesore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesoreExists(int id)
        {
            return _context.Profesores.Any(e => e.DocumentoIdentidad == id);
        }

        private string UploadFile(Profesore profesore)
        {
            string uFileName = null;

            if (profesore.ImagenFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uFileName = Guid.NewGuid().ToString() + "_" + profesore.ImagenFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uFileName);
                using (var myFileStream = new FileStream(filePath, FileMode.Create))
                {
                    profesore.ImagenFile.CopyTo(myFileStream);
                }
            }
            return uFileName;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_oc_fougasse_lover.Data;
using projet_oc_fougasse_lover.Models;

namespace projet_oc_fougasse_lover.Controllers
{
    public class FougassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FougassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fougasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fougasses.ToListAsync());
        }

        // GET: Fougasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fougasse = await _context.Fougasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fougasse == null)
            {
                return NotFound();
            }

            return View(fougasse);
        }

        // GET: Fougasses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fougasses/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Fougasses fougasse, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Crée le dossier si nécessaire
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    // Crée un nom de fichier unique
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(uploadPath, fileName);

                    // Sauvegarde le fichier
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Enregistre le chemin relatif (pour affichage dans le site)
                    fougasse.ImageUrl = "/images/" + fileName;
                }

                _context.Add(fougasse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(fougasse);
        }


        // GET: Fougasses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fougasse = await _context.Fougasses.FindAsync(id);
            if (fougasse == null)
            {
                return NotFound();
            }

            return View(fougasse);
        }

        // POST: Fougasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Fougasses fougasse, IFormFile imageFile)
        {
            if (id != fougasse.Id)
                return NotFound();

            var fougasseInDb = await _context.Fougasses.FindAsync(id);
            if (fougasseInDb == null)
                return NotFound();

            fougasseInDb.Name = fougasse.Name;
            fougasseInDb.Description = fougasse.Description;
            fougasseInDb.Text = fougasse.Text;
            fougasseInDb.Price = fougasse.Price;
            fougasseInDb.Adress = fougasse.Adress;

            if (imageFile != null && imageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(fougasseInDb.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fougasseInDb.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Enregistrer la nouvelle image
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploads);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                fougasseInDb.ImageUrl = "/images/" + fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Fougasses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fougasse = await _context.Fougasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fougasse == null)
            {
                return NotFound();
            }

            return View(fougasse);
        }

        // POST: Fougasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fougasse = await _context.Fougasses.FindAsync(id);
            if (fougasse != null)
            {
                _context.Fougasses.Remove(fougasse);
            }

            if (!string.IsNullOrEmpty(fougasse.ImageUrl))
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fougasse.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Fougasses/About
        public IActionResult About()
        {
            return View();
        }

        // GET: Fougasses/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        private bool FougasseExists(int id)
        {
            return _context.Fougasses.Any(e => e.Id == id);
        }
    }
}

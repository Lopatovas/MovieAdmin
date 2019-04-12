using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieAdministrator.Database;
using MovieAdministrator.Models;

namespace MovieAdministrator.Controllers
{
    public class CastsController : Controller
    {
        private readonly DatabaseContext _context;

        public CastsController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Movie movie = _context.Movies.Where(mov => mov.ID.Equals(id)).FirstOrDefault();
            ViewData["Movie"] = movie;
            var databaseContext = _context.Casts.Include(c => c.Actor).Include(c => c.Movie).Where(s => s.MovieID.Equals(id));
            if (databaseContext == null)
            {
                return NotFound();
            }
            return View(await databaseContext.ToListAsync());
        }

        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Movie movie = _context.Movies.Where(mov => mov.ID.Equals(id)).FirstOrDefault();
            ViewData["Movie"] = movie;
            ViewData["ActorID"] = new SelectList(_context.Actors, "ID", "FullName");
            ViewData["MovieID"] = new SelectList(_context.Movies.Where(mov => mov.ID == id), "ID", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorID,MovieID")] Cast cast)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = cast.MovieID });
            }
            ViewData["ActorID"] = new SelectList(_context.Actors, "ID", "LastName", cast.ActorID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "Title", cast.MovieID);
            return View(cast);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cast = await _context.Casts
                .Include(c => c.Actor)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cast == null)
            {
                return NotFound();
            }

            return View(cast);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cast = await _context.Casts.FindAsync(id);
            _context.Casts.Remove(cast);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = cast.MovieID });
        }

        private bool CastExists(int id)
        {
            return _context.Casts.Any(e => e.ID == id);
        }

        public JsonResult IsExists(int ActorID, int MovieID)
        {
            var validateName = _context.Casts.FirstOrDefault
                                (x => x.ActorID.Equals(ActorID) && x.MovieID.Equals(MovieID));
            if (validateName != null)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
    }
}

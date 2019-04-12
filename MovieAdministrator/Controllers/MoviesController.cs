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
    public class MoviesController : Controller
    {
        private readonly DatabaseContext _context;

        public MoviesController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string titleFilter, DateTime dateFilter, string genreFilter)
        {
            ViewData["TitleFilter"] = titleFilter;
            ViewData["DateFilter"] = dateFilter;
            ViewData["GenreFilter"] = genreFilter;
            var movies = from mov in _context.Movies select mov;
            movies = _context.Movies.Include(m => m.Genre);
            if (!String.IsNullOrEmpty(titleFilter))
                movies = movies.Where(s => s.Title.Contains(titleFilter));
            if (!String.IsNullOrEmpty(genreFilter))
                movies = movies.Where(s => s.Genre.Name.Contains(genreFilter));
            if (dateFilter.Year != 0001)
                movies = movies.Where(s => s.ReleaseDate.Date.Equals(dateFilter));

            return View(await movies.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(cst => cst.Casts)
                .ThenInclude(cst => cst.Actor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public IActionResult Create()
        {
            ViewData["GenreID"] = new SelectList(_context.Genres, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,GenreID")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreID"] = new SelectList(_context.Genres, "ID", "ID", movie.GenreID);
            return View(movie);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["GenreID"] = new SelectList(_context.Genres, "ID", "Name", movie.GenreID);
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,GenreID")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
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
            ViewData["GenreID"] = new SelectList(_context.Genres, "ID", "ID", movie.GenreID);
            return View(movie);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }

        public JsonResult IsExists(string title, DateTime releaseDate)
        {
            var validateMovie = _context.Movies.FirstOrDefault
                                (x => x.Title.Equals(title) && x.ReleaseDate.Equals(releaseDate));
            if (validateMovie != null)
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

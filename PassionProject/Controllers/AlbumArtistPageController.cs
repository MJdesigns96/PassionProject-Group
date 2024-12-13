using Microsoft.AspNetCore.Mvc;
using PassionProject.Data;
using Microsoft.EntityFrameworkCore;
using PassionProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace PassionProject.Controllers
{
    public class AlbumArtistsPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlbumArtistsPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AlbumArtistsPage
        public async Task<IActionResult> Index()
        {
            return View(await _context.AlbumArtists.ToListAsync());
        }

        /* GET: AlbumArtists/index
         * sortOrder Function
         */
        [HttpGet("AlbumArtists/index")]
        public IActionResult Index(string sortOrder)
        {
            var artists = _context.AlbumArtists.AsQueryable();

            if (sortOrder == "name")
            {
                artists = artists.OrderBy(a => a.AlbumArtistName);
            }

            return View(artists.ToList());
        }

        // GET: AlbumArtists/Details
        public IActionResult Details(int id)
        {
            var artist = _context.AlbumArtists
                .Include(a => a.Albums)
                .FirstOrDefault(a => a.AlbumArtistId == id);

            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // GET: AlbumArtists/Create
        //[Authorize(Roles = "admin")]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        //POST: AlbumArtists/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("AlbumArtistName,AlbumArtistBio")] AlbumArtist AlbumArtist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(AlbumArtist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(AlbumArtist);
        }

        // GET: AlbumArtists/Edit
        //[Authorize(Roles = "admin")]
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.AlbumArtists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: AlbumArtists/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumArtistId,AlbumArtistName,AlbumArtistBio")] AlbumArtist AlbumArtist)
        {
            if (id != AlbumArtist.AlbumArtistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(AlbumArtist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(AlbumArtist);
        }

        // GET: AlbumArtists/Delete
        //[Authorize(Roles = "admin")]
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.AlbumArtists.FirstOrDefaultAsync(m => m.AlbumArtistId == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: AlbumArtists/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _context.AlbumArtists.FindAsync(id);
            _context.AlbumArtists.Remove(artist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

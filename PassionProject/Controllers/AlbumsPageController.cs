using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PassionProject.Data;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class AlbumsPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlbumsPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AlbumsPage
        public async Task<IActionResult> Index()
        {
            return View(await _context.Albums.ToListAsync());
        }

        /* GET: Albums/Index 
         * sortOrder Function
         */
        [HttpGet("albums/index")]
        public IActionResult Index(string sortOrder)
        {
            var albums = _context.Albums.AsQueryable();

            if (sortOrder == "name")
            {
                albums = albums.OrderBy(a => a.AlbumTitle);
            }

            return View(albums.ToList());
        }

        // GET: Albums/Details
        public IActionResult Details(int id)
        {
            var album = _context.Albums
                .Include(a => a.Tracks)
                .FirstOrDefault(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            var artists = _context.AlbumArtists.ToList();
            ViewBag.ArtistSelectList = new SelectList(artists, "AlbumArtistId", "AlbumArtistName");
            return View(new Album());
        }


        // POST: Albums/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("AlbumTitle,Genre,ReleaseDate,AlbumArtistId")] Album album)
        {

            if (album.AlbumArtistId == 0)
            {
                ModelState.AddModelError("AlbumArtistId", "Artist is required.");
            }
            else
            {
                var artist = await _context.Artists.FindAsync(album.AlbumArtistId);
            }

            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var artists = await _context.AlbumArtists.ToListAsync();
            ViewBag.ArtistSelectList = new SelectList(artists, "AlbumArtistId", "AlbumArtistName");

            return View(album);
        }


        // GET: Albums/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumId,AlbumTitle,Genre,ReleaseDate,AlbumArtistId")] Album album)
        {
            if (id != album.AlbumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: Albums/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FirstOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

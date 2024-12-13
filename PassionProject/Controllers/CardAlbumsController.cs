using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using PassionProject.Data;
using PassionProject.Models;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PassionProject.Controllers
{
    public class CardAlbumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CardAlbumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CardAlbums/Create
        [HttpGet("CardAlbums/Create")]
        public IActionResult Create()
        {
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "CardName");
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumTitle");
            return View();
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Create([Bind("CardId,AlbumId")] CardAlbum cardAlbum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardAlbum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "CardName", cardAlbum.CardId);
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumTitle", cardAlbum.AlbumId);
            return View(cardAlbum);
        }

        // GET: CardAlbums/Index
        [HttpGet("CardAlbums/Index")]
        public IActionResult Index()
        {
            var cardAlbums = _context.CardAlbums.Include(c => c.Card).Include(c => c.Album).ToList();
            return View(cardAlbums);
        }

        // GET: CardAlbums/Edit/Id
        [HttpGet("CardAlbums/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var cardAlbum = _context.CardAlbums.Find(id);
            if (cardAlbum == null)
            {
                return NotFound();
            }
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "CardName", cardAlbum.CardId);
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumTitle", cardAlbum.AlbumId);
            return View(cardAlbum);
        }

        // POST: CardAlbums/Edit/Id
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CardId,AlbumId")] CardAlbum cardAlbum)
        {
            if (id != cardAlbum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(cardAlbum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "CardName", cardAlbum.CardId);
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumTitle", cardAlbum.AlbumId);
            return View(cardAlbum);
        }

        // GET: CardAlbums/Delete/Id
        [HttpGet("CardAlbums/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var cardAlbum = _context.CardAlbums
                .Include(c => c.Card)
                .Include(c => c.Album)
                .FirstOrDefault(m => m.Id == id);
            if (cardAlbum == null)
            {
                return NotFound();
            }

            return View(cardAlbum);
        }

        // POST: CardAlbums/Delete/Id
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardAlbum = await _context.CardAlbums.FindAsync(id);
            _context.CardAlbums.Remove(cardAlbum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

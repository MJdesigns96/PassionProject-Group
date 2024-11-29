using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassionProject.Data;
using PassionProject.Models;


namespace PassionProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumArtistsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlbumArtistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AlbumArtists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumArtist>>> GetAlbumArtists()
        {
            return await _context.AlbumArtists.ToListAsync();
        }

        // GET: api/AlbumArtists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumArtist>> GetArtist(int id)
        {
            var artist = await _context.AlbumArtists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        // PUT: api/AlbumArtist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, AlbumArtist AlbumArtist)
        {
            if (id != AlbumArtist.AlbumArtistId)
            {
                return BadRequest();
            }

            _context.Entry(AlbumArtist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AlbumArtists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(AlbumArtist AlbumArtist)
        {
            _context.AlbumArtists.Add(AlbumArtist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = AlbumArtist.AlbumArtistId }, AlbumArtist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _context.AlbumArtists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.AlbumArtists.Remove(artist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistExists(int id)
        {
            return _context.AlbumArtists.Any(e => e.AlbumArtistId == id);
        }
    }
}

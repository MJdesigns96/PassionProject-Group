using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassionProject.Data;
using PassionProject.Models.ViewModels;

namespace PassionProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LandingController(ApplicationDbContext context) { 
            _context = context;
        }

        // GET api/Landing
        // will return a list of cards and albums?
        [HttpGet]
        public async Task<ActionResult<CardPlusAlbum>> GetCardAlbum()
        {
            var CardList = await _context.Cards.ToListAsync();
            var AlbumsList = await _context.Albums.ToListAsync();

            var viewmodel = new CardPlusAlbum
            {
                Albums = AlbumsList,
                Cards = CardList
            };
            return viewmodel;
        }
    }
}

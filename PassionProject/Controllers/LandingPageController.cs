using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassionProject.Data;
using PassionProject.Models;
using PassionProject.Models.ViewModels;

namespace PassionProject.Controllers
{
    [Route("landing")]
    public class LandingPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LandingPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cardList = _context.Cards.ToList();
            var albumsList = _context.Albums.ToList();

            var ViewModel = new CardPlusAlbum
            {
                Albums = albumsList,
                Cards = cardList
            };

            return View(ViewModel);
        }
    }
}

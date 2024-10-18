using Microsoft.AspNetCore.Mvc;
using PassionProject.Interfaces;
using PassionProject.Models;
using PassionProject.Services;


namespace PassionProject.Controllers
{
    public class CardPageController : Controller
    {
        private readonly ICardService _cardService;

        // dependency injection of service intergace
        public CardPageController(ICardService cardService)
        {
            _cardService = cardService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: /cards/List
        public async Task<IActionResult> List()
        {
            IEnumerable<Card> Cards = await _cardService.ListCards();
            return View(Cards);
        }
    }
}

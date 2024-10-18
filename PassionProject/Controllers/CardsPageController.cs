using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using PassionProject.Data.Migrations;
using PassionProject.Interfaces;
using PassionProject.Models;
using PassionProject.Models.ViewModels;
using PassionProject.Services;


namespace PassionProject.Controllers
{
    public class CardsPageController : Controller
    {
        private readonly ICardService _cardService;
        private readonly IArtistService _artistService;
        private readonly IColorService _colorService;

        // dependency injection of service intergace
        public CardsPageController(ICardService cardService, IArtistService artistService, IColorService colorService)
        {
            _cardService = cardService;
            _artistService = artistService;
            _colorService = colorService; 
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: CardsPage/List
        public async Task<IActionResult> List()
        {
            IEnumerable<Card> Cards = await _cardService.ListCards();
            return View(Cards);
        }

        // GET: CardsPage/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Card? Card = await _cardService.FindCard(id);

            if (Card == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find Card"] });
            }
            else
            {
                return View(Card);
            }
        }

        //GET CardsPage/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Card? Card = await _cardService.FindCard(id);
            if (Card == null)
            {
                return View("Error");
            }
            return View(Card);
        }

        // POST CardsPage/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(int id, Card card)
        {
            ServiceResponse Response = await _cardService.UpdateCard(id, card);

            if (Response.Status == ServiceResponse.ServiceStatus.Updated) 
            {
                return RedirectToAction("details", "CardsPage", new { id = id });
            } else
            {
                return View("Error", new ErrorViewModel() { Errors = Response.Messages });
            }
            
        }

        // GET CardsPage/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<Artist> artists = await _artistService.ListArtists();
            IEnumerable<ColorDto> colors = await _colorService.ListColors();

            CardNew cardNew = new CardNew()
            {
                artists = artists,
                colors = colors
            };
            return View(cardNew);
        }

        // POST CardsPage/Add
        [HttpPost]
        public async Task<IActionResult> Add(Card card)
        {
            ServiceResponse response = await _cardService.AddCard(card);

            // check response
            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("List");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        // GET CardsPage/DeleteConfirm
        [HttpGet]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            Card? Card = await _cardService.FindCard(id);
            if (Card == null)
            {
                return View("Error");
            } else
            {
                return View(Card);
            }
        }

        // POST CardsPage/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _cardService.DeleteCard(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "CardsPage");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }
    }
}

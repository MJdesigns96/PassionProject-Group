using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PassionProject.Data;
using PassionProject.Interfaces;
using PassionProject.Models;
using PassionProject.Models.ViewModels;
using PassionProject.Services;

namespace PassionProject.Controllers
{
    public class ArtistsPageController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly ICardService _cardService;
        private readonly IArtistService _artistService;

        public ArtistsPageController(ICardService cardService, IArtistService artistService)
        {
            _cardService = cardService;
            _artistService = artistService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        // GET: ArtistsPage/List
        public async Task<IActionResult> List()
        {
            IEnumerable<Artist> Artists = await _artistService.ListArtists();
            return View(Artists);
        }

        // GET: ArtistsPage/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Artist? Artist = await _artistService.FindArtist(id);
            if (Artist == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find Artist"] });
            }
            else
            {
                return View(Artist);
            }
        }

        //GET ArtistsPage/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Artist? Artist = await _artistService.FindArtist(id);
            if (Artist == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find Artist"] });
            }
            else
            {
                return View(Artist);
            }
        }

        // POST ArtistsPage/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(int id, Artist Artist)
        {
            ServiceResponse Response = await _artistService.UpdateArtist(id, Artist);

            if (Response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("details", "ArtistsPage", new { id = id });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = Response.Messages });
            }
        }

        // GET ArtistsPage/Create
        public async Task<IActionResult> Create()
        {
            Artist Artist = new Artist();

            return View(Artist);
        }

        // POST ArtistsPage/Add
        [HttpPost]
        public async Task<IActionResult> Add(Artist artist)
        {
            ServiceResponse response = await _artistService.AddArtist(artist);

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

        // GET ArtistsPage/DeleteConfirm
        [HttpGet]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            Artist? Artist = await _artistService.FindArtist(id);
            if (Artist == null)
            {
                return View("Error");
            }
            else
            {
                return View(Artist);
            }
        }

        // POST ArtistsPage/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _artistService.DeleteArtist(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "ArtistsPage");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassionProject.Data;
using PassionProject.Models.ViewModels;

namespace PassionProject.Controllers
{
    public class LandingPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LandingPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("GetCardAlbum");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PassionProject.Interfaces;
using PassionProject.Models;
using PassionProject.Services;

namespace PassionProject.Controllers
{
    public class ColorsPageController : Controller
    {
        private readonly IColorService _colorService;
        public ColorsPageController(IColorService colorService)
        {
            _colorService = colorService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: ColorsPage/List
        public async Task<IActionResult> List()
        {
            IEnumerable<ColorDto> Colors = await _colorService.ListColors();
            return View(Colors);
        }

        // GET: ColorsPage/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ColorDto? ColorDto = await _colorService.FindColor(id);

            if (ColorDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find that color"] });
            }
            else
            {
                ColorDto ColorDTO = new ColorDto()
                {
                    ColorId = ColorDto.ColorId,
                    ColorName = ColorDto.ColorName,
                    CardCount = ColorDto.CardCount
                };
                return View(ColorDTO);
            }
        }
    }
}

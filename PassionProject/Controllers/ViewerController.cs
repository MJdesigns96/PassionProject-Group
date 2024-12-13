using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PassionProject.Models;
using PassionProject.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace PassionProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewerController : ControllerBase
    {
        private readonly IViewerService _viewerService;

        public ViewerController(IViewerService viewerService)
        {
            _viewerService = viewerService;
        }
        /// <summary>
        /// Gets a list of viewers in the system. Administrator only.
        /// </summary>
        /// <returns>
        /// List of type ViewerDto
        /// </returns>
        /// <example>
        /// GET: api/Viewer/ -> [{ViewerDto},{ViewerDto}]
        ///  HEADERS: Cookie: .AspNetCore.Identity.Application={token}
        /// </example>
        [HttpGet]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<ViewerDto>>> ListCustomers()
        {
            return Ok(await _viewerService.ListViewers());
        }
    }
}

using Microsoft.AspNetCore.Identity;
using PassionProject.Data;
using PassionProject.Interfaces;
using PassionProject.Models;

namespace PassionProject.Services
{
    public class ViewerService : IViewerService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public ViewerService(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<ViewerDto>> ListViewers()
        {
            IEnumerable<IdentityUser> Users = await _userManager.GetUsersInRoleAsync("Viewer");

            List<ViewerDto> ViewerDtos = new List<ViewerDto>();
            foreach (IdentityUser user in Users) 
            {
                ViewerDtos.Add(new ViewerDto()
                {
                    ViewerId = user.Id,
                    ViewerName = user.UserName,
                });
            }
            return ViewerDtos;
        }
    }
}

using PassionProject.Models;

namespace PassionProject.Interfaces
{
    public interface IViewerService
    {
        Task<IEnumerable<ViewerDto>> ListViewers();
    }
}

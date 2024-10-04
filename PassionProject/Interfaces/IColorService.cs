using PassionProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace PassionProject.Interfaces
{
    public interface IColorService
    {
        // base crud
        Task<IEnumerable<ColorDto>> ListColors();

        Task<ColorDto?> FindColor(int id);
        Task<ServiceResponse> UpdateColor(int id, Color color);
        Task<ServiceResponse> AddColor(Color color);
        Task<ServiceResponse> DeleteColor(int id);
    }
}

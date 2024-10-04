using PassionProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace PassionProject.Interfaces
{
    public interface IArtistService
    {
        // base crud
        Task<IEnumerable<Artist>> ListArtists();

        Task<Artist?> FindArtist(int id);
        Task<ServiceResponse> UpdateArtist(int id, Artist artist);
        Task<ServiceResponse> AddArtist(Artist artist);
        Task<ServiceResponse> DeleteArtist(int id);
    }
}

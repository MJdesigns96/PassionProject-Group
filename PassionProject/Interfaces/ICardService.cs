using PassionProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace PassionProject.Interfaces
{
    public interface ICardService 
    { 

        // base crud
        Task<IEnumerable<Card>> ListCards();

        Task<Card?> FindCard(int id);
        Task<ServiceResponse> UpdateCard(int id, Card card);
        Task<ServiceResponse> AddCard(Card card);
        Task<ServiceResponse> DeleteCard(int id);

    }
}

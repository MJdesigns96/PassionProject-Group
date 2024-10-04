using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PassionProject.Interfaces;
using PassionProject.Data.Migrations;
using PassionProject.Models;
using Microsoft.EntityFrameworkCore;
using PassionProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Azure;

namespace PassionProject.Services
{
    public class CardService : ICardService
    {
        private readonly ApplicationDbContext _context;

        public CardService(ApplicationDbContext context)
        {
            // injection of DB _context
            _context = context;
        }

        public async Task<IEnumerable<Card>> ListCards()
        {
            // get all cards in db
            return await _context.Cards.ToListAsync();
        }

        public async Task<Card> FindCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return null;
            }

            return card;
        }

        public async Task<ServiceResponse> UpdateCard(int id, Card card)
        {

            ServiceResponse serviceResponse = new();

            // instance of card
            Card Card = new Card()
            {
                CardId = id,
                CardName = card.CardName,
                Colours = card.Colours,
                Artist = card.Artist,
                ArtistId = card.ArtistId,
                ArtistName = card.ArtistName,
                ColorId = card.ColorId,
                Colors = card.Colors
            };
            // flags that the object has changed
            _context.Entry(Card).State = EntityState.Modified;

            try
            {
                //update card where the card state = the {id}
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if it fails use ServiceResponse to alert
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("An error occurred updating the record");
                return serviceResponse;
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            return serviceResponse;
        }

        public async Task<ServiceResponse> AddCard(Card card)
        {
            ServiceResponse serviceResponse = new();

            // instance of card
            Card Card = new Card()
            {
                CardName = card.CardName,
                Colours = card.Colours,
                Artist = card.Artist,
                ArtistId = card.ArtistId,
                ArtistName = card.ArtistName,
                ColorId = card.ColorId,
                Colors = card.Colors
            };
            try
            {
                _context.Cards.Add(card);
                await _context.SaveChangesAsync();
            } 
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("There was an error adding the Card.");
                serviceResponse.Messages.Add(ex.Message);
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = Card.CardId;
            return serviceResponse;
        }

        public async Task<ServiceResponse> DeleteCard(int id)
        {
            ServiceResponse response = new();
            //find the card
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("CArd cannot be deleted because it does not exist.");
                return response;
            }

            try
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();

            }
            catch
            {
                response.Status = ServiceResponse.ServiceStatus.Error;
                response.Messages.Add("Error encountered while deleting the Card");
                return response;
            }

            response.Status = ServiceResponse.ServiceStatus.Deleted;

            return response;
        }
    }
}

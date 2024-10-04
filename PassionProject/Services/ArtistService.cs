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
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;

        public ArtistService(ApplicationDbContext context)
        {
            // injection of DB _context
            _context = context;
        }

        public async Task<IEnumerable<Artist>> ListArtists()
        {
            // get all artists in db
            return await _context.Artists.ToListAsync();
        }

        public async Task<Artist> FindArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                return null;
            }

            return artist;
        }

        public async Task<ServiceResponse> UpdateArtist(int id, Artist artist)
        {

            ServiceResponse serviceResponse = new();

            // instance of artist
            Artist Artist = new Artist()
            {
                ArtistId = id,
                ArtistName = artist.ArtistName
            };
            // flags that the object has changed
            _context.Entry(Artist).State = EntityState.Modified;

            try
            {
                //update artist where the artist state = the {id}
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

        public async Task<ServiceResponse> AddArtist(Artist artist)
        {
            ServiceResponse serviceResponse = new();

            // instance of artist
            Artist Artist = new Artist()
            {
                ArtistId = artist.ArtistId,
                ArtistName = artist.ArtistName
            };
            try
            {
                _context.Artists.Add(artist);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("There was an error adding the Artist.");
                serviceResponse.Messages.Add(ex.Message);
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = Artist.ArtistId;
            return serviceResponse;
        }

        public async Task<ServiceResponse> DeleteArtist(int id)
        {
            ServiceResponse response = new();
            //find the card
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Artist cannot be deleted because it does not exist.");
                return response;
            }

            try
            {
                _context.Artists.Remove(artist);
                await _context.SaveChangesAsync();

            }
            catch
            {
                response.Status = ServiceResponse.ServiceStatus.Error;
                response.Messages.Add("Error encountered while deleting the Artist");
                return response;
            }

            response.Status = ServiceResponse.ServiceStatus.Deleted;

            return response;
        }
    }
}

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
    public class ColorService : IColorService
    {
        private readonly ApplicationDbContext _context;

        public ColorService(ApplicationDbContext context)
        {
            // injection of DB _context
            _context = context;
        }

        public async Task<IEnumerable<ColorDto>> ListColors()
        {
            //list the colors from all the rows of the table
            List<Color> Colors = await _context.Colors.Include(c => c.Cards).ToListAsync();
            //new colorDto
            List<ColorDto> ColorDtos = new List<ColorDto>();
            foreach (Color Color in Colors)
            {
                //put the information from the db into a package
                ColorDtos.Add(new ColorDto()
                {
                    ColorId = Color.ColorId,
                    ColorName = Color.ColorName,
                    CardCount = "This color has " + Color.Cards.Count + " cards."
                });
            }
            return ColorDtos;
        }

        public async Task<ColorDto> FindColor(int id)
        {
            var color = await _context.Colors.FirstOrDefaultAsync(c => c.ColorId == id);

            if (color == null)
            {
                return null;
            }

            //instance of colorDto
            ColorDto ColorDto = new ColorDto()
            {
                ColorId = color.ColorId,
                ColorName = color.ColorName,
                CardCount = "This color has " + color.Cards.Count + " cards."
            };

            return ColorDto;
        }

        public async Task<ServiceResponse> UpdateColor(int id, Color color)
        {

            ServiceResponse serviceResponse = new();

            // instance of color
            Color Color = new Color()
            {
                ColorId = id,
                ColorName = color.ColorName,
                Cards = color.Cards
            };
            // flags that the object has changed
            _context.Entry(Color).State = EntityState.Modified;

            try
            {
                //update card where the color state = the {id}
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

        public async Task<ServiceResponse> AddColor(Color color)
        {
            ServiceResponse serviceResponse = new();

            // instance of color
            Color Color = new Color()
            {
                ColorName = color.ColorName,
                Cards = color.Cards
            };
            try
            {
                _context.Colors.Add(Color);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("There was an error adding the Color.");
                serviceResponse.Messages.Add(ex.Message);
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = Color.ColorId;
            return serviceResponse;
        }

        public async Task<ServiceResponse> DeleteColor(int id)
        {
            ServiceResponse response = new();
            //find the color
            var color = await _context.Colors.FindAsync(id);
            if (color == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Color cannot be deleted because it does not exist.");
                return response;
            }

            try
            {
                _context.Colors.Remove(color);
                await _context.SaveChangesAsync();

            }
            catch
            {
                response.Status = ServiceResponse.ServiceStatus.Error;
                response.Messages.Add("Error encountered while deleting the Color");
                return response;
            }

            response.Status = ServiceResponse.ServiceStatus.Deleted;

            return response;
        }
    }
}

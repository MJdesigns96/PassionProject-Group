using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassionProject;
using PassionProject.Data;
using PassionProject.Models;
using PassionProject.Interfaces;

namespace PassionProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        /// <summary>
        /// Returns a list of Artists
        /// </summary>
        /// <returns>
        /// 200 Okay
        /// [{artist1}, {artist2}, ...]
        /// </returns>
        /// <example>
        /// GET: api/cards/List -> [{artist1}, {artist2}, ...]
        /// </example>
        [HttpGet(template: "List")]
        public async Task<ActionResult<IEnumerable<Artist>>> ListArtists()
        {
            // use service to get an empty list of object artist
            IEnumerable<Artist> artists = await _artistService.ListArtists();
            // return 200 with cards
            return Ok(artists);
        }

        /// <summary>
        /// Returns a single Artist that matches the {id}
        /// </summary>
        /// <param name="id">The Artist id</param>
        /// <returns>
        /// 200 ok
        /// {artist}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// GET: api/Artists/Find/1 -> {artist}
        /// </example>
        [HttpGet(template: "Find/{id}")]
        public async Task<ActionResult<Artist>> FindArtist(int id)
        {
            var artist = await _artistService.FindArtist(id);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        /// <summary>
        /// Update an Artist
        /// </summary>
        /// <param name="id">ID of the artist</param>
        /// <param name="artist">the required information of the artist(ArtistName)</param>
        /// <returns>
        /// ServiceResponse code:
        /// 400 Bad Request
        /// or
        /// 404 Not Found
        /// or
        /// 204 No Content
        /// </returns>
        /// <example>
        /// PUT: api/Artists/Update/5
        /// Request Headers: Content-Type: application/json
        /// Request Body {artist}
        /// ->
        /// Response Code: 
        /// </example>
        [HttpPut(template: "Update/{id}")]
        public async Task<ActionResult> UpdateArtist(int id, Artist artist)
        {
            if (id != artist.ArtistId)
            {
                return BadRequest();
            }

            //use ServiceResponse to respond when UpdateCard() goes through or not
            ServiceResponse response = await _artistService.UpdateArtist(id, artist);

            //response messages from ServiceResponse class
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            //response code when code has been updated
            return NoContent();
        }

        /// <summary>
        /// Add an Artist to the db
        /// </summary>
        /// <param name="artist">required information of the artist(ArtistId, ArtistName)</param>
        /// <returns>
        /// ServiceResponse code:
        /// 201 Created
        /// Location: api/Artists/Find/{CardId}
        /// {artist}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// Post: api/Artist/Add
        /// Request Headers: Content-type: applicaton/json
        /// Request Body: {artist}
        /// ->
        /// Response Code: 201 Created
        /// Request Headers: Location: api/Artists/Find/{ArtistId}
        /// </example>
        [HttpPost(template: "Add")]
        public async Task<ActionResult<Card>> AddArtist(Artist artist)
        {
            //use ServiceResponse to respond when AddArtist() goes through or not
            ServiceResponse response = await _artistService.AddArtist(artist);

            //response messages from ServiceResponse class
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            //response code when code has been added
            return Created($"api/Artists/Find/{response.CreatedId}", artist);
        }

        /// <summary>
        /// delete an artist from the db
        /// </summary>
        /// <param name="id">id of the artist to be removed</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// DELETE: api/Artists/Delete/2
        /// -> 
        /// Response Code: 204 No Content
        /// </example>
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            //use ServiceResponse to respond when DeleteCard() goes through or not
            ServiceResponse response = await _artistService.DeleteArtist(id);

            //response messages from ServiceResponse class
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound();
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            //response code when code has been deleted
            return NoContent();
        }
    }
    
}

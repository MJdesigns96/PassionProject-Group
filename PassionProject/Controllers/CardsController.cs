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
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Returns a list of Cards
        /// </summary>
        /// <returns>
        /// 200 Okay
        /// [{card1}, {card2}, ...]
        /// </returns>
        /// <example>
        /// GET: api/cards/List -> [{card1}, {card2}, ...]
        /// </example>
        [HttpGet(template: "List")]
        public async Task<ActionResult<IEnumerable<Card>>> ListCards()
        {
            // use service to get an empty list of object card
            IEnumerable<Card> cards = await _cardService.ListCards();
            // return 200 with cards
            return Ok(cards);
        }

        /// <summary>
        /// Returns a single Card that matches the {id}
        /// </summary>
        /// <param name="id">The Card id</param>
        /// <returns>
        /// 200 ok
        /// {card}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// GET: api/Cards/Find/1 -> {card}
        /// </example>
        [HttpGet(template:"Find/{id}")]
        public async Task<ActionResult<Card>> FindCard(int id)
        {
            var card = await _cardService.FindCard(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        /// <summary>
        /// Update a Card
        /// </summary>
        /// <param name="id">ID of the card</param>
        /// <param name="card">the required information of the card(CardName, Colours, Artist, ArtistId, ArtistName, ColorId, Colors)</param>
        /// <returns>
        /// ServiceResponse code:
        /// 400 Bad Request
        /// or
        /// 404 Not Found
        /// or
        /// 204 No Content
        /// </returns>
        /// <example>
        /// PUT: api/Cards/Update/5
        /// Request Headers: Content-Type: application/json
        /// Request Body {card}
        /// ->
        /// Response Code: 
        /// </example>
        [HttpPut(template:"Update/{id}")]
        public async Task<ActionResult> UpdateCard(int id, Card card)
        {
            if (id != card.CardId)
            {
                return BadRequest();
            }

            //use ServiceResponse to respond when UpdateCard() goes through or not
            ServiceResponse response = await _cardService.UpdateCard(id, card);

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
        /// Add a card to the db
        /// </summary>
        /// <param name="card">required information of the card(CardName, Colours, Artist, ArtistId, ArtistName, ColorId, Colors)</param>
        /// <returns>
        /// ServiceResponse code:
        /// 201 Created
        /// Location: api/Cards/Find/{CardId}
        /// {card}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// Post: api/Card/Add
        /// Request Headers: Content-type: applicaton/json
        /// Request Body: {card}
        /// ->
        /// Response Code: 201 Created
        /// Request Headers: Location: api/Cards/Find/{CardId}
        /// </example>
        [HttpPost(template: "Add")]
        public async Task<ActionResult<Card>> AddCard(Card card)
        {
            //use ServiceResponse to respond when AddCard() goes through or not
            ServiceResponse response = await _cardService.AddCard(card);

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
            return Created($"api/Cards/Find/{response.CreatedId}", card);
        }

        /// <summary>
        /// delete a card from the db
        /// </summary>
        /// <param name="id">id of the card to be removed</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// DELETE: api/Cards/Delete/2
        /// -> 
        /// Response Code: 204 No Content
        /// </example>
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteCard(int id)
        {
            //use ServiceResponse to respond when DeleteCard() goes through or not
            ServiceResponse response = await _cardService.DeleteCard(id);

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

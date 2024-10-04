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
using PassionProject.Services;
using PassionProject.Data.Migrations;

namespace PassionProject.Controllers
{
    //entity framework syntax
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        /// <summary>
        /// returns the different colors in the colors table
        /// </summary>
        /// <returns>
        /// 200 Okay
        /// { color1 }, { color2 }, { color3 }
        /// </returns>
        /// <example>
        /// GET: api/color/ListColors ->
        /// { colorName: "white", "This color has {Color.Cards.Count} cards." },
        /// { colorName: "red", "This color has {Color.Cards.Count} cards." }
        /// </example>
        [HttpGet(template:"ListColors")]
        public async Task<ActionResult<IEnumerable<Color>>> ListColors()
        {
            // use service to get an empty list of object color
            IEnumerable<ColorDto> colors = await _colorService.ListColors();
            // return 200 with colors
            return Ok(colors);
        }

        /// <summary>
        /// Returns a single Color that matches the {id}
        /// </summary>
        /// <param name="id">The Color id</param>
        /// <returns>
        /// 200 ok
        /// {color}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// GET: api/Colors/Find/1 -> {color}
        /// </example>
        [HttpGet(template: "Find/{id}")]
        public async Task<ActionResult<ColorDto>> FindColor(int id)
        {
            var color = await _colorService.FindColor(id);

            if (color == null)
            {
                return NotFound();
            } else
            {
                return Ok(color);
            }
        }

        /// <summary>
        /// Update a Color
        /// </summary>
        /// <param name="id">ID of the color</param>
        /// <param name="color">the required information of the color(ColorName, Cards)</param>
        /// <returns>
        /// ServiceResponse code:
        /// 400 Bad Request
        /// or
        /// 404 Not Found
        /// or
        /// 204 No Content
        /// </returns>
        /// <example>
        /// PUT: api/Colors/Update/5
        /// Request Headers: Content-Type: application/json
        /// Request Body {color}
        /// ->
        /// Response Code: 
        /// </example>
        [HttpPut(template: "Update/{id}")]
        public async Task<ActionResult> UpdateColor(int id, Color color)
        {
            if (id != color.ColorId)
            {
                return BadRequest();
            }

            //use ServiceResponse to respond when UpdateColor() goes through or not
            ServiceResponse response = await _colorService.UpdateColor(id, color);

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
        /// Add a color to the db
        /// </summary>
        /// <param name="Color">required information of the color(ColorName, Cards)</param>
        /// <returns>
        /// ServiceResponse code:
        /// 201 Created
        /// Location: api/Color/Find/{ColorId}
        /// {color}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// Post: api/Color/Add
        /// Request Headers: Content-type: applicaton/json
        /// Request Body: {color}
        /// ->
        /// Response Code: 201 Created
        /// Request Headers: Location: api/Colors/Find/{ColorId}
        /// </example>
        [HttpPost(template: "Add")]
        public async Task<ActionResult<Color>> AddColor(Color color)
        {
            //use ServiceResponse to respond when AddColor() goes through or not
            ServiceResponse response = await _colorService.AddColor(color);

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
            return Created($"api/Colors/Find/{response.CreatedId}", color);
        }

        /// <summary>
        /// delete a color from the db
        /// </summary>
        /// <param name="id">id of the color to be removed</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// DELETE: api/Colors/Delete/2
        /// -> 
        /// Response Code: 204 No Content
        /// </example>
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteColor(int id)
        {
            //use ServiceResponse to respond when DeleteColor() goes through or not
            ServiceResponse response = await _colorService.DeleteColor(id);

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

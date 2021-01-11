using Microsoft.AspNetCore.Mvc;
using MyPlaces.Model;
using MyPlaces.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlaces.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaceController : ControllerBase
    {

        private readonly IPlaceRepository _placeRepository;
        public PlaceController(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPLaces()
        {
            IEnumerable<Place> places = await _placeRepository.GetAllPlaces();

            if (places != null)
            {
                return Ok(places);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id}/place")]
        public async Task<IActionResult> GetPlaceDetails(int id)
        {
            try
            {
                return await GetPlaceByID(id);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private async Task<IActionResult> GetPlaceByID(int id)
        {
            var place = await _placeRepository.GetPlaceById(id);

            if (place == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(place);
            }
        }

        [HttpPost]
        public IActionResult CreatePlace([FromBody] Place place)
        {
            try
            {
                if (place == null)
                {
                    return BadRequest("Owner cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is invalid");
                }

                _placeRepository.Create(place);
                _placeRepository.Save();

                return CreatedAtAction("GetPlaceDetails", new { id = place.Id }, place);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}

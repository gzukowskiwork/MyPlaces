using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Entities.Model;
using Entities.Model.DTO;
using Entities.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyPlaces.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaceController : ControllerBase
    {

        private readonly IPlaceRepository _placeRepository;
        private readonly IMapper _mapper;
        public PlaceController(IPlaceRepository placeRepository, IMapper mapper)
        {
            _placeRepository = placeRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllPLaces()
        {
            IEnumerable<Place> places = await _placeRepository.GetAllPlaces();

            if (places != null)
            {
                //var placeResult = _mapper.Map<PlaceWithoutId>(places);
                return Ok(places);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPlaceDetails(int id)
        {
            try
            {
                return await getPlaceByID(id);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private async Task<IActionResult> getPlaceByID(int id)
        {
            var place = await _placeRepository.GetPlaceById(id);

            if (place == null)
            {
                return NotFound();
            }
            else
            {
                var placeResult = _mapper.Map<PlaceWithoutId>(place);
                return Ok(placeResult);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlace([FromBody] PlaceWithoutId place)
        {
            try
            {
                return await createPlace(place);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private async Task<IActionResult> createPlace(PlaceWithoutId place)
        {
            if (place == null)
            {
                return BadRequest("Place cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is invalid");
            }

            var placeEntity = _mapper.Map<Place>(place);

            _placeRepository.Create(placeEntity);
            await _placeRepository.Save();

            return CreatedAtAction("GetPlaceDetails", new { id = placeEntity.Id }, place);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(int id, [FromBody] PlaceWithoutId place)
        {
            try
            {
                return await updatePlace(id, place);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private async Task<IActionResult> updatePlace(int id, PlaceWithoutId place)
        {
            if (place == null)
            {
                return BadRequest("Place cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is invalid");
            }

            Place placeToUpdate = await _placeRepository.GetPlaceById(id);

            if (placeToUpdate == null)
            {
                return NotFound();
            }

            _mapper.Map(place, placeToUpdate);

            _placeRepository.Update(placeToUpdate);
            await _placeRepository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await delete(id);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private async Task<IActionResult> delete(int id)
        {
            var place = await _placeRepository.GetPlaceById(id);
            if (place == null)
            {
                return NotFound();
            }

            _placeRepository.Delete(place);
            await _placeRepository.Save();

            return NoContent();
        }
    }
}

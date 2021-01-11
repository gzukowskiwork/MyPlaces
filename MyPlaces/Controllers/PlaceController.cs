using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyPlaces.Model;
using MyPlaces.Model.DTO;
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
        private readonly IMapper _mapper;
        public PlaceController(IPlaceRepository placeRepository, IMapper mapper)
        {
            _placeRepository = placeRepository;
            _mapper = mapper;
        }

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
        [Route("{id}/place")]
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
        public IActionResult CreatePlace([FromBody] PlaceWithoutId place)
        {
            try
            {
                return createPlace(place);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private IActionResult createPlace(PlaceWithoutId place)
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
            _placeRepository.Save();

            return CreatedAtAction("GetPlaceDetails", new { id = placeEntity.Id }, place);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlace(int id, [FromBody] PlaceWithoutId place)
        {
            try
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
                _placeRepository.Save();

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }


        }
    }
}

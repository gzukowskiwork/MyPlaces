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

            return Ok(places);
        }
    }
}

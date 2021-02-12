using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Model.Repository
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly ApplicationContext _applicationContext;

        public PlaceRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IEnumerable<Place>> GetAllPlaces()
        {
            return await _applicationContext.Places.ToListAsync();
        }

        public async Task<Place> GetPlaceById(int id)
        {
            return await _applicationContext.Places.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public void Create(Place place)
        {
            _applicationContext.Places.Add(place);
        }

        public void Update(Place place)
        {
            _applicationContext.Places.Update(place);
        }

        public void Delete(Place place)
        {
            _applicationContext.Places.Remove(place);
        }

        public Task Save()
        {
            return _applicationContext.SaveChangesAsync();
        }
    }
}

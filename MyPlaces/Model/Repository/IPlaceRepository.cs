using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlaces.Model.Repository
{
    public interface IPlaceRepository
    {
        Task<IEnumerable<Place>> GetAllPlaces();
        Task<Place> GetPlaceById(int id);
        void Create(Place place);
        void Update(Place place);
        void Delete(Place place);
        Task Save();
    }
}

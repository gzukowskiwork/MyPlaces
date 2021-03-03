using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Model.Repository
{
    public interface IPlaceRepository
    {
        Task<IEnumerable<Place>> GetAllPlaces();
        Task<PagedList<Place>> GetAllPlacesPagination(PlaceParmeters placeParmeters);
        Task<Place> GetPlaceById(int id);
        void Create(Place place);
        void Update(Place place);
        void Delete(Place place);
        Task Save();
    }
}

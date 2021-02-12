using AutoMapper;
using Entities.Model;
using Entities.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlaces
{
    public class Profiles: Profile
    {
        public Profiles()
        {
            CreateMap<Place, PlaceWithoutId>();
            CreateMap<PlaceWithoutId, Place>();
        }
    }
}

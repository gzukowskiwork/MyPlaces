using AutoMapper;
using MyPlaces.Model;
using MyPlaces.Model.DTO;
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

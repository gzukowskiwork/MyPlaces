﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlaces.Model.DTO
{
    public class PlaceWithoutId
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Longtidude { get; set; }
        [Required]
        public double Latidude { get; set; }
        public string PathToImage { get; set; }
    }
}

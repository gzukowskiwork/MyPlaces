using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class ApplicationUser: IdentityUser
    {
        [ForeignKey(nameof(Place))]
        public int PlaceId { get; set; }

        public ICollection<Place> Places { get; set; }
    }
}

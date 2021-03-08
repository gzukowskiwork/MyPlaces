using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities.Model;

namespace Entities.Configuration
{
    public class PlaceConfiguration: IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.HasData(
                new Place()
                {
                    Id = 1,
                    Name = "Koloseum",
                    Description = "Spodziewałem się czegoś więcej",
                    Latitude = 41.89024,
                    Longitude = 12.49237,
                    PathToImage = ""
                },
                new Place()
                {
                    Id = 2,
                    Name = "Torre di Chia",
                    Description = "Urokliwe miejsce",
                    Latitude = 38.89533,
                    Longitude = 8.88511,
                    PathToImage = ""
                });
        }
    }
}
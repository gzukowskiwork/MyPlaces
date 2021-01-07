using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPlaces.Model;

namespace MyPlaces.Configuration
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
                    Latidude = 41.89024,
                    Longtidude = 12.49237,
                    PathToImage = ""
                },
                new Place()
                {
                    Id = 2,
                    Name = "Torre di Chia",
                    Description = "Urokliwe miejsce",
                    Latidude = 38.89533,
                    Longtidude = 8.88511,
                    PathToImage = ""
                });
        }
    }
}
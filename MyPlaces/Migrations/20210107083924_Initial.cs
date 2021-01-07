using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPlaces.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longtidude = table.Column<double>(type: "float", nullable: false),
                    Latidude = table.Column<double>(type: "float", nullable: false),
                    PathToImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "Description", "Latidude", "Longtidude", "Name", "PathToImage" },
                values: new object[] { 1, "Spodziewałem się czegoś więcej", 41.890239999999999, 12.492369999999999, "Koloseum", "" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "Description", "Latidude", "Longtidude", "Name", "PathToImage" },
                values: new object[] { 2, "Urokliwe miejsce", 38.895330000000001, 8.8851099999999992, "Torre di Chia", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}

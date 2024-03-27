using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HomeAgency.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Sqft = table.Column<int>(type: "int", nullable: false),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_villas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "villas",
                columns: new[] { "Id", "CreateDate", "Description", "ImageUrl", "Name", "Occupancy", "Price", "Sqft", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, "This is a great villa", null, "LasVegas Mantion", 0, 99.900000000000006, 0, null },
                    { 2, null, "This is a great villa", null, "NewYork Mantion", 0, 99.900000000000006, 0, null },
                    { 3, null, "This is a great villa", null, "LasVegas Mantion", 0, 99.900000000000006, 0, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "villas");
        }
    }
}

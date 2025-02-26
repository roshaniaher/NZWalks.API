using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Seeddatadiff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("33c8a633-669b-4dde-8ea7-b0d5bbec0b5e"), "Medium" },
                    { new Guid("c0614621-162c-4bdf-92c4-423ccc2a4185"), "Easy" },
                    { new Guid("f32cbcd1-0e71-40a0-8a6e-6135c6e9c3e3"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2a203c87-e96b-453b-838c-2e3ee42c7e01"), "A", "Auckland", "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/auckland/auckland-forest.jpg" },
                    { new Guid("6022b045-66b9-406e-9633-3511e2f0d072"), "T", "Taranaki", "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/taranaki/taranaki-forest.jpg" },
                    { new Guid("6edeaa5f-d22c-420a-82f6-5ba5321c5a94"), "B", "Bay of Plenty", "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/bay-of-plenty/bay-of-plenty-forest.jpg" },
                    { new Guid("b2100453-885f-41bf-b120-1b1090dcd345"), "N", "Northland", "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/northland/northland-forest.jpg" },
                    { new Guid("df538c82-d453-4b46-bcc5-db4238ff7073"), "W", "Waikato", "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/waikato/waikato-forest.jpg" },
                    { new Guid("f745e180-6a3d-4bf0-a810-6485871bc07a"), "G", "Gisborne", "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/gisborne/gisborne-forest.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("33c8a633-669b-4dde-8ea7-b0d5bbec0b5e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c0614621-162c-4bdf-92c4-423ccc2a4185"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f32cbcd1-0e71-40a0-8a6e-6135c6e9c3e3"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2a203c87-e96b-453b-838c-2e3ee42c7e01"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6022b045-66b9-406e-9633-3511e2f0d072"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6edeaa5f-d22c-420a-82f6-5ba5321c5a94"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b2100453-885f-41bf-b120-1b1090dcd345"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("df538c82-d453-4b46-bcc5-db4238ff7073"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f745e180-6a3d-4bf0-a810-6485871bc07a"));
        }
    }
}

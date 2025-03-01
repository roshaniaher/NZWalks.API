using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data for difficulties
            //Easy medium hard

            var Difficulties = new Difficulty[]
            {
                new Difficulty {Id = Guid.Parse("c0614621-162c-4bdf-92c4-423ccc2a4185"), Name = "Easy"},
                new Difficulty {Id = Guid.Parse("33c8a633-669b-4dde-8ea7-b0d5bbec0b5e"), Name = "Medium"},
                new Difficulty {Id = Guid.Parse("f32cbcd1-0e71-40a0-8a6e-6135c6e9c3e3"), Name = "Hard"}
            };

            modelBuilder.Entity<Difficulty>().HasData(Difficulties);

            //seeed data for regions
            var Regions = new Region[] {
                new Region {Id = Guid.Parse("b2100453-885f-41bf-b120-1b1090dcd345"), Code = "N", Name = "Northland", RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/northland/northland-forest.jpg"},
                new Region {Id = Guid.Parse("2a203c87-e96b-453b-838c-2e3ee42c7e01"), Code = "A", Name = "Auckland", RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/auckland/auckland-forest.jpg"},
                new Region {Id = Guid.Parse("df538c82-d453-4b46-bcc5-db4238ff7073"), Code = "W", Name = "Waikato", RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/waikato/waikato-forest.jpg"},
                new Region {Id = Guid.Parse("6edeaa5f-d22c-420a-82f6-5ba5321c5a94"), Code = "B", Name = "Bay of Plenty", RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/bay-of-plenty/bay-of-plenty-forest.jpg"},
                new Region {Id = Guid.Parse("6022b045-66b9-406e-9633-3511e2f0d072"), Code = "T", Name = "Taranaki", RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/taranaki/taranaki-forest.jpg"},
                new Region {Id = Guid.Parse("f745e180-6a3d-4bf0-a810-6485871bc07a"), Code = "G", Name = "Gisborne", RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/gisborne/gisborne-forest.jpg"},
            };
             modelBuilder.Entity<Region>().HasData(Regions);
        }

    }
}

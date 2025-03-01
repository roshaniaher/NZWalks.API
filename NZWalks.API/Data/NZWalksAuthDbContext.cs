using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var readerRoleId = "b1b3b3b3-3b3b-3b3b-3b3b-3b3b3b3b3b3b";
            var writerRoleId = "a1a1a1a1-1a1a-1a1a-1a1a-1a1a1a1a1a1a";
            var roles = new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Id = readerRoleId,
                        ConcurrencyStamp = readerRoleId,
                        Name = "Reader",
                        NormalizedName = "READER"
                    },
                    new IdentityRole
                    {
                        Id = writerRoleId,
                        ConcurrencyStamp = writerRoleId,
                        Name = "Writer",
                        NormalizedName = "WRITER"
                    }
                };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

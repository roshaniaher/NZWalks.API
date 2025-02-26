using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var ExistingWalk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);
            if (ExistingWalk == null)
            {
                return null;
            }

            ExistingWalk.Name = walk.Name;
            ExistingWalk.Description = walk.Description;
            ExistingWalk.LengthInKm = walk.LengthInKm;
            ExistingWalk.DifficultyId = walk.DifficultyId;
            ExistingWalk.RegionId = walk.RegionId;
            ExistingWalk.WalkImageUrl = walk.WalkImageUrl;

            await dbContext.SaveChangesAsync();
            return ExistingWalk;


        }
        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);
            if (walk == null)
            {
               return null;

            }
            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }
    }
}

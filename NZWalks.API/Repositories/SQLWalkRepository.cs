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

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool? isAscending = true, int pageNumber=1, int pageSize=1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                switch (filterOn.ToLower())
                {
                    case "name":
                        walks = walks.Where(w => w.Name.Contains(filterQuery));
                        break;
                    case "description":
                        walks = walks.Where(w => w.Description.Contains(filterQuery));
                        break;
                    case "lengthinkm":
                        walks = walks.Where(w => w.LengthInKm.ToString().Contains(filterQuery));
                        break;
                    case "difficulty":
                        walks = walks.Where(w => w.Difficulty.Name.Contains(filterQuery));
                        break;
                    case "region":
                        walks = walks.Where(w => w.Region.Name.Contains(filterQuery));
                        break;
                    default:
                        break;
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", System.StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending == true ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w => w.Name);
                }
                else if (sortBy.Equals("LengthInKm", System.StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending == true ? walks.OrderBy(w => w.LengthInKm) : walks.OrderByDescending(w => w.LengthInKm);
                }
                else if (sortBy.Equals("Difficulty", System.StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending == true ? walks.OrderBy(w => w.Difficulty.Name) : walks.OrderByDescending(w => w.Difficulty.Name);
                }
                else if (sortBy.Equals("Region", System.StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending == true ? walks.OrderBy(w => w.Region.Name) : walks.OrderByDescending(w => w.Region.Name);
                }
            }

            //Pagination
            var skipresults = (pageNumber-1) * pageSize;



            return await walks.Skip(skipresults).Take(pageSize).ToListAsync();
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

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFolder;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        [ValidateModelAttribute]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);


            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //GET Walks
        //GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetWalks()
        {
            var walks = await walkRepository.GetAllAsync();
            return Ok(mapper.Map<List<WalkDto>>(walks));
        }

        //Get Walks by ID
        //GET: /api/walks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Update walk by id
        //PUT: /api/walks/{id}
        [HttpPut("{id}")]
        [ValidateModelAttribute]
        public async Task<IActionResult> UpdateWalk(Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
           
            //Map DTO to Domin Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
            
        }

        //Delete walk by ID
        //DELETE: /api/walks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

    }
}

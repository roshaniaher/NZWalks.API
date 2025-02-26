using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper) 
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
       public async Task<IActionResult> GetAllRegions()
        {
            var regionsDomain = await regionRepository.GetAllRegionsAsync();

            //var regionsDto = new List<RegionDto>();
            //foreach(var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl
            //    });
            //}
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(regionsDto);
        }

        //Get region by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var RegionDomain = await regionRepository.GetByIdAsync(id);

            if (RegionDomain == null)
            {
                return NotFound();
            }
            //Mapping RegionDomain to RegionDto
            //var RegionDto = new RegionDto
            //{
            //    Id = RegionDomain.Id,
            //    Code = RegionDomain.Code,
            //    Name = RegionDomain.Name,
            //    RegionImageUrl = RegionDomain.RegionImageUrl
            //};

            var RegionDto = mapper.Map<RegionDto>(RegionDomain);
            return Ok(RegionDto);
        }

        //Create new region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto AddregionDto)
        {
            var regionDomain = mapper.Map<Region>(AddregionDto);

            regionDomain = await regionRepository.CreateAsync(regionDomain);

            var regionDto = mapper.Map<RegionDto>(regionDomain);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //update region
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto upadateregionrequestDto)
        {
            var regionDomain = mapper.Map<Region>(upadateregionrequestDto);

            regionDomain = await regionRepository.UpdateAsync(id, regionDomain);

            //convert Domain model to dto
            

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async  Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.DeleteAsync(id);

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
    }
}

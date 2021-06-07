using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;
using WebApplication1.Models.Energy;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Energy
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingApiController : ControllerBase
    {
        private IMapper _mapper;
        private IBuildingRepository _buildingRepository;

        public BuildingApiController(IMapper mapper, IBuildingRepository buildingRepository)
        {
            _mapper = mapper;
            _buildingRepository = buildingRepository;
        }

        // GET: api/<BuildingApiController>
        [HttpGet]
        public ActionResult<IEnumerable<BuildingViewModel>> Get()
        {
            return _buildingRepository
                .GetAll()
                .Select(x => _mapper.Map<BuildingViewModel>(x))
                .ToList();
        }

        // GET api/<BuildingApiController>/5
        [HttpGet("{id}")]
        public ActionResult<BuildingViewModel> Get(long id)
        {
            var isContains = _buildingRepository
                .GetAll()
                .Any(x => x.Id == id);

            if (isContains)
            {
                var model = _buildingRepository.Get(id);
                return _mapper.Map<BuildingViewModel>(model);
            }

            return null;
        }

        [HttpPost]
        public ActionResult<IEnumerable<BuildingViewModel>> Post([FromBody] DistrictName districtName)
        {
            return _buildingRepository
                .GetAll()
                .Where(x => x.District.Name == districtName)
                .Select(x => _mapper.Map<BuildingViewModel>(x))
                .ToList();
        }

        // PUT api/<BuildingApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BuildingApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;
using WebApplication1.Models.Energy;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Energy
{
    public class BuildingController : Controller
    {
        private IMapper _mapper;
        private IUserService _userService;
        private IBuildingRepository _buildingRepository;

        public BuildingController(IMapper mapper, IUserService userService, IBuildingRepository buildingRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _buildingRepository = buildingRepository;
        }
        public IActionResult Index()
        {
            var viewModels = _buildingRepository
               .GetAll()
               .Select(x => _mapper.Map<BuildingViewModel>(x))
               .ToList();

            return View(viewModels);
        }

        // GET: Building/AddBuilding
        [HttpGet]
        public IActionResult AddBuilding()
        {
            return View();
        }

        // P0ST: Building/AddBuilding
        [HttpPost]
        public IActionResult AddBuilding(BuildingViewModel viewModel)
        {

            var model = _mapper.Map<Building>(viewModel);

            _buildingRepository.Save(model);

            return View();
        }
    }
}

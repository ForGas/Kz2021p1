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
        private IUserService _userService { get; set; }
        private IMapper _mapper { get; set; }
        private IBuildingRepository _buildingRepository { get; set; }

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

        // перенос В Админ
        // GET: Building/AddBuilding
        [HttpGet]
        public IActionResult AddBuilding()
        {
            return View();
        }
        /// <summary>
        /// Добавление дома
        /// Адрес
        /// Рандомная Генерация счета по электроэнергий
        /// </summary>
        /// <returns></returns>

        // P0ST: Building/AddBuilding
        [HttpPost]
        public IActionResult AddBuilding(BuildingViewModel viewModel)
        {
            //var modelB = new Building
            //{
            //    Adress = new Adress { HouseNumber = 1, FloorCount = 2, Street = "gg" },
            //    ElectricBill = new ElectricBill { Consumption = 0, TotalDebt = 0 },
            //    District = new District { Name = DistrictName.BlueDistrict },
            //    TotalArea = 30,
            //};

            //var modelVew = _mapper.Map<BuildingViewModel>(modelB);

            var model = _mapper.Map<Building>(viewModel);

            _buildingRepository.Save(model);

            return View();
        }
    }
}

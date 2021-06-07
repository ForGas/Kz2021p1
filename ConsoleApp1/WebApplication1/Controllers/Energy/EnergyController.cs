using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Energy
{
    public class EnergyController : Controller
    {
        private IMapper _mapper;
        private IUserService _userService;

        public EnergyController(IMapper mapper, IUserService userService, IBuildingRepository buildingRepository)
        {
            _mapper = mapper;
            _userService = userService;
        }

        // GET: Energy/Index
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            ViewData["userName"] = _userService.GetUser().Name;

            return View();
        }

        // GET: Energy/Tariff
        [HttpGet]
        public IActionResult Tariff()
        {
            return View();
        }

        // GET: Energy/Services
        [HttpGet]
        public IActionResult Services()
        {
            return View();
        }

        // GET: Energy/Contacts
        [HttpGet]
        public IActionResult Contacts()
        {
            return View();
        }
    }
}

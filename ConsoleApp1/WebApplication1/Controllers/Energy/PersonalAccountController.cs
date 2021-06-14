using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models.Energy;
using WebApplication1.Presentation.Energy;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Energy
{
    public class PersonalAccountController : Controller
    {
        private IMapper _mapper;
        private IUserService _userService;
        private ICitizenRepository _citizenRepository;
        private IBuildingRepository _buildingRepository;
        private IPersonalAccountRepository _accountRepository;
        private IPersonalAccountPresentation _accountPresentation;
        private IElectricityMeterRepository _meterRepository;
        private ITariffRepository _tariffRepository;

        public PersonalAccountController(IPersonalAccountRepository accountRepository,
            IMapper mapper, IUserService userService,
            IBuildingRepository buildingRepository, ICitizenRepository citizenRepository,
            IPersonalAccountPresentation accountPresentation, IElectricityMeterRepository meterRepository,
            ITariffRepository tariffRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _citizenRepository = citizenRepository;
            _accountRepository = accountRepository;
            _buildingRepository = buildingRepository;
            _accountPresentation = accountPresentation;
            _meterRepository = meterRepository;
            _tariffRepository = tariffRepository;
        }

        // GET: PersonalAccountController
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var citizen = _userService.GetUser();

            var accounts = _accountPresentation.GetAllOwnAccounts();

            if (accounts == null || accounts.Count() == 0)
            {
                return RedirectToAction("Registration", "PersonalAccount");
            }

            accounts.ForEach(account => _accountPresentation.CalculateDebt(account));

            var viewModel = _accountPresentation.GetViewModels(accounts);

            return View(viewModel);
        }

        // GET: PersonalAccountController/Registration
        [HttpGet]
        [Authorize]
        public IActionResult Registration()
        {
            var viewModel = _accountPresentation.CreateBaseViewModel();

            return View(viewModel);
        }


        // POST: PersonalAccountController/Registration
        [HttpPost]
        public IActionResult Registration(PersonalAccountViewModel viewModel)
        {
            _accountPresentation.InizializeAccount(viewModel);

            var building = _accountPresentation.GetBuildingByAddress(viewModel.Address);

            viewModel.Consumption = _accountPresentation.GetCalculateConsumption(building);

            _accountPresentation.RegisterAccount(viewModel, building);

            return RedirectToAction("Details", "PersonalAccount");
        }

        // GET: PersonalAccountController/Details/
        [Authorize]
        public IActionResult Details()
        {
            var viewModel = _accountPresentation.GetNewAccountDetails();

            return View(viewModel);
        }

        // GET: PersonalAccountController/Delete/5
        public IActionResult Delete(string accountNumber)
        {
            var account = _accountRepository.GetByNumber(accountNumber);

            var viewModel = _mapper.Map<PersonalAccountViewModel>(account);

            return View(viewModel);
        }

        // POST: PersonalAccountController/Delete/5
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var account = _accountRepository.Get(id);

            if (account == null)
            {
                return Json(false);
            }

            var citizen = account.Citizen;
            var building = account.Meter.ElectricBill.Building;

            citizen.PersonalAccounts.Remove(account);
            building.ElectricBill.ElectricityMeters.Remove(account.Meter);

            _accountRepository.Remove(account);
            _meterRepository.Remove(account.Meter);
            _tariffRepository.DeleteById(account.TariffId);

            _citizenRepository.Save(citizen);
            _buildingRepository.Save(building);

            return Json(true);
        }
    }
}

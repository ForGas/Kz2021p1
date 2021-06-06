using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models.Energy;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Energy
{
    public class PersonalAccountController : Controller
    {
        private IAdressRepository _adressRepository { get; set; }
        private IBuildingRepository _buildingRepository { get; set; }
        private IPersonalAccountRepository _accountRepository { get; set; }
        private IUserService _userService { get; set; }
        private IMapper _mapper { get; set; }        

        public PersonalAccountController(IAdressRepository adressRepository,
            IPersonalAccountRepository accountRepository, IMapper mapper, 
            IUserService userService, IBuildingRepository buildingRepository)
        {
            _adressRepository = adressRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _userService = userService;
            _buildingRepository = buildingRepository;
        }

        // GET: PersonalAccountController
        [HttpGet]
        public IActionResult Index()
        {
            Citizen citizen = _userService.GetUser();
            PersonalAccount account = _accountRepository.GetCitizen(citizen.Id);

            if (account == null)
            {
                return RedirectToAction("Registration", "PersonalAccount");
            }

            return View();
        }

        // GET: PersonalAccountController/Registration
        [HttpGet]
        public IActionResult Registration()
        {
            Citizen citizen = _userService.GetUser();
            if (citizen == null)
            {
                return RedirectToAction("Login", "Citizen");
            }

            var viewModel = new PersonalAccountViewModel
            {
                CitizenId = citizen.Id,
                UserName = citizen.Name,
                DateRegistration = DateTime.Now,
                SerialNumber = GenerateSerialNumber()

            };

            return View(viewModel);
        }


        public string GenerateSerialNumber()
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder(9);

            builder.Append("№");

            for (int i = 0; i < 8; i++)
            {
                builder.Append(random.Next(10));
            }

            return builder.ToString();
        }


        // POST: PersonalAccountController/Registration
        [HttpPost]
        public IActionResult Registration(PersonalAccountViewModel viewModel)
        {

            viewModel.Debt = 0;
            viewModel.Number = GenerateAccountNumber();

            // переписать на get(address)
            var building = _buildingRepository.GetAll().SingleOrDefault(x => x.Adress.Street == viewModel.Address);

            //viewModel.Consumption {viewModel.Address (площадь/n) , viewModel.Person, viewModel.Rate}

            var count = building.ElectricBill.ElectricityMeters.Count();

            viewModel.Consumption = ((building.TotalArea / 50) * 
                (250 * (count != 0 ? (count + 1) : 1))) 
                / 30;


            return View();
        }

        public string GenerateAccountNumber()
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder(8);

            for (int i = 0; i < 7; i++)
            {
                builder.Append(random.Next(10));
            }

            return builder.ToString();
        }

        // GET: PersonalAccountController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonalAccountController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonalAccountController/Delete/5
        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

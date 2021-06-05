using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
        private IPersonalAccountRepository _accountRepository { get; set; }
        private IUserService _userService { get; set; }
        private IMapper _mapper { get; set; }

        public PersonalAccountController(IAdressRepository adressRepository,
            IPersonalAccountRepository accountRepository, IMapper mapper, IUserService userService)
        {
            _adressRepository = adressRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _userService = userService;
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
                UserName = citizen.Name,
                DateRegistration = DateTime.Now,

            };

            return View(viewModel);
        }

        // POST: PersonalAccountController/Registration
        [HttpPost]
        public IActionResult Registration(PersonalAccountViewModel viewModel)
        {
            return View();
        }

        // GET: PersonalAccountController/Details/5
        public ActionResult Details(int id)
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

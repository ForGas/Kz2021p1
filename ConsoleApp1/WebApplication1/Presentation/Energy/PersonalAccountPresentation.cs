using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models.Energy;
using WebApplication1.Services;

namespace WebApplication1.Presentation.Energy
{
    public class PersonalAccountPresentation : IPersonalAccountPresentation
    {
        private IMapper _mapper;
        private IUserService _userService;
        private ICitizenRepository _citizenRepository;
        private IBuildingRepository _buildingRepository;
        private IPersonalAccountRepository _accountRepository;

        public PersonalAccountPresentation(IMapper mapper, IUserService userService,
            ICitizenRepository citizenRepository, IPersonalAccountRepository accountRepository,
            IBuildingRepository buildingRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _userService = userService;
            _buildingRepository = buildingRepository;
            _citizenRepository = citizenRepository;
        }


        public PersonalAccountViewModel CreateBaseViewModel()
        {
            var citizen = _userService.GetUser();

            var viewModel = new PersonalAccountViewModel
            {
                CitizenId = citizen.Id,
                UserName = citizen.Name,
                DateRegistration = DateTime.Now,
                SerialNumber = GenerateSerialNumber()

            };

            return viewModel;
        }

        public PersonalAccountViewModel GetNewAccountDetails()
        {
            var citizen = _userService.GetUser();
            var account = citizen.PersonalAccounts.ElementAt(citizen.PersonalAccounts.Count - 1);

            return _mapper.Map<PersonalAccountViewModel>(account);
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

        public List<PersonalAccount> GetAllOwnAccounts()
        {
            var citizen = _userService.GetUser();

            return _accountRepository
                .GetAll()
                .Where(x => x.CitizenId == citizen.Id)
                .ToList();
        }

        public void InizializeAccount(PersonalAccountViewModel viewModel)
        {
            viewModel.Debt = 0;
            viewModel.DateLastPayment = DateTime.Now;
            viewModel.Number = GenerateAccountNumber();
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

        public Building GetBuildingByAddress(string fullAddress)
        {
            string street = new Regex(@"[(\d-\s]").Replace(fullAddress, string.Empty);
            int adressNumber = int.Parse(new Regex(@"[\D]").Replace(fullAddress, string.Empty));

            return _buildingRepository
                .GetAll()
                .SingleOrDefault(x => x.Adress.Street == street
                && x.Adress.HouseNumber == adressNumber);
        }

        public int GetCalculateConsumption(Building building)
        {
            int count = building.ElectricBill.ElectricityMeters.Count();
            int daysInMonth = 30;
            int averageArea = 50;
            int consumptionInMonth = 250;
            int currentClientCount = count + 1;

            return ((building.TotalArea / averageArea) *
                (consumptionInMonth * (count != 0 ? currentClientCount : 1)))
                / daysInMonth;
        }

        public void RegisterAccount(PersonalAccountViewModel viewModel, Building building)
        {
            var citizen = _userService.GetUser();
            var personalAccount = _mapper.Map<PersonalAccount>(viewModel);

            personalAccount.Meter.ElectricBill = new ElectricBill
            {
                Id = building.ElectricBill.Id,
                TotalDebt = building.ElectricBill.TotalDebt + personalAccount.Meter.Debt,
                Consumption = building.ElectricBill.Consumption + personalAccount.Meter.Consumption
            };

            citizen.PersonalAccounts.Add(personalAccount);
            building.ElectricBill.ElectricityMeters.Add(personalAccount.Meter);

            _buildingRepository.Save(building);
            _citizenRepository.Save(citizen);
        }

        public PersonalAccount CalculateDebt(PersonalAccount account)
        {
            int tariffRateMin = 10;
            int tariffRateMax = 15;
            int tariffRate = tariffRateMin;
            int tariffPersonNatural = 5;
            int tariffPersonJuridical = 10;
            int tariffPerson = tariffPersonNatural;
            var countDayDebt = DateTime.Now - account.DateLastPayment;

            if (account.Tariff.Rate == Rate.Max)
            {
                tariffRate = tariffRateMax;
            }

            if (account.Tariff.Person == EfStuff.Model.Energy.Person.Juridical)
            {
                tariffPerson = tariffPersonJuridical;
            }

            int newDebt = -1 * ((tariffRate + tariffPerson) *
                (account.Meter.Consumption * countDayDebt.Days));

            if (account.Meter.Debt != newDebt)
            {
                account.Meter.ElectricBill.TotalDebt += -1 * account.Meter.Debt;
                account.Meter.Debt = newDebt;
                account.Meter.ElectricBill.TotalDebt += account.Meter.Debt;

                _accountRepository.SaveBalance(account);
            }

            return account;
        }

        public List<PersonalAccountViewModel> GetViewModels(IEnumerable<PersonalAccount> accounts)
        {
            return accounts.Select(x => _mapper.Map<PersonalAccountViewModel>(x)).ToList();
        }
    }
}

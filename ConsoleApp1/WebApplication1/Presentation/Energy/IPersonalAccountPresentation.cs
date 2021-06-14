using System.Collections.Generic;
using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.Models.Energy;

namespace WebApplication1.Presentation.Energy
{
    public interface IPersonalAccountPresentation
    {
        public PersonalAccountViewModel CreateBaseViewModel();

        public PersonalAccountViewModel GetNewAccountDetails();

        public List<PersonalAccount> GetAllOwnAccounts();

        public void InizializeAccount(PersonalAccountViewModel viewModel);

        public Building GetBuildingByAddress(string fullAddress);

        public int GetCalculateConsumption(Building building);

        public void RegisterAccount(PersonalAccountViewModel viewModel, Building building);

        public PersonalAccount CalculateDebt(PersonalAccount account);

        public List<PersonalAccountViewModel> GetViewModels(IEnumerable<PersonalAccount> accounts);
    }
}

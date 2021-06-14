using WebApplication1.EfStuff.Model.Energy;

namespace WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces
{
    public interface IPersonalAccountRepository : IBaseRepository<PersonalAccount>
    {
        public PersonalAccount GetByCitizenId(long id);

        public PersonalAccount GetByNumber(string accountNumber);

        public PersonalAccount SaveBalance(PersonalAccount account);
    }
}

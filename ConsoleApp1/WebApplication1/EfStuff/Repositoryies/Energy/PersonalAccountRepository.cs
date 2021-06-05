using System.Linq;
using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;

namespace WebApplication1.EfStuff.Repositoryies.Energy
{
    public class PersonalAccountRepository : BaseRepository<PersonalAccount>, IPersonalAccountRepository
    {
        public PersonalAccountRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public PersonalAccount GetCitizen(long id)
        {
            return _kzDbContext.PersonalAccounts.SingleOrDefault(x => x.CitizenId == id);
        }
    }
}

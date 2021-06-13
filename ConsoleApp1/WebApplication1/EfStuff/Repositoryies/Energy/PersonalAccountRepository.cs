using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;

namespace WebApplication1.EfStuff.Repositoryies.Energy
{
    public class PersonalAccountRepository : BaseRepository<PersonalAccount>, IPersonalAccountRepository
    {
        public PersonalAccountRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public PersonalAccount GetByCitizenId(long id)
        {
            return _kzDbContext.PersonalAccounts.SingleOrDefault(x => x.CitizenId == id);
        }

        public PersonalAccount GetByNumber(string accountNumber)
        {
            return _kzDbContext.PersonalAccounts.SingleOrDefault(x => x.Number == accountNumber);
        }

        public PersonalAccount SaveBalance(PersonalAccount account)
        {
            using var transaction = _kzDbContext.Database.BeginTransaction();

            try
            {
                if (account.Id > 0)
                {
                    _dbSet.Update(account);
                }
                else
                {
                    _dbSet.Add(account);
                }

                _kzDbContext.SaveChanges();

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();      
                throw;
            }

            return account;
        }
    }
}

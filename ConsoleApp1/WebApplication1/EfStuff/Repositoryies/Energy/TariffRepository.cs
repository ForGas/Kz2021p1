using System.Linq;
using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;

namespace WebApplication1.EfStuff.Repositoryies.Energy
{
    public class TariffRepository : BaseRepository<Tariff>, ITariffRepository
    {
        public TariffRepository(KzDbContext kzDbContext) : base(kzDbContext) { }


        public void DeleteById(long id)
        {
            var model =_dbSet.SingleOrDefault(x => x.Id == id);

            if (model != default)
            {
                _dbSet.Remove(model);
                _kzDbContext.SaveChanges();
            }
        }
    }
}

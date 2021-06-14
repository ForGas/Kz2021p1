using WebApplication1.EfStuff.Model.Energy;

namespace WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces
{
    public interface ITariffRepository : IBaseRepository<Tariff>
    {
        public void DeleteById(long id);
    }
}

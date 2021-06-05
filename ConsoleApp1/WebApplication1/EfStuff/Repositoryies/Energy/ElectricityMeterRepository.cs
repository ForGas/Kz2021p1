using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;

namespace WebApplication1.EfStuff.Repositoryies.Energy
{
    public class ElectricityMeterRepository : BaseRepository<ElectricityMeter>, IElectricityMeterRepository
    {
        public ElectricityMeterRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
    }
}

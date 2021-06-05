using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;

namespace WebApplication1.EfStuff.Repositoryies.Energy
{
    public class ElectricBillRepository : BaseRepository<ElectricBill>, IElectricBillRepository
    {
        public ElectricBillRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
    }
}

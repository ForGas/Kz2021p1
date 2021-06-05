using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;

namespace WebApplication1.EfStuff.Repositoryies.Energy
{
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        public DistrictRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
    }
}

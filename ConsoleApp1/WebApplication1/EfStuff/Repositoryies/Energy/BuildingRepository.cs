using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;

namespace WebApplication1.EfStuff.Repositoryies.Energy
{
    public class BuildingRepository : BaseRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Energy;

namespace WebApplication1.Models.Energy
{
    public class BuildingViewModel
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int FloorCount { get; set; }

        public DistrictName DistrictName { get; set; }

        public int TotalArea { get; set; }
        public int TotalDebt { get; set; }
        public int Consumption { get; set; }
    }
}

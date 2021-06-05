using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Energy
{
    public class ElectricBill : BaseModel
    {
        public int TotalDebt { get; set; }
        public int Consumption { get; set; }

        /// <summary>
        /// Счетчики за электричество у здания
        /// </summary>
        public virtual ICollection<ElectricityMeter> ElectricityMeters { get; set; }
        public virtual Building Building { get; set; }
    }
}

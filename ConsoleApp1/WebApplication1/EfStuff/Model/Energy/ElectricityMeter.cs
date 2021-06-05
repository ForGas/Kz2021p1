using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Energy
{
    public class ElectricityMeter : BaseModel
    {
        /// <summary>
        /// Серийный номер у счетчика для регистраций
        /// </summary>
        public string SerialNumber { get; set; }

        public int Consumption { get; set; }

        public int Debt { get; set; }

        public virtual ElectricBill ElectricBill { get; set; }

        public virtual PersonalAccount Account { get; set; }
    }
}

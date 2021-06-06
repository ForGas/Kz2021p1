using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Energy
{
    public class PersonalAccount : BaseModel
    {
        public string Number { get; set; }

        public DateTime DateRegistration { get; set; }

        public DateTime DateLastPayment { get; set; }

        public long TariffId { get; set; }

        public virtual Tariff Tariff { get; set; }

        public long ElectricityMeterId { get; set; }

        public virtual ElectricityMeter Meter { get; set; }

        public long CitizenId { get; set; }

        public virtual Citizen Citizen { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Energy
{
    public class Building : BaseModel
    {
        public int TotalArea { get; set; }

        public long ElectricBillId { get; set; }

        public virtual ElectricBill ElectricBill { get; set; }

        public virtual District District { get; set; }

        public long AdressId { get; set; }

        public virtual Adress Adress { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Energy
{
    public class District : BaseModel
    {
        public DistrictName Name { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
    }
}

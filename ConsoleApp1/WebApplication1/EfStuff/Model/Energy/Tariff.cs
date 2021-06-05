using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Energy
{
    public class Tariff : BaseModel
    {
        /// <summary>
        /// Физическое или Юридическое лицо
        /// </summary>
        public Person Person { get; set; }
        public Rate Rate { get; set; }
        public virtual PersonalAccount Account { get; set; }
    }
}

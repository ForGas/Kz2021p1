using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Energy;

namespace WebApplication1.Models.Energy
{
    public class PersonalAccountViewModel
    {
        public long Id { get; set; }

        public long TariffId { get; set; }

        public long CitizenId { get; set; }

        public long ElectricityMeterId { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public string Number { get; set; }

        public Rate Rate { get; set; }

        public Person Person { get; set; }

        public DateTime DateRegistration { get; set; }

        public string SerialNumber { get; set; }

        public int Consumption { get; set; }

        public int Debt { get; set; }
    }
}

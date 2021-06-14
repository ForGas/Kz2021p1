using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.Models.Energy;

namespace WebApplication1.Profiles
{
    public class PersonalAccountProfiles : Profile
    {
        public PersonalAccountProfiles()
        {
            CreateMap<PersonalAccount, PersonalAccountViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(p => p.Id))
                .ForMember(x => x.Number, opt => opt.MapFrom(p => p.Number))
                .ForMember(x => x.TariffId, opt => opt.MapFrom(p => p.TariffId))
                .ForMember(x => x.Person, opt => opt.MapFrom(p => p.Tariff.Person))
                .ForMember(x => x.Rate, opt => opt.MapFrom(p => p.Tariff.Rate))
                .ForMember(x => x.CitizenId, opt => opt.MapFrom(p => p.CitizenId))
                .ForMember(x => x.UserName, opt => opt.MapFrom(p => p.Citizen.Name))
                .ForMember(x => x.ElectricityMeterId, opt => opt.MapFrom(p => p.ElectricityMeterId))
                .ForMember(x => x.SerialNumber, opt => opt.MapFrom(p => p.Meter.SerialNumber))
                .ForMember(x => x.Debt, opt => opt.MapFrom(p => p.Meter.Debt))
                .ForMember(x => x.DateRegistration, opt => opt.MapFrom(p => p.DateRegistration))
                .ForMember(x => x.DateLastPayment, opt => opt.MapFrom(p => p.DateLastPayment))
                .ForMember(x => x.Consumption, opt => opt.MapFrom(p => p.Meter.Consumption))
                .ForMember(x => x.Address, opt => opt.MapFrom(p => string.Format("{0} {1}",
                    p.Meter.ElectricBill.Building.Adress.Street,
                    p.Meter.ElectricBill.Building.Adress.HouseNumber)))
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(p => p.Id))
                .ForMember(x => x.Number, opt => opt.MapFrom(p => p.Number))
                .ForMember(x => x.TariffId, opt => opt.MapFrom(p => p.TariffId))
                .ForMember(x => x.CitizenId, opt => opt.MapFrom(p => p.CitizenId))
                .ForMember(x => x.ElectricityMeterId, opt => opt.MapFrom(p => p.ElectricityMeterId))
                .ForMember(x => x.DateLastPayment, opt => opt.MapFrom(p => p.DateLastPayment))
                .ForMember(x => x.DateRegistration, opt => opt.MapFrom(p => p.DateRegistration))
                .ForMember(x => x.Meter, opt => opt.MapFrom(p => new ElectricityMeter()
                {
                    Id = p.ElectricityMeterId,
                    SerialNumber = p.SerialNumber,
                    Debt = p.Debt,
                    Consumption = p.Consumption,
                }))
                .ForMember(x => x.Tariff, opt => opt.MapFrom(p => new Tariff()
                {
                    Id = p.TariffId,
                    Person = p.Person,
                    Rate = p.Rate,
                }));
        }
    }
}

using AutoMapper;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.Models.Energy;

namespace WebApplication1.Profiles
{
    public class BuildingProfiles : Profile
    {
        public BuildingProfiles()
        {
            CreateMap<Building, BuildingViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(x => x.Consumption, opt => opt.MapFrom(b => b.ElectricBill.Consumption))
                .ForMember(x => x.Street, opt => opt.MapFrom(b => b.Adress.Street))
                .ForMember(x => x.FloorCount, opt => opt.MapFrom(b => b.Adress.FloorCount))
                .ForMember(x => x.HouseNumber, opt => opt.MapFrom(b => b.Adress.HouseNumber))
                .ForMember(x => x.DistrictName, opt => opt.MapFrom(b => b.District.Name))
                .ForMember(x => x.TotalDebt, opt => opt.MapFrom(b => b.ElectricBill.TotalDebt))
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(x => x.TotalArea, opt => opt.MapFrom(b => b.TotalArea))
                .ForMember(x => x.Adress, opt => opt.MapFrom(b => new Adress()
                {
                    FloorCount = b.FloorCount,
                    HouseNumber = b.HouseNumber,
                    Street = b.Street,
                }))
                .ForMember(x => x.District, opt => opt.MapFrom(b => new District()
                {
                    Name = b.DistrictName,
                }))
                .ForMember(x => x.ElectricBill, opt => opt.MapFrom(b => new ElectricBill()
                {
                    TotalDebt = b.TotalDebt,
                    Consumption = b.Consumption
                }));
        }
    }
}

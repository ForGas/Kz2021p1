using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.Models.Energy;

namespace WebApplication1.Profiles
{
    public class PersonalAccountProfiles : Profile
    {
        public PersonalAccountProfiles()
        {
            CreateMap<PersonalAccount, PersonalAccountViewModel>();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Energy;
using WebApplication1.EfStuff.Repositoryies.Energy.Intrefaces;
using WebApplication1.Models.Energy;

namespace WebApplication1.Controllers.Energy
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalAccountApiController : ControllerBase
    {
        private IMapper _mapper;
        private IPersonalAccountRepository _accountRepository;

        public PersonalAccountApiController(IMapper mapper, IPersonalAccountRepository accountRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
        }


        [HttpPost]
        public ActionResult<PersonalAccountViewModel> Post([FromBody] string accountNumber)
        {
            var model = _accountRepository.GetByNumber(accountNumber);

            return _mapper.Map<PersonalAccountViewModel>(model);
        }
    }
}

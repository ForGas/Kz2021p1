using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Energy
{
    public enum Person
    {
        [Display(Name = "Физическое")]
        Natural = 1,

        [Display(Name = "Юридическое")]
        Juridical = 2
    }
}

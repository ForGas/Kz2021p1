using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Energy
{
    public enum DistrictName
    {
        [EnumMember(Value = "Red District")]
        RedDistrict = 1,

        [EnumMember(Value = "Blue District")]
        BlueDistrict = 2,

        [EnumMember(Value = "Green District")]
        GreenDistrict = 3
    }
}

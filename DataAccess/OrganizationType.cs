using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public enum OrganizationType
    {
        [Description("Олімпіади")]
        Olempyada = 1,
        [Description("Статті")]
        Articles = 2,
        [Description("Конференції")]
        Meetings = 3
    }
}

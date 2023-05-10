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
        [Description("Олімпіада")]
        Olempyada = 1,
        [Description("Стаття")]
        Article = 2,
        [Description("Конференція")]
        Meeting = 3,
        [Description("Підвищення кваліфікації")]
        Qualification = 4,
        [Description("Методична розробка")]
        Methodical = 5
    }
}

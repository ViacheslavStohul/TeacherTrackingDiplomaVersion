using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class UserFullModel
    {
        public UserInfo User { get; set; }

        public Department Department { get; set; }
    }
}

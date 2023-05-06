using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class UserBasicUpdateRequestModel : IUserValidation
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string MiddleName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}

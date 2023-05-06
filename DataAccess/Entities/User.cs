using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class User
    {
        public int IdUser { get; set; }

        public UserInfo UserInfo { get; set; }

        [MaxLength(25)]
        public string Login { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }
    }
}

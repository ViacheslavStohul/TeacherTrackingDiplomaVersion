using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class UserChangeResponseModel
    {
        public UserTableModel User { get; set; }

        public List<string> Ranks { get; set; }

        public List<string> Commissions { get; set; }

        public List<string> Chairs { get; set; }

        public List<string> WorkTypes { get; set; }

        public List<string> AccessLevels { get; set; }
    }
}

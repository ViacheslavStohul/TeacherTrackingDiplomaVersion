using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class ChairChangeResponseModel
    {
        public ChairTableModel Model { get; set; }

        public List<string> Departments { get; set; }

        public List<string> Users { get; set; }
    }
}

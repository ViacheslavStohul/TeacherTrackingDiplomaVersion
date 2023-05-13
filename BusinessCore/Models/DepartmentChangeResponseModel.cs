using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class DepartmentChangeResponseModel
    {
        public DepartmentTableModel Model { get; set; }

        public List<string> Users { get; set; }
    }
}

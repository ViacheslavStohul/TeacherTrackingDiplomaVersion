using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class CommissionChangeResponseModel
    {
        public CommissionTableModel Model { get; set; }

        public List<string> Departments { get; set; }

        public List<string> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class WorkChangeResponseModel
    {
        public WorkTableModel Model { get; set; }

        public List<string> Types { get; set; }
        
    }
}

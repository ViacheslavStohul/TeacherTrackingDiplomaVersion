using BusinessCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Services.Interfaces
{
    public interface IWorkServise
    {
        Task<List<WorkTableModel>> GetWorkTableAsync(int id);
    }
}

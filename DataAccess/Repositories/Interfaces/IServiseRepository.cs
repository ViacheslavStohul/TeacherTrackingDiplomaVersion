using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IServiseRepository
    {
        Task<List<string>> GetRankNamesAsync();

        Task<List<string>> GetAccessLevelNamesAsync();

        Task<List<string>> GetWorkTypeNamesAsync();
    }
}

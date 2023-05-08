using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IChairRepository
    {
        Task<List<string>> GetChairAbbreviationsAsync();
        Task<List<string>> GetChairNamesAsync();

        Task<Chair> GetChairBynameAsync(string name);
    }
}

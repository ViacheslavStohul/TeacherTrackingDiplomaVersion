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

        Task<List<Chair>> GetChairsAsync();

        Task<Chair> GetChairByIdAsync(int id);

        Task<int> ChangeChairAsync(Chair chair);

        Task<Chair> CreateChairAsync(Chair chair);


        Task<int> DeleteChairAsync(Chair chair);
    }
}

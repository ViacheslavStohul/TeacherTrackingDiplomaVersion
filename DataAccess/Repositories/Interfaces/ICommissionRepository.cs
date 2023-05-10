using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface ICommissionRepository
    {
        Task<List<string>> GetCommissionAbbreviationsAsync();

        Task<List<string>> GetCommissionNamesAsync();

        Task<Commission> GetCommissionByNameAsync(string name);

        Task<List<Commission>> GetCommissions();

        Task<Commission> GetCommisionByIdAsync(int id);

        Task<int> UpdateCommissionAsync(Commission commission);

        Task<Commission> CreateCommissionAsync(Commission commission);

        Task<int> DeleteCommissionAsync(Commission commission);
    }
}

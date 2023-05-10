using BusinessCore.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Services.Interfaces
{
    public interface ICommissionServise
    {
        Task<List<CommissionTableModel>> GetCommissionTableAsync();

        Task<CommissionChangeResponseModel> GetDataToChangeCommisionPageAsync(int id);

        Task<int> UpdateCommissionAsync(CommissionTableModel model, UserInfo user, string ip);

        Task<int> CreateCommissionAsync(CommissionTableModel model, UserInfo user, string ip);

        Task<int> DeleteCommissionAsync(int id, UserInfo user, string ip);
    }
}

using BusinessCore.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Services.Interfaces
{
    public interface IChairServise
    {
        Task<List<ChairTableModel>> GetChairTableAsync();

        Task<ChairChangeResponseModel> GetDataToChangeCommisionPageAsync(int id);

        Task<int> AddChairAsync(ChairTableModel model, UserInfo user, string ip);

        Task<int> UpdateChairAsync(ChairTableModel model, UserInfo user, string ip);

        Task<int> DeleteChairAsync(int id, UserInfo user, string ip);
    }
}

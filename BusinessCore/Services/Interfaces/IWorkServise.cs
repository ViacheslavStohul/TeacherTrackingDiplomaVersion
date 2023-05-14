using BusinessCore.Models;
using DataAccess.Entities;
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

        Task<WorkChangeResponseModel> GetWorkToChangeAsync(int idUserIndo, int id);

        Task<int> AddWorkAsync(WorkTableModel model, UserInfo user, string ip);

        Task<int> UpdateWorkAsync(WorkTableModel model, UserInfo user, string ip);

        Task<int> DeleteWorkAsync(int id, int userId, UserInfo user, string ip);
    }
}

using BusinessCore.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Services.Interfaces
{
    public interface IDepartmentServise
    {
        Task<List<DepartmentTableModel>> GetDapartmentTableAsync();

        Task<DepartmentChangeResponseModel> GetDataToChangeDepartmentPageAsync(int id);

        Task<int> CreateDepartmentAsync(DepartmentTableModel model, UserInfo user, string ip);

        Task<int> UpdateDepartmentAsync(DepartmentTableModel model, UserInfo user, string ip);

        Task<int> DeleteDepartmentAsync(int id, UserInfo user, string ip);
    }
}

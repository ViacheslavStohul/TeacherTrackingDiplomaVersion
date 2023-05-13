using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Department GetDepartmentByChairAndComission(Chair chair, Commission commission);

        bool IsChairAndCommissionCorrect(Chair chair, Commission commission);

        Task<Department> GetDepartmentByCommission(Commission commission);

        List<string> GetDepartmentNames();

        Task<int> UpdateDepartmentCommissionAsync(Commission commission, string newDepartment);

        Task<Department> GetDepartmentByNameAsync(string name);

        Task<int> DeleteChairFromDepartmentAsync(Chair chair);

        Task<List<Department>> GetDepartmentsAsync();

        Task<Department> GetDepartmentByIdAsync(int id);

        Task<int> UpdateDepartmentAsync(Department department);

        Task<Department> AddDepartmentAsync(Department department);

        Task<int> DeleteDepartmentAsync(Department department);
    }
}

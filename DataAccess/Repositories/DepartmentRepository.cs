using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext context;

        private readonly IUserRepository _userRepository;

        public DepartmentRepository(DataContext context, IUserRepository userRepository)
        {
            this.context = context;
            _userRepository = userRepository;
        }

        public Department GetDepartmentByChairAndComission(Chair chair, Commission commission)
        {
            return this.context.Departments.Where(d => d.Chair == chair && d.Commissions.Contains(commission)).FirstOrDefault();
        }
        
        public bool IsChairAndCommissionCorrect(Chair chair, Commission commission)
        {
            return this.context.Departments.Any(d => d.Chair == chair && d.Commissions.Contains(commission));
        }

        public async Task<Department> GetDepartmentByCommission(Commission commission)
        {
            return await this.context.Departments.Where(d => d.Commissions.Contains(commission)).FirstOrDefaultAsync();
        }

        public List<string> GetDepartmentNames()
        {
            return this.context.Departments.Select(s => s.Name).ToList();
        }

        public async Task<int> UpdateDepartmentCommissionAsync(Commission commission, string newDepartment)
        {
            Department oldDepartment = await this.context.Departments.Where(d => d.Commissions.Contains(commission)).Include(c => c.Commissions).FirstOrDefaultAsync();

            if (oldDepartment != null)
            {
                if (oldDepartment.Name == newDepartment)
                {
                    return 0;
                }

                oldDepartment.Commissions.Remove(commission);
            }

            Department newDep = await this.context.Departments.Where(d => d.Name == newDepartment).Include(c => c.Commissions).FirstOrDefaultAsync();

            newDep.Commissions.Add(commission);

            await this.context.SaveChangesAsync();

            return await _userRepository.UpdateUsersCommission(commission);
        }

        public async Task<Department> GetDepartmentByNameAsync(string name)
        {
            return await this.context.Departments.Where(d => d.Name == name).FirstOrDefaultAsync();
        }

        public async Task<int> DeleteChairFromDepartmentAsync(Chair chair)
        {
            Department toUpdate = await this.context.Departments.Where(d => d.Chair == chair).FirstOrDefaultAsync();

            toUpdate.Chair = await this.context.Chairs.Where(ch => ch.ChairId == 1).FirstOrDefaultAsync();

            return await this.context.SaveChangesAsync();
        }


        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await this.context.Departments.Where(d => d.DepartamentId != 1).Include(d => d.Head).ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await this.context.Departments
                .Where(d => d.DepartamentId == id)
                .Include(h => h.Head)
                .Include(c => c.Commissions)
                .Include(ch => ch.Chair)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateDepartmentAsync(Department department)
        {
            Department toUpdate = await this.context.Departments.Where(d => d.DepartamentId == department.DepartamentId).FirstOrDefaultAsync();

            if (toUpdate == null)
            {
                throw new Exception("Такого відділення не існує");
            };

            toUpdate.Name = department.Name;
            toUpdate.Abbreviatoin = department.Abbreviatoin;
            toUpdate.Head = department.Head;

            return await this.context.SaveChangesAsync();
            
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            department.Chair = await this.context.Chairs.Where(ch => ch.ChairId == 1).FirstOrDefaultAsync();
            this.context.Departments.Add(department);
            await this.context.SaveChangesAsync();
            return department;
        }

        public async Task<int> DeleteDepartmentAsync(Department department)
        {
            List<Commission> commissions = department.Commissions.ToList();
            await this.context.Departments.Where(d => d.DepartamentId == 1).ForEachAsync(d => d.Commissions = commissions);
            this.context.Departments.Remove(department);

            return await this.context.SaveChangesAsync();
        }
    }
}

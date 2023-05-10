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
            var a = this.context.Departments.Where(d => d.Abbreviatoin == "-").Select(d => d.Commissions);
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

        public async Task<int> UpdateDepartmentCommission(Commission commission, string newDepartment)
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
    }
}

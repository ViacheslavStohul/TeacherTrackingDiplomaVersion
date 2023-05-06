using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext context;

        public DepartmentRepository(DataContext context)
        {
            this.context = context;
        }

        public Department GetDepartmentByChairAndComission(Chair chair, Commission commission)
        {
            return this.context.Departaments.Where(d => d.Chair == chair && d.Comissions.Contains(commission)).FirstOrDefault();
        }
    }
}

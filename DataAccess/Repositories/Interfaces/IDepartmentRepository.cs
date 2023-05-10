﻿using DataAccess.Entities;
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

        Task<int> UpdateDepartmentCommission(Commission commission, string newDepartment);
    }
}

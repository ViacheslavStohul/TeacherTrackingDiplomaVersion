﻿using DataAccess.Entities;
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
    }
}

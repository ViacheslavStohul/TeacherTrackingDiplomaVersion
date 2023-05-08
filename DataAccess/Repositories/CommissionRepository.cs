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
    public class CommissionRepository : ICommissionRepository
    {
        private readonly DataContext context;

        public CommissionRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<string>> GetCommissionAbbreviationsAsync()
        {
            return await this.context.Commissions.Select(c => c.Abbreviation).ToListAsync();
        }

        public async Task<List<string>> GetCommissionNamesAsync()
        {
            return await this.context.Commissions.Select(c => c.Name).ToListAsync();
        }

        public async Task<Commission> GetCommissionByNameAsync(string name)
        {
            return await this.context.Commissions.Where(c => c.Name == name).FirstOrDefaultAsync();
        }
    }
}

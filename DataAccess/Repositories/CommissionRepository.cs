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
            return await this.context.Comissions.Select(c => c.Abbreviation).ToListAsync();
        }
    }
}

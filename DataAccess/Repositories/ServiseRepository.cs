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
    public class ServiseRepository : IServiseRepository
    {
        private readonly DataContext context;

        public ServiseRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<string>> GetRankNamesAsync()
        {
            return await this.context.Ranks.Select(r => r.Name).ToListAsync();
        }

        public async Task<List<string>> GetAccessLevelNamesAsync()
        {
            return await this.context.AccessLevels.Select(l => l.Name).ToListAsync();
        }

        public async Task<List<string>> GetWorkTypeNamesAsync()
        {
            return await this.context.WorkTypes.Select(w => w.Name).ToListAsync();
        }

        public async Task<AccessLevel> GetAccessLevelByNameAsync(string name)
        {
            return await this.context.AccessLevels.Where(l => l.Name == name).FirstOrDefaultAsync();
        }

        public async Task<WorkType> GetWorkTypeByNameAsync(string name)
        {
            return await this.context.WorkTypes.Where(l => l.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Rank> GetRankTypeByNameAsync(string name)
        {
            return await this.context.Ranks.Where(l => l.Name == name).FirstOrDefaultAsync();
        }
    }
}

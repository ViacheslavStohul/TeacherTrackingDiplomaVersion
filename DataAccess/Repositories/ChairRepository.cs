using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ChairRepository : IChairRepository
    {
        private readonly DataContext context;

        public ChairRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<string>> GetChairAbbreviationsAsync()
        {
            return await this.context.Chairs.Select(c => c.Abbreviation).ToListAsync();
        }
    }
}

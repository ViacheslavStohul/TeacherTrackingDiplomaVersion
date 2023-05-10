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

        public async Task<List<Commission>> GetCommissions()
        {
            List<Commission> commissions = await this.context.Commissions.Where(c => c.Name != "Відсутня").ToListAsync();
            commissions.ForEach(c => c.Head = this.context.ComissionHeads.Where(co => co.Comission == c).Select(c => c.Head).FirstOrDefault());
            return commissions;
        }

        public async Task<Commission> GetCommisionByIdAsync(int id)
        {
            Commission commission = await this.context.Commissions.Where(c => c.ComissionId == id).FirstOrDefaultAsync();
            if (commission != null)
            {
                commission.Head = await this.context.ComissionHeads.Where(c => c.Comission == commission).Select(c => c.Head).FirstOrDefaultAsync();
            }
            return commission;
        }

        public async Task<int> UpdateCommissionAsync(Commission commission)
        {
            Commission toUpdate = this.context.Commissions.Where(c => c.ComissionId == commission.ComissionId).FirstOrDefault();

            toUpdate.Name = commission.Name;
            toUpdate.Abbreviation = commission.Abbreviation;

            return await this.context.SaveChangesAsync();
        }

        public async Task<Commission> CreateCommissionAsync(Commission commission)
        {
            if (this.context.Commissions.Any(c => c.Name == commission.Name))
            {
                throw new Exception("Така комісія вже існує");
            }

            this.context.Commissions.Add(commission);

            await this.context.SaveChangesAsync();

            return commission;
        }

        public async Task<int> DeleteCommissionAsync(Commission commission)
        {
            if (this.context.UserInfos.Any(c => c.Commission == commission))
            {
                throw new Exception("Неможливо видалити комісію, на яку призначені користувачі");
            }

            this.context.ComissionHeads.RemoveRange(this.context.ComissionHeads.Where(c => c.Comission == commission));

            this.context.Commissions.Remove(commission);

            return await this.context.SaveChangesAsync();
        }
    }
}

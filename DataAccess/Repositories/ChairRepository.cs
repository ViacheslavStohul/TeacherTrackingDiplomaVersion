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

        public async Task<List<string>> GetChairNamesAsync()
        {
            return await this.context.Chairs.Select(c => c.Name).ToListAsync();
        }

        public async Task<Chair> GetChairBynameAsync(string name)
        {
            return await this.context.Chairs.Where(ch => ch.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Chair>> GetChairsAsync()
        {
            IQueryable<Chair> chairs = this.context.Chairs.Where(c => c.ChairId != 1);

            await chairs.ForEachAsync(ch => ch.Department = this.context.Departments.Where(d => d.Chair == ch).FirstOrDefault());

            await chairs.ForEachAsync(ch => ch.Head = this.context.ChairHeads.Where(c => c.Chair == ch).Select(c => c.Head).FirstOrDefault());

            return await chairs.ToListAsync();
        }

        public async Task<Chair> GetChairByIdAsync(int id)
        {
            Chair chair = await this.context.Chairs.Where(c => c.ChairId == id).FirstOrDefaultAsync();
            if (chair != null)
            {
                chair.Department = await this.context.Departments.Where(d => d.Chair == chair).FirstOrDefaultAsync();
                chair.Head = await this.context.ChairHeads.Where(c => c.Chair == chair).Select(c => c.Head).FirstOrDefaultAsync();
            }
            return chair;
        }

        public async Task<int> ChangeChairAsync(Chair chair)
        {
            Chair toUpdate = await this.context.Chairs.Where(ch => ch.ChairId == chair.ChairId).FirstOrDefaultAsync();
            toUpdate.Department = this.context.Departments.Where(d => d.Chair == toUpdate).FirstOrDefault();

            toUpdate.Name = chair.Name;
            toUpdate.Abbreviation = chair.Abbreviation;

            if (toUpdate.Department == null)
            {
                Department department = this.context.Departments.Where(d => d.DepartamentId == chair.Department.DepartamentId).Include(ch => ch.Chair).FirstOrDefault();

                if (department.Chair.ChairId != 1)
                {
                    throw new Exception("Відділення може містити тільки одну кафедру");
                }
                department.Chair = toUpdate;
            }

            return await this.context.SaveChangesAsync();
        }

        public async Task<Chair> CreateChairAsync(Chair chair)
        {
            if (this.context.Chairs.Any(ch => ch.Name == chair.Name))
            {
                throw new Exception("Така кафедра уже існує");
            }

            if (this.context.Departments.Any(d => d.Name == chair.Department.Name && d.Chair != null && d.Chair.ChairId != 1))
            {
                throw new Exception("Відділення може містити тільки одну кафедру");
            }

            this.context.Chairs.Add(chair);

            chair.Department.Chair = chair;

            chair.Department = this.context.Departments.Where(d => d.DepartamentId == chair.Department.DepartamentId).Include(d => d.Commissions).FirstOrDefault();

            await this.context.UserInfos.Where(u => chair.Department.Commissions.Contains(u.Commission)).ForEachAsync(u => u.Chair = chair);

            await this.context.SaveChangesAsync();

            return chair;
        }

        public async Task<int> DeleteChairAsync(Chair chair)
        {
            this.context.Remove(chair);

            return await this.context.SaveChangesAsync();
        }


    }
}

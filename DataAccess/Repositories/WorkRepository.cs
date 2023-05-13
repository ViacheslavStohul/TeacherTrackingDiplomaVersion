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
    public class WorkRepository : IWorkRepository
    {
        private readonly DataContext context;

        public WorkRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<OrganizationalWork> GetOrganizationWorkByIdAsync(int id)
        {
            return await this.context.OrganizationalWorks.Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> AddOrganiztionWork(OrganizationalWork work, UserInfo user)
        {
            this.context.OrganizationalWorks.Add(work);
            user.OrganizationalWorks.Add(work);

            return await this.context.SaveChangesAsync();
        }

        public async Task<int> UpdateOrganizationAsync(OrganizationalWork work)
        {
            OrganizationalWork toUpdate = await this.GetOrganizationWorkByIdAsync(work.Id);

            toUpdate.OrganizationType = work.OrganizationType;
            toUpdate.Description = work.Description;
            toUpdate.Name = work.Name;
            toUpdate.Date = work.Date;

            return await this.context.SaveChangesAsync();
        }

        public async Task<int> DeleteOrganizationWork(OrganizationalWork work, UserInfo user)
        {
            user.OrganizationalWorks.Remove(work);

            this.context.OrganizationalWorks.Remove(work);

            return await this.context.SaveChangesAsync();
        }
    }
}

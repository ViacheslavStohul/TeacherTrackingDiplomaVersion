using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IWorkRepository
    {
        Task<OrganizationalWork> GetOrganizationWorkByIdAsync(int id);

        Task<int> AddOrganiztionWork(OrganizationalWork work, UserInfo user);

        Task<int> UpdateOrganizationAsync(OrganizationalWork work);

        Task<int> DeleteOrganizationWork(OrganizationalWork work, UserInfo user);
    }
}

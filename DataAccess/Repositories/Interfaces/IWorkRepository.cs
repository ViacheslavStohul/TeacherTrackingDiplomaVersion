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

        Task<OrganizationalWork> AddOrganiztionWorkAsync(OrganizationalWork work, UserInfo user);

        Task<int> UpdateOrganizationAsync(OrganizationalWork work);

        Task<int> DeleteOrganizationWorkAsync(OrganizationalWork work, UserInfo user);

        List<string> GetWorkTypes();

        Task<OrganizationWorkType> GetWorkTypeByDescriptionAsync(string description);
    }
}

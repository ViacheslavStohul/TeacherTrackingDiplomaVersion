using BusinessCore.Models;
using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Services
{
    public class WorkServise : IWorkServise
    {
        private readonly IWorkRepository _workRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogRepository _logRepository;

        public WorkServise(IWorkRepository workRepository, IUserRepository userRepository, ILogRepository logRepository)
        {
            _workRepository = workRepository;
            _userRepository = userRepository;
            _logRepository = logRepository;
        }

        public async Task<List<WorkTableModel>> GetWorkTableAsync(int id)
        {
            List<WorkTableModel> models = new List<WorkTableModel>();
            UserInfo user = await _userRepository.GetUserInfoByIdAsync(id);
            foreach (var item in user.OrganizationalWorks)
            {
                WorkTableModel model = new WorkTableModel();
                model.ToModel(item, user);

                models.Add(model);
            }

            return models;
        }

        public async Task<WorkChangeResponseModel> GetWorkToChangeAsync(int idUserIndo, int id)
        {
            WorkTableModel model = new WorkTableModel();

            OrganizationalWork work = await _workRepository.GetOrganizationWorkByIdAsync(id);

            if (work != null)
            {
                model.ToModel(work, await _userRepository.GetUserInfoByIdAsync(idUserIndo));
            }

            return new WorkChangeResponseModel
            {
                Model = model,
                Types = _workRepository.GetWorkTypes().Where(t => t != model.OrganizationType).ToList()
            };
        }

        public async Task<int> AddWorkAsync(WorkTableModel model, UserInfo user, string ip)
        {
            UserInfo updatedUser = _userRepository.GetFullUserInfo(model.User);

            if (updatedUser == null)
            {
                throw new Exception("Помилка. Зверніться до адміністратора");
            }

            DateTime date;
            if (DateTime.TryParseExact(model.Date, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                OrganizationalWork newWork = await _workRepository.AddOrganiztionWorkAsync(new OrganizationalWork
                {
                    OrganizationType = await _workRepository.GetWorkTypeByDescriptionAsync(model.OrganizationType),
                    Name = model.Name,
                    Description = model.Description,
                    Date = date,
                }, updatedUser);

                return await _logRepository.LogDataAsync(user, "created", newWork.Id.ToString(), "OrganizationWorks", ip, 1);
            }
            else
            {
                throw new Exception("Помилка обробки дати");
            }
        }

        public async Task<int> UpdateWorkAsync(WorkTableModel model, UserInfo user, string ip)
        {
            UserInfo updatedUser = _userRepository.GetFullUserInfo(model.User);

            if (updatedUser == null)
            {
                throw new Exception("Помилка. Зверніться до адміністратора");
            }

            DateTime date;
            if (DateTime.TryParseExact(model.Date, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                await _workRepository.UpdateOrganizationAsync(new OrganizationalWork
                {
                    Id = model.Id,
                    OrganizationType = await _workRepository.GetWorkTypeByDescriptionAsync(model.OrganizationType),
                    Name = model.Name,
                    Description = model.Description,
                    Date = date,
                });

                return await _logRepository.LogDataAsync(user, "updated", model.Id.ToString(), "OrganizationWorks", ip, 1);
            }
            else
            {
                throw new Exception("Помилка обробки дати");
            }
        }

        public async Task<int> DeleteWorkAsync(int id, int userId, UserInfo user, string ip)
        {
            UserInfo updatedUser = _userRepository.GetFullUserInfo(userId);
            OrganizationalWork work = await _workRepository.GetOrganizationWorkByIdAsync(id);

            if (updatedUser == null || work == null)
            {
                throw new Exception("Помилка. Зверніться до адміністратора");
            }

            await _workRepository.DeleteOrganizationWorkAsync(work, updatedUser);

            return await _logRepository.LogDataAsync(user, "deleted", id.ToString(), "OrganizationWorks", ip, 1);
        }
    }
}

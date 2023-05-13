using BusinessCore.Models;
using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}

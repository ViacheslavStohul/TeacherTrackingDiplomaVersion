using BusinessCore.Models;
using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Services
{
    public class CommissionServise : ICommissionServise
    {
        private readonly IUserRepository _userRepository;

        private readonly ICommissionRepository _commissionRepository;

        private readonly IDepartmentRepository _departmentRepository;

        private readonly ILogRepository _logRepository;

        public CommissionServise(IUserRepository userRepository, ICommissionRepository commissionRepository, IDepartmentRepository departmentRepository, ILogRepository logRepository)
        {
            _userRepository = userRepository;
            _commissionRepository = commissionRepository;
            _departmentRepository = departmentRepository;
            _logRepository = logRepository;
        }

        public async Task<List<CommissionTableModel>>GetCommissionTableAsync()
        {
            List<CommissionTableModel> models = new List<CommissionTableModel>();
            foreach (var item in await _commissionRepository.GetCommissions())
            {
                CommissionTableModel model = new CommissionTableModel();
                model.ToModel(item, await _departmentRepository.GetDepartmentByCommission(item));
                models.Add(model);
            }

            return models;
        }

        public async Task<CommissionChangeResponseModel> GetDataToChangeCommisionPageAsync(int id)
        {
            Commission commission = await _commissionRepository.GetCommisionByIdAsync(id);

            CommissionTableModel commissionTableModel = new CommissionTableModel();

            if (commission != null)
            {
                commissionTableModel.ToFullModel(commission, await _departmentRepository.GetDepartmentByCommission(commission));
            }

            return new CommissionChangeResponseModel
            {
                Model = commissionTableModel,
                Departments = _departmentRepository.GetDepartmentNames().Where(d => d != commissionTableModel.Department).ToList(),
                Users = (await _userRepository.GetUsersAsync()).Select(u => u.SecondName + " " + u.FirstName).Where(u => u != commissionTableModel.Head).ToList()
            };
        }

        public async Task<int> UpdateCommissionAsync(CommissionTableModel model, UserInfo user, string ip)
        {
            Commission commission = await _commissionRepository.GetCommisionByIdAsync(model.Id);

            if(commission == null)
            {
                throw new Exception("Такої коміссії не існує");
            }

            if (!string.IsNullOrWhiteSpace(model.Head))
            {
                await _userRepository.SetCommissionHead(model.Head, commission);
            }
            else if (string.IsNullOrWhiteSpace(model.Head) && commission.Head != null)
            {
                await _userRepository.RemoveCommissionHead(model.Head, commission);
            }

            await _commissionRepository.UpdateCommissionAsync(new Commission
            {
                ComissionId = model.Id,
                Name = model.Name,
                Abbreviation = model.Abbreviation,
            });

            await _departmentRepository.UpdateDepartmentCommission(commission, model.Department);

            return await _logRepository.LogDataAsync(user, "updated", model.Id.ToString(), "Commissions", ip, 1);
        }

        public async Task<int> CreateCommissionAsync(CommissionTableModel model, UserInfo user, string ip)
        {
            Commission commission = await _commissionRepository.CreateCommissionAsync(new Commission
            {
                Name = model.Name,
                Abbreviation = model.Abbreviation,
            });

            if (!string.IsNullOrWhiteSpace(model.Head))
            {
                await _userRepository.SetCommissionHead(model.Head, commission);
            }

            await _departmentRepository.UpdateDepartmentCommission(commission, model.Department);

            return await _logRepository.LogDataAsync(user, "created", commission.ComissionId.ToString(), "Commissions", ip, 1);
        }

        public async Task<int> DeleteCommissionAsync(int id, UserInfo user, string ip)
        {
            Commission toDelete = await _commissionRepository.GetCommisionByIdAsync(id);

            if (toDelete == null)
            {
                throw new Exception("Такої комісії не існує");
            }

            await _commissionRepository.DeleteCommissionAsync(toDelete);

            return await _logRepository.LogDataAsync(user, "deleted", id.ToString(), "Commissions", ip, 1);
        }
    }
}

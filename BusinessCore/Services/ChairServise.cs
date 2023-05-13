using BusinessCore.Models;
using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Services
{
    public class ChairServise : IChairServise
    {
        private readonly IChairRepository _chairRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogRepository _logRepository;
        public ChairServise(IChairRepository chairRepository, IUserRepository userRepository, IDepartmentRepository departmentRepository, ILogRepository logRepository)
        {
            _chairRepository = chairRepository;
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _logRepository = logRepository;
        }

        public async Task<List<ChairTableModel>> GetChairTableAsync()
        {
            List<ChairTableModel> models = new List<ChairTableModel>();
            foreach (var item in await _chairRepository.GetChairsAsync())
            {
                ChairTableModel model = new ChairTableModel();
                model.ToModel(item);
                models.Add(model);
            }

            return models;
        }

        public async Task<ChairChangeResponseModel> GetDataToChangeCommisionPageAsync(int id)
        {
            Chair chair = await _chairRepository.GetChairByIdAsync(id);

            ChairTableModel tableModel = new ChairTableModel();

            if (chair != null)
            {
                tableModel.ToFullModel(chair);
            }

            return new ChairChangeResponseModel
            {
                Model = tableModel,
                Departments = _departmentRepository.GetDepartmentNames().Where(d => d != tableModel.Department).ToList(),
                Users = (await _userRepository.GetUsersAsync()).Select(u => u.SecondName + " " + u.FirstName).Where(u => u != tableModel.Head).ToList()
            };
        }

        public async Task<int> AddChairAsync(ChairTableModel model, UserInfo user, string ip)
        {
            Chair chair = await _chairRepository.CreateChairAsync(new Chair
            {
                Name = model.Name,
                Abbreviation = model.Abbreviation,
                Department = await _departmentRepository.GetDepartmentByNameAsync(model.Department)
            });

            if (!string.IsNullOrWhiteSpace(model.Head))
            {
                await _userRepository.SetChairHead(model.Head, chair);
            }

            return await _logRepository.LogDataAsync(user, "created", chair.ChairId.ToString(), "Chairs", ip, 1);
        }

        public async Task<int> DeleteChairAsync(int id, UserInfo user, string ip)
        {
            Chair toDelete = await _chairRepository.GetChairByIdAsync(id);

            await _userRepository.DeleteAllUsersFromChairAsync(toDelete);

            await _departmentRepository.DeleteChairFromDepartmentAsync(toDelete);

            await _chairRepository.DeleteChairAsync(toDelete);

            return await _logRepository.LogDataAsync(user, "deleted", id.ToString(), "Chairs", ip, 1);
        }

        public async Task<int> UpdateChairAsync(ChairTableModel model, UserInfo user, string ip)
        {
            Chair toUpdate = await _chairRepository.GetChairByIdAsync(model.Id);

            if (toUpdate == null)
            {
                throw new Exception("Такої кафедри не існує");
            }

            if (!string.IsNullOrWhiteSpace(model.Head))
            {
                await _userRepository.SetChairHead(model.Head, toUpdate);
            }
            else if (string.IsNullOrWhiteSpace(model.Head) && toUpdate.Head != null)
            {
                await _userRepository.RemoveChairHead(null, toUpdate);
            }

            await _chairRepository.ChangeChairAsync(new Chair
            {
                ChairId = model.Id,
                Name = model.Name,
                Abbreviation = model.Abbreviation,
                Department = await _departmentRepository.GetDepartmentByNameAsync(model.Department),
            });

            return await _logRepository.LogDataAsync(user, "updated", model.Id.ToString(), "Chairs", ip, 1);
        }
    }
}

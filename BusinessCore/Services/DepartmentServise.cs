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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessCore.Services
{
    public class DepartmentServise : IDepartmentServise
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogRepository _logRepository;

        public DepartmentServise(IDepartmentRepository department, IUserRepository user, ILogRepository log)
        {
            _departmentRepository = department;
            _userRepository = user;
            _logRepository = log;
        }

        public async Task<List<DepartmentTableModel>> GetDapartmentTableAsync()
        {
            List<DepartmentTableModel> models = new List<DepartmentTableModel>();

            foreach (Department item in await _departmentRepository.GetDepartmentsAsync())
            {
                DepartmentTableModel model = new DepartmentTableModel();
                model.ToModel(item);

                models.Add(model);
            }

            return models;
        }

        public async Task<DepartmentChangeResponseModel> GetDataToChangeDepartmentPageAsync(int id)
        {
            Department department = await  _departmentRepository.GetDepartmentByIdAsync(id);

            DepartmentTableModel departmentTableModel = new DepartmentTableModel();

            if (department != null)
            {
                departmentTableModel.ToModel(department);
            }

            return new DepartmentChangeResponseModel
            {
                Model = departmentTableModel,
                Users = (await _userRepository.GetUsersAsync()).Select(u => u.SecondName + " " + u.FirstName).Where(u => u != departmentTableModel.Head).ToList()
            };
        }

        public async Task<int> CreateDepartmentAsync(DepartmentTableModel model, UserInfo user, string ip)
        {
            Department newDepartment = await _departmentRepository.AddDepartmentAsync(new Department
            {
                Name = model.Name,
                Abbreviatoin = model.Abbreviation,
                Head = await _userRepository.GetUserByNameAndSurnameAsync(model.Head)
            });

            return await _logRepository.LogDataAsync(user, "created", newDepartment.DepartamentId.ToString(), "Departments", ip, 1);
        }

        public async Task<int> UpdateDepartmentAsync(DepartmentTableModel model, UserInfo user, string ip)
        {
            Department toUpdate = await _departmentRepository.GetDepartmentByIdAsync(model.Id);

            toUpdate.Name = model.Name;
            toUpdate.Abbreviatoin = model.Abbreviation;
            toUpdate.Head = await _userRepository.GetUserByNameAndSurnameAsync(model.Head);

            await _departmentRepository.UpdateDepartmentAsync(toUpdate);

            return await _logRepository.LogDataAsync(user, "updated", toUpdate.DepartamentId.ToString(), "Departments", ip, 1);
        }

        public async Task<int> DeleteDepartmentAsync(int id, UserInfo user, string ip)
        {
            await _departmentRepository.DeleteDepartmentAsync(await _departmentRepository.GetDepartmentByIdAsync(id));

            return await _logRepository.LogDataAsync(user, "deleted", id.ToString(), "Departments", ip, 1);
        }
    }
}

using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class UserTableModel : IUserValidation
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string WorkType { get; set; }

        [Required]
        public string Rank { get; set; }

        [Required]
        public string Chair { get; set; }

        [Required]
        public string Comission { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string AccessLevel { get; set; }

        public DateTime? DeletionDate { get; set; }

        public int WorksAmmount { get; set; }


        public void ToModel(UserInfo item, string department)
        {
            this.Id = item.IdUserInfo;
            this.FirstName = item.FirstName;
            this.SecondName = item.SecondName;
            this.MiddleName = item.MiddleName;
            this.Phone = item.Phone;
            this.Email = item.Email;
            this.WorkType = item?.WorkType?.Abbreviation;
            this.Rank = item?.Rank?.Abbreviation;
            this.Chair = item?.Chair?.Abbreviation;
            this.Comission = item?.Commission?.Abbreviation;
            this.Department = department;
            this.AccessLevel = item.AccessLevel.Name;
            this.DeletionDate = item?.DeletionDate;
            this.WorksAmmount = item.WorkAmmount;
        }

        public void ToFullModel(UserInfo item, string department)
        {
            this.Id = item.IdUserInfo;
            this.FirstName = item.FirstName;
            this.SecondName = item.SecondName;
            this.MiddleName = item.MiddleName;
            this.Phone = item.Phone;
            this.Email = item.Email;
            this.WorkType = item?.WorkType?.Name;
            this.Rank = item?.Rank?.Name;
            this.Chair = item?.Chair?.Name;
            this.Comission = item?.Commission?.Name;
            this.Department = department;
            this.AccessLevel = item.AccessLevel.Name;
            this.DeletionDate = item?.DeletionDate;
            this.WorksAmmount = item.WorkAmmount;
        }
    }
}

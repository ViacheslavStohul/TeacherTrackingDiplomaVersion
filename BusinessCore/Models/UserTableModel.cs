using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class UserTableModel : IUserValidation
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string WorkType { get; set; }

        public string Rank { get; set; }

        public string Chair { get; set; }

        public string Comission { get; set; }

        public string Department { get; set; }

        public string AccessLevel { get; set; }

        public DateTime? DeletionDate { get; set; }


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
            this.Comission = item?.Comission?.Abbreviation;
            this.Department = department;
            this.AccessLevel = item.AccessLevel.Name;
            this.DeletionDate = item?.DeletionDate;
        }
    }
}

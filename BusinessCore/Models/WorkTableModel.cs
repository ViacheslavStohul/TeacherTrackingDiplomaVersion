using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class WorkTableModel
    {
        public int Id { get; set; }

        public string OrganizationType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string User { get; set; }

        public void ToModel(OrganizationalWork organizationalWork, UserInfo user)
        {
            this.Id = organizationalWork.Id;
            this.OrganizationType = Enum.GetName(typeof(OrganizationType), organizationalWork.OrganizationType);
            this.Name = organizationalWork.Name;
            this.Description = organizationalWork.Description;
            this.Date = organizationalWork.Date;
            this.User = user.SecondName + " " + user.FirstName;
        }
    }
}

using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class WorkTableModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string OrganizationType { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public int User { get; set; }

        public void ToModel(OrganizationalWork organizationalWork, UserInfo user)
        {
            this.Id = organizationalWork.Id;
            this.OrganizationType = organizationalWork.OrganizationType.Description;
            this.Name = organizationalWork.Name;
            this.Description = organizationalWork.Description;
            this.Date = organizationalWork.Date.Year.ToString();
            this.User = user.IdUserInfo;
        }
    }
}

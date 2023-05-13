using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class DepartmentTableModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        public string Head { get; set; }

        public void ToModel(Department entity)
        {
            this.Id = entity.DepartamentId;
            this.Name = entity.Name;
            this.Abbreviation = entity.Abbreviatoin;
            this.Head = entity?.Head?.SecondName + " " + entity?.Head?.FirstName;
        }
    }
}

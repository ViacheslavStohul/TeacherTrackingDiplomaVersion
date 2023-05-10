using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class CommissionTableModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        [Required]
        public string Department { get; set; }

        public string Head { get; set; }

        public void ToModel(Commission commission, Department department)
        {
            this.Id = commission.ComissionId;
            this.Name = commission.Name;
            this.Abbreviation = commission.Abbreviation;
            this.Department = department.Abbreviatoin;
            this.Head = commission?.Head?.SecondName + " " + commission?.Head?.FirstName;
        }

        public void ToFullModel(Commission commission, Department department)
        {
            this.Id = commission.ComissionId;
            this.Name = commission.Name;
            this.Abbreviation = commission.Abbreviation;
            this.Department = department.Name;
            this.Head = commission?.Head?.SecondName + " " + commission?.Head?.FirstName;
        }
    }
}

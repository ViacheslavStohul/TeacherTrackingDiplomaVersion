using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Models
{
    public class ChairTableModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        [Required]
        public string Department { get; set; }

        public string Head { get; set; }

        public void ToModel(Chair entity)
        {
            this.Id = entity.ChairId;
            this.Name = entity.Name;
            this.Abbreviation = entity.Abbreviation;
            this.Department = entity?.Department?.Abbreviatoin;
            this.Head = entity?.Head?.SecondName + " " + entity?.Head?.FirstName;
        }

        public void ToFullModel(Chair entity)
        {
            this.Id = entity.ChairId;
            this.Name = entity.Name;
            this.Abbreviation = entity.Abbreviation;
            this.Department = entity?.Department?.Name;
            this.Head = entity?.Head?.SecondName + " " + entity?.Head?.FirstName;
        }
    }
}

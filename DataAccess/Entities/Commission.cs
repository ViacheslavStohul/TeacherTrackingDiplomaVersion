using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Commission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComissionId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Abbreviation { get; set; }

        public Department? Departament { get; set; }
    }
}

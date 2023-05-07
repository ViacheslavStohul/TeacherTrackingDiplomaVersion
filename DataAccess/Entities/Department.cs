using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartamentId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Abbreviatoin { get; set; }

        public ICollection<Commission> Comissions { get; set; }

        public Chair Chair { get; set; }

        public UserInfo? Head { get; set; }
    }
}

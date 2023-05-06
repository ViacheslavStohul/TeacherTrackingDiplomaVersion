using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class AccessLevel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public bool User { get; set; }

        public bool Chair { get; set; }

        public bool Comission { get; set; }

        public bool Departament { get; set; }
    }
}

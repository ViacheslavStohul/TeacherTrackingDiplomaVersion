using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class OrganizationalWork
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public OrganizationType OrganizationType { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}

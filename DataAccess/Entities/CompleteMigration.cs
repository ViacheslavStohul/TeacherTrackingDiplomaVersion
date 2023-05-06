using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("_CompleteMigrations")]
    public class CompleteMigration
    {
        [Key]
        public string CompleteMigrationId { get; set; }
    }
}

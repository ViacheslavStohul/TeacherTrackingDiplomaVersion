using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLog { get; set; }

        public UserInfo? User { get; set; }

        [MaxLength(30)]
        public string Action { get; set; }

        [MaxLength(30)]
        public string? Target { get; set; }

        [MaxLength(25)]
        public string? ObjectTable { get; set; }

        [MaxLength(20)]
        public string Ip { get; set; }

        public int Result { get; set; }

        public DateTime Time { get; set; }
    }
}

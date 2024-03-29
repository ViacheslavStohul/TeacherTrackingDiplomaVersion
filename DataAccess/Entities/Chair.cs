﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Chair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChairId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Abbreviation { get; set; }

        [NotMapped]
        public Department Department { get; set; }

        [NotMapped]
        public UserInfo Head { get; set; }
    }
}

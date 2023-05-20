using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUserInfo { get; set; }

        public User? User { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string SecondName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(70)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        public Chair? Chair { get; set; }

        public Commission? Commission { get; set; }

        public WorkType? WorkType { get; set; }

        public ICollection<OrganizationalWork> OrganizationalWorks { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public Rank? Rank { get; set; }

        public DateTime? DeletionDate { get; set; }

        [NotMapped]
        public int WorkAmmount
        {
            get
            {
                try
                {
                    return this.OrganizationalWorks.Count;
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}

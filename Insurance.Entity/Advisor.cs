using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Insurance.Entity
{
    [Table("Advisor")]
    public class Advisor: IInsurance
    {
        public Advisor() 
        {
            System.Random random = new System.Random();
            var randaom = random.Next(1, 100);
            HealthStatus = randaom > 30 ? "Green" : "Red";
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(256)")]
        public string LastName { get; set; }


        [Column(TypeName = "nvarchar(256)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string HealthStatus { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }

    }
}

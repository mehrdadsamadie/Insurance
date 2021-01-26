using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Insurance.Entity
{
    [Table("MGA")]
   public class MGA: IInsurance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string BusinessName { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string BusinessAddress { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string BusinessPhoneNumber { get; set; }
        public virtual ICollection<FirstContractor> FirstContractors { get; set; }
        public virtual ICollection<SecondContractor> SecondContractors { get; set; }
    }
}

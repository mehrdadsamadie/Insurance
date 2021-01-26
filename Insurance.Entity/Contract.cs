using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Insurance.Entity
{
    [Table("Contract")]
     public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FirstContractorId { get; set; }
        public int SecondContractorId { get; set; }
        public FirstContractor FirstContractor { get; set; }
        public SecondContractor SecondContractor { get; set; }
    }
}

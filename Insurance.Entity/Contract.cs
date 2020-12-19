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

        [ForeignKey("FK_Contract_Advisor")]
        public int? AdvisorId { get; set; }

        [ForeignKey("FK_Contract_Carrier")]
        public int? CarrierId { get; set; }

        [ForeignKey("FK_Contract_MGA")]
        public int? MGAId { get; set; }
        public virtual MGA MGA { get; set; }
        public virtual Carrier Carrier { get; set; }
        public virtual Advisor Advisor { get; set; }
    }
}

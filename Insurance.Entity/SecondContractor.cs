using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Insurance.Entity
{
    [Table("SecondContractor")]
    public class SecondContractor: IContractor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? AdvisorId { get; set; }
        public int? CarrierId { get; set; }
        public int? MGAId { get; set; }
        public virtual MGA MGA { get; set; }
        public virtual Carrier Carrier { get; set; }
        public virtual Advisor Advisor { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Entity
{
    public class Contractor : IContractor
    {
        public int? AdvisorId { get ; set ; }
        public int? CarrierId { get; set; }
        public int? MGAId { get; set ; }
    }
}

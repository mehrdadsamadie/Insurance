using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Web.Model
{
    public class AdvisorView 
    {

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Phone Number")]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        [DisplayName("Health Status")]
        public string HealthStatus { get; set; }
    }
    public class AdvisorList 
    {
        public AdvisorList()
        {
            List = new List<AdvisorView>();
        }
        public int Total { get; set; }
        public List<AdvisorView> List { get; set; }
    }
}

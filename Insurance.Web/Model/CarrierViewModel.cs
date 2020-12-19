using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Web.Model
{

    public class CarrierView
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Business Name")]
        public string BusinessName { get; set; }
        [DisplayName("Business Address")]
        public string BusinessAddress { get; set; }
        [DisplayName("Business PhoneNumber")]
        public string BusinessPhoneNumber { get; set; }
    }
    public class CarrierList
    {
        public CarrierList() 
        {
            List = new List<CarrierView>();
        }
        public int Total { get; set; }
        public List<CarrierView> List { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Web.Model
{
    public class MGAViewModel
    {
        public class MGAView : CarrierView
        {
        }
        public class MGAList
        {
            public MGAList()
            {
                List = new List<MGAView>();
            }
            public int Total { get; set; }
            public List<MGAView> List { get; set; }
        }
    }
}

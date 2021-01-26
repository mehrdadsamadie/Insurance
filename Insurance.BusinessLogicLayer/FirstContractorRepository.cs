using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.BusinessLogicLayer
{
   public class FirstContractorRepository : DataRepository<FirstContractor>, IFirstContractorRepository
    {
        public FirstContractorRepository(InsuranceContext insuranceContext) : base(insuranceContext) { }
    }
}

using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.BusinessLogicLayer
{
    class SecondContractorRepository : DataRepository<SecondContractor>, ISecondContractorRepository
    {
        public SecondContractorRepository(InsuranceContext insuranceContext) : base(insuranceContext) { }
    }
}

using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.BusinessLogicLayer
{
    public class AdvisorRepository : DataRepository<Advisor>, IAdvisorRepository
    {
        public AdvisorRepository(InsuranceContext insuranceContext):base(insuranceContext) { }
    }
}

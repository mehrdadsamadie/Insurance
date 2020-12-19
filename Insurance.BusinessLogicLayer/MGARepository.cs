using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.BusinessLogicLayer
{
    public class MGARepository:DataRepository<MGA>, IMGARepository
    {
        public MGARepository(InsuranceContext insuranceContext) : base(insuranceContext) { }
    }
}

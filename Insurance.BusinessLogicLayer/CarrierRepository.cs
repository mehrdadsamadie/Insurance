using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.BusinessLogicLayer
{
    public class CarrierRepository:DataRepository<Carrier>,ICarrierRepository
    {
        public CarrierRepository(InsuranceContext insuranceContext) : base(insuranceContext) { }
    }
}

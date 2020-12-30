using Insurance.BusinessLogicLayer;
using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Service
{
    public class CarrierService : DataService<Carrier>,ICarrierService
    {
        private readonly ICarrierRepository iCarrierRepository;
        public CarrierService(ICarrierRepository iCarrierRepository) : base(iCarrierRepository)
        {
            this.iCarrierRepository = iCarrierRepository;
        }
    }
}

using Insurance.BusinessLogicLayer;
using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Insurance.Service
{
    public class CarrierService : DataService<Carrier>,ICarrierService
    {
        private readonly ICarrierRepository iCarrierRepository;

        private readonly IContractRepository IContractRepository;
        public CarrierService(ICarrierRepository iCarrierRepository, IContractRepository iContractRepository) : base(iCarrierRepository)
        {
            this.iCarrierRepository = iCarrierRepository;
            this.IContractRepository = iContractRepository;
        }
        public override void DeleteWithSaveChange(Carrier carrier)
        {

            try
            {
                using (var scope = new TransactionScope())
                {

                    if (carrier != null)
                    {
                        var contractor = new Contractor()
                        {
                            AdvisorId = null,
                            CarrierId = carrier.Id,
                            MGAId = null
                        };
                        this.IContractRepository.DeleteWithContactor(contractor);
                        this.iCarrierRepository.Delete(carrier);
                        this.IContractRepository.SaveChanges();
                        this.IContractRepository.SaveChanges();
                        scope.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }


        }
    }
}

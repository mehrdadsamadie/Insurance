using Insurance.BusinessLogicLayer;
using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Insurance.Service
{
    public class MGAService : DataService<MGA>, IMGAService
    {
        private readonly IMGARepository iMGARepository;
        private readonly IContractRepository iContractRepository;
        public MGAService(IMGARepository iMGARepository, IContractRepository iContractRepository) :base(iMGARepository)
        {
            this.iMGARepository = iMGARepository;
            this.iContractRepository = iContractRepository;
        }
        public override void DeleteWithSaveChange(MGA mga)
        {

            try
            {
                using (var scope = new TransactionScope())
                {

                    if (mga != null)
                    {
                        var contractor = new Contractor()
                        {
                            AdvisorId = null,
                            CarrierId = null,
                            MGAId = mga.Id
                        };
                        this.iContractRepository.DeleteWithContactor(contractor);
                        this.iMGARepository.Delete(mga);
                        this.iContractRepository.SaveChanges();
                        this.iMGARepository.SaveChanges();
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

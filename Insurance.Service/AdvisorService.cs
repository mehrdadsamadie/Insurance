using Insurance.BusinessLogicLayer;
using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Transactions;

namespace Insurance.Service
{
    public class AdvisorService : DataService<Advisor>, IAdvisorService
    {
        private readonly IAdvisorRepository iAdvisorRepository;
        private readonly IContractRepository iContractRepository;
        public AdvisorService(IAdvisorRepository iAdvisorRepository, IContractRepository iContractRepository) : base(iAdvisorRepository)
        {
            this.iAdvisorRepository = iAdvisorRepository;
            this.iContractRepository = iContractRepository;

        }
        public override void DeleteWithSaveChange(Advisor advisor)
        {

            try
            {
                using (var scope = new TransactionScope())
                {
                    
                    if (advisor != null)
                    {
                        var contractor = new Contractor()
                        {
                            AdvisorId = advisor.Id,
                            CarrierId = null,
                            MGAId = null
                        };
                        this.iContractRepository.DeleteWithContactor(contractor);
                        this.iAdvisorRepository.Delete(advisor);
                       this.iContractRepository.SaveChanges();
                        this.iAdvisorRepository.SaveChanges();
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

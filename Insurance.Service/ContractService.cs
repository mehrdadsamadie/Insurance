using Insurance.BusinessLogicLayer;
using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Service
{
    public class ContractService: DataService<Contract>, IContractService
    {
        private readonly IContractRepository iContractRepository;
        public ContractService(IContractRepository iContractRepository) :base(iContractRepository)
        {
            this.iContractRepository = iContractRepository;
        }

        public List<Contract> GetIndirect(Contract model)
        {
            return iContractRepository.GetIndirect(model);
        }
    }
}

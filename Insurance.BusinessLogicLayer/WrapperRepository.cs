using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.BusinessLogicLayer
{
    public class WrapperRepository : IWrapperRepository
    {
        private InsuranceContext _insuranceContext;
        private IAdvisorRepository _advisor;
        private ICarrierRepository _carrier;
        private IContractRepository _contract;
        private IMGARepository _MGA;
        public IAdvisorRepository Advisor {
            get
            {
                if (_advisor == null)
                {
                    _advisor = new AdvisorRepository(_insuranceContext);
                }

                return _advisor;
            }
        }

        public ICarrierRepository Carrier 
        {
            get
            {
                if (_carrier == null)
                {
                    _carrier = new CarrierRepository(_insuranceContext);
                }

                return _carrier;
            }
        }

        public IContractRepository Contract
        {
            get
            {
                if (_contract == null)
                {
                    _contract = new ContractRepository(_insuranceContext);
                }

                return _contract;
            }
        }

        public IMGARepository MGA
        {
            get
            {
                if (_MGA == null)
                {
                    _MGA = new MGARepository(_insuranceContext);
                }

                return _MGA;
            }
        }
        public WrapperRepository(InsuranceContext insuranceContext) 
        {
            _insuranceContext = insuranceContext;
        }
        public void Save()
        {
            _insuranceContext.SaveChanges();
        }
    }
}

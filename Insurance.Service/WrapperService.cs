using Insurance.DataAccessLayer;
using Insurance.Entity;
using Insurance.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Service
{
    public class WrapperService : IWrapperService
    {

        public IAdvisorService Advisor { get; set; }
        public ICarrierService Carrier { get; set; }
        public IContractService Contract { get; set; }
        public IMGAService MGA { get; set; }
        public WrapperService(IAdvisorService advisor, ICarrierService carrier, IContractService contract, IMGAService mGA)
        {
            this.Advisor = advisor;
            this.Carrier = carrier;
            this.Contract = contract;
            this.MGA = mGA;
        }

        //public IAdvisorService Advisor
        //{
        //    get
        //    {
        //        if (_advisor == null)
        //        {
        //            _advisor = new AdvisorService(IAdvisorService);
        //        }

        //        return _advisor;
        //    }
      //  }

        //public ICarrierService Carrier 
        //{
        //    get
        //    {
        //        if (_carrier == null)
        //        {
        //            _carrier = new CarrierRepository(_insuranceContext);
        //        }

        //        return _carrier;
        //    }
        //}

        //public IContractService Contract
        //{
        //    get
        //    {
        //        if (_contract == null)
        //        {
        //            _contract = new ContractRepository(_insuranceContext);
        //        }

        //        return _contract;
        //    }
        //}

        //public IMGAService MGA
        //{
        //    get
        //    {
        //        if (_MGA == null)
        //        {
        //            _MGA = new MGARepository();
        //        }

        //        return _MGA;
        //    }
        //}
    }
}

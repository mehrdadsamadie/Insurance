using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.BusinessLogicLayer
{
   public interface IWrapperRepository
    {
        IAdvisorRepository Advisor { get; }
        ICarrierRepository Carrier { get; }
        IContractRepository Contract { get; }
        IMGARepository MGA { get; }
        void Save();
    }
}

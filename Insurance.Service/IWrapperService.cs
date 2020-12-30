using Insurance.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Service
{
   public interface IWrapperService
    {
        IAdvisorService Advisor { get; set; }
        ICarrierService Carrier { get; }
        IContractService Contract { get; }
        IMGAService MGA { get; }
    }
}

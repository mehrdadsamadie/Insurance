using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Service
{
    public interface IContractService : IDataService<Contract>
    {
        public List<Contractor> GetShortestPath(Contractor source, Contractor destination);
        public Contract FindByContractor(IContractor firstContractor, IContractor secondContractor);

    }
}

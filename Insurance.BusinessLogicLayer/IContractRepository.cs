using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.BusinessLogicLayer
{
    public interface IContractRepository:IDataRepository<Contract>
    {
        public List<Contract> GetIndirect (Contract model);
    }
}

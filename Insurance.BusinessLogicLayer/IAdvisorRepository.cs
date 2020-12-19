using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.BusinessLogicLayer
{
    public interface IAdvisorRepository: IDataRepository<Advisor>
    {
    }
}

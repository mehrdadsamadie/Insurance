using Insurance.BusinessLogicLayer;
using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Service
{
    public class MGAService : DataService<MGA>, IMGAService
    {
        private readonly IMGARepository iMGARepository;
        public MGAService(IMGARepository iMGARepository):base(iMGARepository)
        {
            this.iMGARepository = iMGARepository;
        }
    }
}

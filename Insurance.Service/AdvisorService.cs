using Insurance.BusinessLogicLayer;
using Insurance.DataAccessLayer;
using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Service
{
    public class AdvisorService : DataService<Advisor>, IAdvisorService
    {
        private readonly IAdvisorRepository iAdvisorRepository;
        public AdvisorService(IAdvisorRepository iAdvisorRepository) : base(iAdvisorRepository)
        {
            this.iAdvisorRepository = iAdvisorRepository;
        }
    }
}

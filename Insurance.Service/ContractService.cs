using Insurance.BusinessLogicLayer;
using Insurance.DataAccessLayer;
using Insurance.Entity;
using Insurance.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insurance.Service
{
    public class ContractService : DataService<Contract>, IContractService
    {
        private readonly IContractRepository iContractRepository;

        public ContractService(IContractRepository iContractRepository) : base(iContractRepository)
        {
            this.iContractRepository = iContractRepository;

        }

        public List<Contractor> GetShortestPath(Contractor source, Contractor destination)
        {
            var allcontract = this.FindByCondition(x=>x.Id!=null).ToList();
            var graph = new Graph<Contractor>();
            foreach (var item in allcontract)
            {
                var newfirstcontractor = new Contractor() { AdvisorId = item.FirstContractor.AdvisorId, CarrierId = item.FirstContractor.CarrierId, MGAId = item.FirstContractor.MGAId };
                var newsecondcontractor = new Contractor() { AdvisorId = item.SecondContractor.AdvisorId, CarrierId = item.SecondContractor.CarrierId, MGAId = item.SecondContractor.MGAId };
                graph.AddEdge(newfirstcontractor, newsecondcontractor);
            }
            var stack = graph.ShortestPath(source, destination);

            return stack.ToList();

        }

    }
}

using Insurance.DataAccessLayer;
using Insurance.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Insurance.BusinessLogicLayer
{
    public class ContractRepository : DataRepository<Contract>,IContractRepository
    {
        public ContractRepository(InsuranceContext insuranceContext) : base(insuranceContext) { }
        public override Contract Create(Contract entity)
        {
            Verify(entity);
            base.Create(entity);
            return entity;
        }
        public override IQueryable<Contract> FindByCondition(Expression<Func<Contract, bool>> expression)
        {
            return this.insuranceContext.Set<Contract>().Where(expression).
                Include(x => x.SecondContractor).Include(x => x.FirstContractor).AsNoTracking();
        }
        public Contract FindByContractor(IContractor firstContractor,IContractor secondContractor) 
        {
            var _contract = base.FindByCondition(x =>
          (x.FirstContractor.AdvisorId == firstContractor.AdvisorId
           && x.FirstContractor.CarrierId == firstContractor.CarrierId
           && x.FirstContractor.MGAId == firstContractor.MGAId
           && x.SecondContractor.AdvisorId == secondContractor.AdvisorId
           && x.SecondContractor.CarrierId == secondContractor.CarrierId
           && x.SecondContractor.MGAId == secondContractor.MGAId) ||
           (
           x.SecondContractor.AdvisorId == firstContractor.AdvisorId
           && x.SecondContractor.CarrierId == firstContractor.CarrierId
           && x.SecondContractor.MGAId == firstContractor.MGAId
           && x.FirstContractor.AdvisorId == secondContractor.AdvisorId
           && x.FirstContractor.CarrierId == secondContractor.CarrierId
           && x.FirstContractor.MGAId == secondContractor.MGAId
           ));
            return _contract.FirstOrDefault();
        }
        protected void Verify(Contract entity)
        {
            if(entity.FirstContractor.CarrierId== entity.SecondContractor.CarrierId && entity.FirstContractor.MGAId == entity.SecondContractor.MGAId && entity.FirstContractor.AdvisorId == entity.SecondContractor.AdvisorId)
            {
                throw (new Exception(message: "contract is between same entity"));
            }
            var _contract = FindByContractor(entity.FirstContractor, entity.SecondContractor);
        
            if (_contract!=null)
            {
                throw new Exception(message: "another contract exist");
            }
        }
        public void DeleteWithContactor(IContractor contactor)
        {
            var contracts = this.FindByCondition(x =>
              x.SecondContractor.AdvisorId == contactor.AdvisorId && x.SecondContractor.CarrierId == contactor.CarrierId && x.SecondContractor.MGAId == contactor.MGAId ||
                          x.FirstContractor.AdvisorId == contactor.AdvisorId && x.FirstContractor.CarrierId == contactor.CarrierId && x.FirstContractor.MGAId == contactor.MGAId
             ).ToList();
            foreach (var item in contracts.ToList())
            {
                this.Delete(item);

                this.insuranceContext.FirstContractor.RemoveRange(item.FirstContractor);
                this.insuranceContext.secondContractors.RemoveRange(item.SecondContractor);


            }

        }
     
    }
}

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
             );
            foreach (var item in contracts)
            {
                this.Delete(item);
            //    this.insuranceContext.FirstContractor.Attach(item.FirstContractor);
            //    this.insuranceContext.secondContractors.Attach(item.SecondContractor);
                this.insuranceContext.FirstContractor.RemoveRange(item.FirstContractor);
                this.insuranceContext.secondContractors.RemoveRange(item.SecondContractor);
              //  this.SaveChanges();

            }

        }
        //public List<Contract> GetIndirect(Contract model)
        //{
        //    if (model.MGAId != null && model.CarrierId != null && model.AdvisorId != null)
        //    {
        //        throw (new Exception(message: "Contract is between two Entity"));
        //    }
        //    var _carrierlist = new List<Contract>();
        //    var _advisorlist = new List<Contract>();
        //    var _MGAlist = new List<Contract>();
        //    var result = new List<Contract>();
        //    if (model.AdvisorId != null)
        //    {
        //        _advisorlist = base.FindByCondition(x => x.AdvisorId == model.AdvisorId.Value).Include(x => x.Advisor).Include(x => x.Carrier).Include(x => x.MGA).ToList();
        //    }
        //    if (model.CarrierId != null)
        //    {
        //        _carrierlist = base.FindByCondition(x => x.CarrierId == model.CarrierId.Value).Include(x => x.Advisor).Include(x => x.Carrier).Include(x => x.MGA).ToList();
        //    }
        //    if (model.MGAId != null)
        //    {
        //        _MGAlist = base.FindByCondition(x => x.MGAId == model.MGAId.Value).Include(x => x.Advisor).Include(x => x.Carrier).Include(x => x.MGA).ToList();
        //    }



        //    if (model.AdvisorId == null)
        //    {

        //        var _temp = from p in _carrierlist
        //                    join pm in _MGAlist on p.AdvisorId equals pm.AdvisorId
        //                    select new Contract { Advisor = p.Advisor, AdvisorId = p.AdvisorId, Carrier = p.Carrier, CarrierId = p.CarrierId, MGA = pm.MGA, MGAId = pm.MGAId };
        //        result.AddRange(_temp);
        //    }
        //    if (model.CarrierId == null)
        //    {
        //     var _temp=   from p in _advisorlist
        //        join pm in _MGAlist on p.CarrierId equals pm.CarrierId
        //        select new Contract {  Advisor = p.Advisor,AdvisorId=p.AdvisorId,Carrier=p.Carrier,CarrierId=p.CarrierId,MGA=pm.MGA, MGAId = pm.MGAId };
        //        result.AddRange(_temp);
        //    }
        //    if (model.MGAId == null)
        //    {
        //        var _temp = from p in _carrierlist
        //                    join pm in _advisorlist on p.MGAId equals pm.MGAId
        //                    select new Contract { Advisor = pm.Advisor, AdvisorId = pm.AdvisorId, Carrier = p.Carrier, CarrierId = p.CarrierId, MGA = p.MGA, MGAId = p.MGAId };
        //        result.AddRange(_temp);
        //    }
        //    return (result);

        //}
    }
}

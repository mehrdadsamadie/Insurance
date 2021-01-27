using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurance.BusinessLogicLayer;
using Insurance.Entity;
using Insurance.Service;
using Insurance.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContarctController : ControllerBase
    {
        private readonly IContractService _serContract;
        private readonly IAdvisorService _serAdvisor;
        private readonly IMGAService _serMGA;
        private readonly ICarrierService _serCarrier;
        public ContarctController(IContractService serContract, IAdvisorService serAdvisor, IMGAService serMGA, ICarrierService serCarrier)
        {
            _serContract = serContract;
            _serAdvisor = serAdvisor;
            _serCarrier = serCarrier;
            _serMGA = serMGA;

        }


        [HttpGet("{firstCarrierId?}/{firstAdvisorId?}/{firstMgaId?}/{secondCarrierId?}/{secondAdvisorId?}/{secondMgaId?}")]
        [Produces("application/json")]
        public IActionResult GET([FromQuery]string firstCarrierId, [FromQuery]string firstAdvisorId, [FromQuery]string firstMgaId, [FromQuery]string secondCarrierId, [FromQuery]string secondAdvisorId, [FromQuery]string secondMgaId)
        {

            try
           {
                var model = new ContractCreate()
                {
                    FirstContractor = new Contractor()
                    {
                        CarrierId = (string.IsNullOrEmpty(firstCarrierId) || firstCarrierId.ToLower() == "null") ? (int?)null : int.Parse(firstCarrierId),
                        AdvisorId = (string.IsNullOrEmpty(firstAdvisorId) || firstAdvisorId.ToLower() == "null") ? (int?)null : int.Parse(firstAdvisorId),
                        MGAId = (string.IsNullOrEmpty(firstMgaId) || firstMgaId.ToLower() == "null") ? (int?)null : int.Parse(firstMgaId)
                    },
                    SecondContractor = new Contractor()
                    {
                        CarrierId = (string.IsNullOrEmpty(secondCarrierId) || secondCarrierId.ToLower() == "null") ? (int?)null : int.Parse(secondCarrierId),
                        AdvisorId = (string.IsNullOrEmpty(secondAdvisorId) || secondAdvisorId.ToLower() == "null") ? (int?)null : int.Parse(secondAdvisorId),
                        MGAId = (string.IsNullOrEmpty(secondMgaId) || secondMgaId.ToLower() == "null") ? (int?)null : int.Parse(secondMgaId)
                    }
                };

                if (!TryValidateModel(model))
                {
                    return BadRequest("Invalid model object");
                }
                 var _path=_serContract.GetShortestPath(model.FirstContractor,model.SecondContractor);
            var result = new ContractList();
            if (_path.Count > 0)
            {
                if (_path.Count == 2)
                {
                    var _contract = _serContract.FindByContractor(_path[0],_path[1]);
                    result.ContractId = _contract.Id;
                }
                foreach(var item in _path) 
                {
                    var _contractor = new ContractorResult();
                    if (item.AdvisorId != null) 
                    {
                        var _advisor = _serAdvisor.FindByCondition(x => x.Id == item.AdvisorId.Value).FirstOrDefault();
                        if (_advisor != null) {
                            _contractor.AdvisorId = _advisor.Id;
                            _contractor.AdvisorFullName= _advisor.FirstName + " " + _advisor.LastName;
                                }
                    }
                    else if(item.CarrierId!=null)
                    {
                        var _carrier = _serCarrier.FindByCondition(x => x.Id == item.CarrierId.Value).FirstOrDefault();
                        if (_carrier != null)
                        {
                            _contractor.CarrierId = _carrier.Id;
                            _contractor.CarrierBusinessName = _carrier.BusinessName;
                        }
                    }
                    else if (item.MGAId != null) 
                    {
                        var _mga = _serMGA.FindByCondition(x => x.Id == item.MGAId.Value).FirstOrDefault();
                        if (_mga != null)
                        {
                            _contractor.MGAId = _mga.Id;
                            _contractor.MGABusinessName = _mga.BusinessName;
                        }
                    }
                    result.Contractors.Add(_contractor);
                    
                }
            }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }



        [HttpPost]
        public IActionResult Post([FromBody]ContractCreateed model)
        {
            try
            {
                if (model == null)
               {
                    return BadRequest("User object is null");
               }
                var _model = new ContractCreate()
                {
                    FirstContractor = new Contractor()
                    {
                        CarrierId = (string.IsNullOrEmpty(model.FirstContractor.CarrierId) == true || model.FirstContractor.CarrierId.ToLower() == "null" ? (int?)null : int.Parse(model.FirstContractor.CarrierId)),
                        AdvisorId = (string.IsNullOrEmpty(model.FirstContractor.AdvisorId) == true || model.FirstContractor.AdvisorId.ToLower() == "null" ? (int?)null : int.Parse(model.FirstContractor.AdvisorId)),
                        MGAId = (string.IsNullOrEmpty(model.FirstContractor.MGAId) == true || model.FirstContractor.MGAId.ToLower() == "null" ? (int?)null : int.Parse(model.FirstContractor.MGAId)),
                    },
                    SecondContractor = new Contractor()
                    {
                        CarrierId = (string.IsNullOrEmpty(model.SecondContractor.CarrierId) == true || model.SecondContractor.CarrierId.ToLower() == "null" ? (int?)null : int.Parse(model.SecondContractor.CarrierId)),
                        AdvisorId = (string.IsNullOrEmpty(model.SecondContractor.AdvisorId) == true || model.SecondContractor.AdvisorId.ToLower() == "null" ? (int?)null : int.Parse(model.SecondContractor.AdvisorId)),
                        MGAId = (string.IsNullOrEmpty(model.SecondContractor.MGAId) == true || model.SecondContractor.MGAId.ToLower() == "null" ? (int?)null : int.Parse(model.SecondContractor.MGAId)),
                    }
                };

                if (!TryValidateModel(_model))
                {
                    return BadRequest("Invalid model object");
                }
                var _contract = new Contract()
                {
                    FirstContractor= new FirstContractor()
                    {
                        AdvisorId = _model.FirstContractor.AdvisorId,
                        CarrierId = _model.FirstContractor.CarrierId,
                        MGAId = _model.FirstContractor.MGAId,
                    },
                    SecondContractor = new SecondContractor()
                    {
                        AdvisorId = _model.SecondContractor.AdvisorId,
                        CarrierId = _model.SecondContractor.CarrierId,
                        MGAId = _model.SecondContractor.MGAId,
                    }

                };


                _contract = _serContract.CreateWithSaveChange(_contract);
             

                return Ok();
           }
            catch (Exception ex)
            {
               return StatusCode(500, $"Internal server error: {ex}");
           }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var _contract = _serContract.FindByCondition(x => x.Id == id).FirstOrDefault();
                if (_contract == null)
                { return BadRequest("User object is null"); }
                else
                {

                    _serContract.DeleteWithSaveChange(_contract);

                    return Ok();
                }
            }
            catch (Exception e) { return StatusCode(500, $"Internal server error: {e}"); }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurance.BusinessLogicLayer;
using Insurance.Entity;
using Insurance.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContarctController : ControllerBase
    {
        private readonly IWrapperRepository _repoWrapper;
        public ContarctController(IWrapperRepository repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }


        [HttpGet("{carrierId?}/{advisorId?}/{mgaId?}")]
        [Produces("application/json")]
        public IActionResult GET([FromQuery]string carrierId = null, [FromQuery]string advisorId = null, [FromQuery]string mgaId = null)
        {

            try
            {
                var model = new ContractCreate()
                {
                    CarrierId = (string.IsNullOrEmpty(carrierId) || carrierId.ToLower() == "null") ? (int?)null : int.Parse(carrierId),
                    AdvisorId = (string.IsNullOrEmpty(advisorId) || advisorId.ToLower() == "null") ? (int?)null : int.Parse(advisorId),
                    MGAId = (string.IsNullOrEmpty(mgaId) || mgaId.ToLower() == "null") ? (int?)null : int.Parse(mgaId)
                };
                if (model == null)
                {
                    return BadRequest("User object is null");
                }

                if (!TryValidateModel(model))
                {
                    return BadRequest("Invalid model object");
                }
                var _contract = new Contract()
                {
                    CarrierId = model.CarrierId,
                    AdvisorId = model.AdvisorId,
                    MGAId = model.MGAId,
                };
                var _directcontract = _repoWrapper.Contract.FindByCondition(x => x.CarrierId == model.CarrierId && x.MGAId == model.MGAId && x.AdvisorId == model.AdvisorId).FirstOrDefault();
                var _indirectcontract = _repoWrapper.Contract.GetIndirect(_contract);
                var ContractList = new ContractList();
                if (_directcontract != null)
                {
                    ContractList.Direct = new ContractResult()
                    {
                        Id = _directcontract.Id,
                        AdvisorId = _directcontract.AdvisorId,
                        AdvisorLastName = _directcontract.Advisor == null ? null : _directcontract.Advisor.LastName,
                        AdvisorFirstName = _directcontract.Advisor == null ? null : _directcontract.Advisor.FirstName,
                        MGAId = _directcontract.MGAId,
                        MGABusinessName = _directcontract.MGA == null ? null : _directcontract.MGA.BusinessName,
                        CarrierId = _directcontract.CarrierId,
                        CarrierBusinessName = _directcontract.Carrier == null ? null : _directcontract.Carrier.BusinessName
                    };
                }
                if (_indirectcontract.Count > 0)
                {
                    ContractList.IndirectList = _indirectcontract.Select(x => new ContractResult
                    {
                        AdvisorId = x.AdvisorId,
                        AdvisorLastName = x.Advisor == null ? null : x.Advisor.LastName,
                        AdvisorFirstName = x.Advisor == null ? null : x.Advisor.FirstName,
                        MGAId = x.MGAId,
                        MGABusinessName = x.MGA == null ? null : x.MGA.BusinessName,
                        CarrierId = x.CarrierId,
                        CarrierBusinessName = x.Carrier == null ? null : x.Carrier.BusinessName
                    }).ToList();
                }


                return Ok(ContractList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }



        [HttpPost]
        public IActionResult Post([FromBody]ContractCreated model)
        {
            var _model = new ContractCreate()
            {
                CarrierId = (string.IsNullOrEmpty(model.CarrierId) == true || model.CarrierId.ToLower() == "null" ? (int?)null : int.Parse(model.CarrierId)),
                AdvisorId = (string.IsNullOrEmpty(model.AdvisorId) == true || model.AdvisorId.ToLower() == "null" ? (int?)null : int.Parse(model.AdvisorId)),
                MGAId = (string.IsNullOrEmpty(model.MGAId) == true || model.MGAId.ToLower() == "null" ? (int?)null : int.Parse(model.MGAId)),
            };
            try
            {
                if (model == null)
                {
                    return BadRequest("User object is null");
                }

                if (!TryValidateModel(_model))
                {
                    return BadRequest("Invalid model object");
                }
                var _advisor = new Contract()
                {

                    AdvisorId = _model.AdvisorId,
                    CarrierId = _model.CarrierId,
                    MGAId = _model.MGAId,

                };


                _advisor = _repoWrapper.Contract.Create(_advisor);
                _repoWrapper.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var _contract = _repoWrapper.Contract.FindByCondition(x => x.Id == id).FirstOrDefault();
                if (_contract == null)
                { return BadRequest("User object is null"); }
                else
                {

                    _repoWrapper.Contract.Delete(_contract);
                    _repoWrapper.Save();
                    return Ok();
                }
            }
            catch (Exception e) { return StatusCode(500, $"Internal server error: {e}"); }
        }
    }
}
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
        public ContarctController(IContractService serContract)
        {
            _serContract = serContract;
        }


        [HttpGet("{firstCarrierId?}/{firstAdvisorId?}/{firstMgaId?}/{secondCarrierId?}/{secondAdvisorId?}/{secondMgaId?}")]
        [Produces("application/json")]
        public IActionResult GET([FromQuery]string firstCarrierId, [FromQuery]string firstAdvisorId, [FromQuery]string firstMgaId, [FromQuery]string secondCarrierId, [FromQuery]string secondAdvisorId, [FromQuery]string secondMgaId)
        {

         //   try
        //    {
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
            if (_path.Count > 0)
            {
                if (_path.Count == 2)
                {

                }
                else 
                { }
            }
                return Ok(_path);
        //    }
         //   catch (Exception ex)
         //   {
          //      return StatusCode(500, $"Internal server error: {ex}");
            //}
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
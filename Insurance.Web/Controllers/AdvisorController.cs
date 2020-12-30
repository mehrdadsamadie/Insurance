using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurance.BusinessLogicLayer;
using Insurance.Entity;
using Insurance.Service;
using Insurance.Web.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorController : ControllerBase
    {
        private readonly IAdvisorService _serAdvisor;
        public AdvisorController(IAdvisorService serAdvisor)
        {
            _serAdvisor = serAdvisor;
        }


        [HttpGet("{page?}/{pageSize?}")]
        [Produces("application/json")]
        public IActionResult Get([FromQuery]int? page=null, [FromQuery]int? pageSize=null)
        {
            try
            {
                var _advisors = _serAdvisor.FindAll();


                int _total = _advisors.Count();

                if (_advisors != null)
                {
                    var _list = new List<AdvisorView>();
                    if (page != null)
                    {
                        _list = _advisors.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).Select(x => new AdvisorView()
                        {
                            Id = x.Id,
                            Address = x.Address,
                            FirstName = x.FirstName,
                            HealthStatus = x.HealthStatus,
                            LastName = x.LastName,
                            PhoneNumber = x.PhoneNumber
                        }).ToList();
                    }
                    else {
                        _list = _advisors.Select(x => new AdvisorView()
                        {
                            Id = x.Id,
                            Address = x.Address,
                            FirstName = x.FirstName,
                            HealthStatus = x.HealthStatus,
                            LastName = x.LastName,
                            PhoneNumber = x.PhoneNumber
                        }).ToList();
                    }
                    return Ok(new AdvisorList() { List = _list, Total = _total });
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody]AdvisorView model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var _advisor = new Advisor()
                {

                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                };

                if (model.Id == 0)
                {

                    _advisor = _serAdvisor.CreateWithSaveChange(_advisor);
        
                }
                else
                {
                    _advisor.Id = model.Id;
                    _advisor.HealthStatus = model.HealthStatus;
                    _serAdvisor.UpdateWithSaveChange(_advisor);

                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            try
            {
                var _advisor = _serAdvisor.FindByCondition(x => x.Id == id).FirstOrDefault();
                if (_advisor != null)
                {
                    var _advisorView = new AdvisorView()
                    {
                        Address = _advisor.Address,
                        FirstName = _advisor.FirstName,
                        HealthStatus = _advisor.HealthStatus,
                        LastName = _advisor.HealthStatus,
                        PhoneNumber = _advisor.PhoneNumber
                    };
                    return Ok(_advisorView);
                }
                else
                {
                    return BadRequest("User object is null");
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var _advisor = _serAdvisor.FindByCondition(x => x.Id == id).FirstOrDefault();
                if (_advisor == null)
                { return BadRequest("User object is null"); }
                else
                {

                    _serAdvisor.DeleteWithSaveChange(_advisor);
                    return Ok();
                }
            }
            catch (Exception e) { return StatusCode(500, $"Internal server error: {e}"); }
        }

    }
}
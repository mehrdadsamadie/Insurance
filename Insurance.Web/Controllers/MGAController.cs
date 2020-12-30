using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurance.BusinessLogicLayer;
using Insurance.Entity;
using Insurance.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Insurance.Web.Model.MGAViewModel;

namespace Insurance.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MGAController : ControllerBase
    {
        private readonly IMGAService _serMGA;
        public MGAController(IMGAService serMGA)
        {
            _serMGA = serMGA;
        }

        [HttpGet("{page?}/{pageSize?}")]
        [Produces("application/json")]
        public IActionResult Get([FromQuery]int? page = null, [FromQuery]int? pageSize = null)
        {
            try
            {
                var _MGAs = _serMGA.FindAll();


                int _total = _MGAs.Count();

                if (_MGAs != null)
                {
                    var _list = new List<MGAView>();
                    if (page != null)
                    {
                         _list = _MGAs.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).Select(x => new MGAView()
                        {
                            Id = x.Id,
                            BusinessAddress = x.BusinessAddress,
                            BusinessName = x.BusinessName,
                            BusinessPhoneNumber = x.BusinessPhoneNumber
                        }).ToList();
                    }
                    else {
                        _list = _MGAs.Select(x => new MGAView()
                        {
                            Id = x.Id,
                            BusinessAddress = x.BusinessAddress,
                            BusinessName = x.BusinessName,
                            BusinessPhoneNumber = x.BusinessPhoneNumber
                        }).ToList();
                    }
                    return Ok(new { List = _list, Total = _total });
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]MGAView model)
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
                var _MGA = new MGA()
                {

                    BusinessAddress = model.BusinessAddress,
                    BusinessName = model.BusinessName,
                    BusinessPhoneNumber = model.BusinessPhoneNumber
                };

                if (model.Id == 0)
                {

                    _MGA = _serMGA.CreateWithSaveChange(_MGA);

                }
                else
                {
                    _MGA.Id = model.Id;
                    _serMGA.UpdateWithSaveChange(_MGA);
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
                var _MGA = _serMGA.FindByCondition(x => x.Id == id).FirstOrDefault();
                if (_MGA != null)
                {
                    var _contactView = new MGAView()
                    {
                        Id = _MGA.Id,
                        BusinessAddress = _MGA.BusinessAddress,
                        BusinessName = _MGA.BusinessName,
                        BusinessPhoneNumber = _MGA.BusinessPhoneNumber
                    };
                    return Ok(_contactView);
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
                var _MGA = _serMGA.FindByCondition(x => x.Id == id).FirstOrDefault();
                if (_MGA == null)
                { return BadRequest("User object is null"); }
                else
                {

                    _serMGA.DeleteWithSaveChange(_MGA);
                    return Ok();
                }
            }
            catch (Exception e) { return StatusCode(500, $"Internal server error: {e}"); }
        }

    }
}
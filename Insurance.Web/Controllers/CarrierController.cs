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
    public class CarrierController : ControllerBase
    {
        private readonly IWrapperRepository _repoWrapper;
        public CarrierController(IWrapperRepository repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        [HttpGet("{page?}/{pageSize?}")]
        public IActionResult Get([FromQuery]int? page = null, [FromQuery]int? pageSize = null)
        {
            try
            {
                var _carriers = _repoWrapper.Carrier.FindAll();
                int _total = _carriers.Count();

                if (_carriers != null)
                {
                    var _list = new List<CarrierView>();
                    if (page != null)
                    {
                        _list = _carriers.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).Select(x => new CarrierView()
                        {
                            Id = x.Id,
                            BusinessAddress = x.BusinessAddress,
                            BusinessName = x.BusinessName,
                            BusinessPhoneNumber = x.BusinessPhoneNumber
                        }).ToList();
                    }
                    else 
                    {
                        _list = _carriers.Select(x => new CarrierView()
                        {
                            Id = x.Id,
                            BusinessAddress = x.BusinessAddress,
                            BusinessName = x.BusinessName,
                            BusinessPhoneNumber = x.BusinessPhoneNumber
                        }).ToList();
                    }
                    return Ok(new CarrierList() { List = _list, Total = _total });
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody]CarrierView model)
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
                var _carrier = new Carrier()
                {
                    
                    BusinessAddress = model.BusinessAddress,
                    BusinessName = model.BusinessName,
                    BusinessPhoneNumber = model.BusinessPhoneNumber
                };

                if (model.Id == 0)
                {
                    
                    _carrier = _repoWrapper.Carrier.Create(_carrier);
                    _repoWrapper.Save();
                }
                else
                {
                    _carrier.Id = model.Id;
                    _repoWrapper.Carrier.Update(_carrier);
                    _repoWrapper.Save();
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
                var _carrier = _repoWrapper.Carrier.FindByCondition(x => x.Id == id).FirstOrDefault();
                if (_carrier != null)
                {
                    var _contactView = new CarrierView()
                    {
                        Id= _carrier.Id,
                        BusinessAddress=_carrier.BusinessAddress,
                        BusinessName=_carrier.BusinessName,
                        BusinessPhoneNumber=_carrier.BusinessPhoneNumber
                    };
                    return Ok( _contactView );
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
                var _carrier = _repoWrapper.Carrier.FindByCondition(x => x.Id == id).FirstOrDefault();
                if (_carrier == null)
                { return BadRequest("User object is null"); }
                else
                {

                    _repoWrapper.Carrier.Delete(_carrier);
                    _repoWrapper.Save();
                    return Ok();
                }
            }
            catch (Exception e) { return StatusCode(500, $"Internal server error: {e}"); }
        }


    }
}
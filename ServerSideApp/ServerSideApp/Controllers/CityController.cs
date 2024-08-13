using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerSideApp.DTOs;
using ServerSideApp.Services;
using ServerSideApp.Services.CityService;

namespace ServerSideApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly  ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("GetAllCitys")]
        public async Task<ActionResult<ServiceResponse<List<CityDTO>>>> GetAllCitys()
        {
            var res = await _cityService.GetAllCitys();

            if(res.Data != null)
            {
                return Ok(res.Data);
            }
            else
            {
                return BadRequest(res.Message);
            }                            
        }
    }
}

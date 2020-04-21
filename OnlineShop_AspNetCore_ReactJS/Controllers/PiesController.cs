using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PiesController : ControllerBase
    {
        private readonly IPieService pieService;
        private readonly IMapper mapper;

        public PiesController(IPieService pieService, IMapper mapper)
        {
            this.pieService = pieService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get All Pies or Pies Of The Week (?isPieOfTheWeek=true)
        /// </summary>
        /// <param name="isPieOfTheWeek">Optional parameter</param>
        /// <returns>Returns All Pies or Pies Of The Week</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Pie>>> GetPies(bool? isPieOfTheWeek)
        {
            IEnumerable<Pie> pies;
            if (isPieOfTheWeek.HasValue && isPieOfTheWeek.Value)
            {
                pies = await pieService.GetPiesOfTheWeekAsync();
            }
            else
            {
                pies = await pieService.GetPiesAsync();
            }
            return Ok(mapper.Map<Models.Pie[]>(pies));
        }

        /// <summary>
        /// Get Pie with specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Pie with specific id</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Models.Pie>> GetPie(int id)
        {
            var pie = await pieService.GetPieAsync(id);
            if (pie == null)
            {
                return NotFound($"Pie id {id} is invalid!");
            }
            return Ok(mapper.Map<Models.Pie>(pie));
        }
    }
}

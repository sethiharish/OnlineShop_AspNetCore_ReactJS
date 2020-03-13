using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Controllers
{
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Models.Pie>> GetPie(int id)
        {
            var pie = await pieService.GetPieAsync(id);
            if (pie == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Models.Pie>(pie));
        }
    }
}

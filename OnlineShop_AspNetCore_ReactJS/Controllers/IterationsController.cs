using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IterationsController : ControllerBase
    {
        private readonly IIterationService iterationService;
        private readonly IMapper mapper;

        public IterationsController(IIterationService iterationService, IMapper mapper)
        {
            this.iterationService = iterationService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Iteration>>> GetIterations()
        {
            var iterations = await iterationService.GetIterationsAsync();
            return Ok(mapper.Map<Models.Iteration[]>(iterations));
        }
    }
}

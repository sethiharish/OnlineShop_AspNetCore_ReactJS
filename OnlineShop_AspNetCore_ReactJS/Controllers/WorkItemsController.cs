using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemsController : ControllerBase
    {
        private readonly IWorkItemService workItemService;
        private readonly IMapper mapper;

        public WorkItemsController(IWorkItemService workItemService, IMapper mapper)
        {
            this.workItemService = workItemService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get All WorkItems
        /// </summary>
        /// <returns>Returns All WorkItems</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.WorkItem>>> GetWorkItems()
        {
            var workItems = await workItemService.GetWorkItemsAsync();
            return Ok(mapper.Map<Models.WorkItem[]>(workItems));
        }
    }
}

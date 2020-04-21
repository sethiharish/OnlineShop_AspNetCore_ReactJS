using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IBannerService bannerService;
        private readonly IMapper mapper;

        public BannersController(IBannerService bannerService, IMapper mapper)
        {
            this.bannerService = bannerService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get All Banners
        /// </summary>
        /// <returns>Returns All Banners</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Banner>>> GetBanners()
        {
            var banners = await bannerService.GetBannersAsync();
            return Ok(mapper.Map<Models.Banner[]>(banners));
        }

        /// <summary>
        /// Get Banner with specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Banner with specific id</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Models.Banner>> GetBanner(int id)
        {
            var banner = await bannerService.GetBannerAsync(id);
            if (banner == null)
            {
                return NotFound($"Banner id {id} is invalid!");
            }
            return Ok(mapper.Map<Models.Banner>(banner));
        }
    }
}

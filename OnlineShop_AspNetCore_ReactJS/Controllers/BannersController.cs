using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Controllers
{
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Banner>>> GetBanners()
        {
            var banners = await bannerService.GetBannersAsync();
            return Ok(mapper.Map<Models.Banner[]>(banners));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Banner>> GetBanner(int id)
        {
            var banner = await bannerService.GetBannerAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Models.Banner>(banner));
        }
    }
}

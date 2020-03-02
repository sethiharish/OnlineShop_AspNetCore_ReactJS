using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using OnlineShop_AspNetCore_ReactJS.Services;
using System;
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
        public async Task<ActionResult<IEnumerable<Banner>>> GetBanners()
        {
            var banners = await bannerService.GetBannersAsync();
            return Ok(mapper.Map<Models.Banner[]>(banners));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Banner>> GetBanner(int id)
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

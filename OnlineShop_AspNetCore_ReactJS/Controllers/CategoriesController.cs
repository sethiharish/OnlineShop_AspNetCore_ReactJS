using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Category>>> GetCategories()
        {
            var categories = await categoryService.GetCategoriesAsync();
            return Ok(mapper.Map<Models.Category[]>(categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Category>> GetCategory(int id)
        {
            var category = await categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Models.Category>(category));
        }
    }
}

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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns>Returns All Categories</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Category>>> GetCategories()
        {
            var categories = await categoryService.GetCategoriesAsync();
            return Ok(mapper.Map<Models.Category[]>(categories));
        }

        /// <summary>
        /// Get Category with specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Category with specific id</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Models.Category>> GetCategory(int id)
        {
            var category = await categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound($"Category id {id} is invalid!");
            }
            return Ok(mapper.Map<Models.Category>(category));
        }
    }
}

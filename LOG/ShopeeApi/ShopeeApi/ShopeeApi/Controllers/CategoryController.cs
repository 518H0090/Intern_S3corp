using Microsoft.AspNetCore.Mvc;
using ShopeeApi.Dtos;
using ShopeeApi.Service;

namespace ShopeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAllCategoriesWithoutRestaurant")]
        public async Task<IActionResult> GetAllCategoriesWithoutRestaurant()
        {
            var getFullCategories = await _service.GetFullCategories();

            if (getFullCategories.Data == null)
            {
                return BadRequest(getFullCategories);
            }

            return Ok(getFullCategories);
        }

        [HttpGet]
        [Route("GetAllCategoriesWithRestaurant/{ResId}")]
        public async Task<IActionResult> GetAllCategoriesWithRestaurant(int ResId)
        {
            var getAllCategories = await _service.GetAllCategories(ResId);

            if (getAllCategories.Data == null)
            {
                return BadRequest(getAllCategories);
            }

            return Ok(getAllCategories);
        }

        [HttpGet]
        [Route("GetCategoryWithId")]
        public async Task<IActionResult> GetCategoryWithId([FromQuery] RequestCategoryById request)
        {
            var GetCategoryById = await _service.GetCategoryById(request);

            if (GetCategoryById.Data == null)
            {
                return BadRequest(GetCategoryById);
            }

            return Ok(GetCategoryById);
        }

        [HttpGet]
        [Route("GetCategoryWithTag")]
        public async Task<IActionResult> GetCategoryWithTag([FromQuery] RequestCategoryByTag request)
        {
            var GetCategoryByTag = await _service.GetCategoryByTag(request);

            if (GetCategoryByTag.Data == null)
            {
                return BadRequest(GetCategoryByTag);
            }

            return Ok(GetCategoryByTag);
        }

        [HttpPost]
        [Route("NewCategoryInRestaurant")]
        public async Task<IActionResult> NewCategoryInRestaurant(RequestAddCategory request)
        {
            var newCategory = await _service.CreateCategory(request);

            if (newCategory.Data == null)
            {
                return BadRequest(newCategory);
            }

            return Ok(newCategory);
        }

        [HttpDelete]
        [Route("DeleteCategoryInRestaurant")]
        public async Task<IActionResult> DeleteCategoryInRestaurant(RequestDeleteCategory request)
        {
            var deleteCategory = await _service.DeleteCategory(request);

            if (deleteCategory.Data == null)
            {
                return BadRequest(deleteCategory);
            }

            return Ok(deleteCategory);
        }

        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(RequestEditCategory request)
        {
            var updateCategory = await _service.UpdateCategory(request);

            if (updateCategory.Data == null)
            {
                return BadRequest(updateCategory);
            }

            return Ok(updateCategory);
        }
    }
}
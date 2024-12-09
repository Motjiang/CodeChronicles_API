using CodeChronicles_API.Models.Domain;
using CodeChronicles_API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodeChronicles_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryRepository _categoryRepository) : ControllerBase
    {
        // Create a new category
        [HttpPost("add")]
        public async Task<IActionResult> Create(Category category)
        {
            var result = await _categoryRepository.CreateAsync(category);

            if (result)
            {
                return CreatedAtAction(nameof(Create), new { id = category.Id }, category);
            }
            return BadRequest();
        }

        // Get all categories
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();

            if(!categories.Any())
            {
                return NotFound();
            }

            return Ok(categories);
        }


        // Get a category by id
        [HttpGet("get-single/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // Update a category
        [HttpPut("update")]
        public async Task<IActionResult> Update(Category category)
        {
            var result = await _categoryRepository.UpdateAsync(category);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }


        // Delete a category
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryRepository.DeleteAsync(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Data;
using NewsPortal.DTOs;
using NewsPortal.Entity;

namespace NewsPortal.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategories([FromBody]Category _category)
        {
            var category = await _context.Categories.FindAsync(_category.Id);

            if (category != null)
            {
                return Conflict("Category already exists.");
            }

            _context.Categories.Add(_category);
            await _context.SaveChangesAsync();

            return Ok("Category Created Successfully");
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var allCategory = await _context.Categories.ToListAsync();
            if (allCategory == null)
            {
                return NotFound("No Categories");
            }

            return Ok(allCategory);
        }


        [HttpDelete("DelCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound("Error in Deleting Category");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditCategory(int id,[FromBody]Category _category)
        {
            var category = await _context.Categories.FindAsync(id);

            if(category == null)
            {
                return NotFound("Category not found");
            }

            if (!string.IsNullOrEmpty(_category.CategoryName))
            {
                category.CategoryName = _category.CategoryName;
            }

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return Ok("Updated Sucessfully");
        }

        [HttpGet("Category/{id}")]
        public async Task<IActionResult> GetCategoriesById(int id)
        {
            var categoryByID = await _context.Categories.FindAsync(id);

            if( categoryByID == null)
            {
                return NotFound("Category Not Found");
            }

            return Ok(categoryByID);

        }

        [HttpGet("newsByCategory")]
        public async Task<IActionResult> GetNewsCountByCategory()
        {
            var newsCountByCategory = await _context.News
                                                    .Include(news => news.Category) // This line ensures the Category is loaded
                                                    .Where(news => news.Category != null)
                                                    .GroupBy(news => news.Category!.CategoryName)
                                                    .Select(group => new
                                                    {
                                                        Category = group.Key,
                                                        Count = group.Count()
                                                    })
                                                    .ToListAsync();


            return Ok(newsCountByCategory);
        }

        [HttpGet("top-categories")]
        public async Task<IActionResult> GetTopCategories()
        {
            var result = await _context.News
                .Where(news => news.Category != null)
                .GroupBy(news => news.Category!.CategoryName)
                .Select(group => new CategoryCountDtos
                {
                    Category = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(g => g.Count)
                .Take(3)
                .ToListAsync();

            return Ok(result);
        }





    }
}

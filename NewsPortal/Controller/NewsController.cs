using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Data;
using NewsPortal.Entity;
using NewsPortal.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NewsPortal.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly DataContext _context;  
        public NewsController(DataContext context)
        {
            _context = context;
        }


        [HttpPost("create_news")]
        public async Task<IActionResult> AddNews([FromBody] NewsEntity news)
        {
            news.Category = null;
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return StatusCode(201, news);

        }

        [HttpGet("all_news")]
        public async Task<IActionResult> GetAllNews()
        {
            var allNews = await _context.News
                                        .Include(news => news.Category)  
                                        .ToListAsync();

            return Ok(allNews);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditNews([FromBody]NewsEntity editNews, [FromRoute] int id)
        {
            var existingNews = await _context.News.FindAsync(id);

            if (existingNews == null)
            {
                return NotFound("News item not found");
            }

            if (editNews.Title != null) existingNews.Title = editNews.Title;
            if (editNews.Description != null) existingNews.Description = editNews.Description;
            if (editNews.ImageUrl != null) existingNews.ImageUrl = editNews.ImageUrl;
            //if (editNews.Category.HasValue) existingNews.Category = editNews.Category.Value;
            if (editNews.CategoryId.HasValue)
            {
                existingNews.CategoryId = editNews.CategoryId.Value;
            }


            await _context.SaveChangesAsync();

            
            return Ok(existingNews);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var news = await _context.News
                                     .Include(n => n.Category) 
                                     .FirstOrDefaultAsync(n => n.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _context.News.FindAsync(id);

            if (news == null)
            {
                return NotFound("News item not found");
            }

            _context.News.Remove(news);
            await _context.SaveChangesAsync();

            return Ok("News Deleted Successfully");
        }

        [HttpGet("search/{id}")]
        public async Task<IActionResult> SearchNews(int id)
        {
            var search = await _context.News.FindAsync(id);
            if (search == null)
            {
                return NotFound("News item not found.");
            }

            return Ok(search);
        }

    }
}

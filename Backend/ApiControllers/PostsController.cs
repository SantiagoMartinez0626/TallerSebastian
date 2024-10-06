using ForumApp.API.Models;
using ForumApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly PostService _service;

        public ArticlesController(PostService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Post>> RetrieveAll() =>
            await _service.GetAsync();

        [HttpGet("{identifier:length(24)}")]
        public async Task<ActionResult<Post>> Retrieve(string identifier)
        {
            var article = await _service.GetAsync(identifier);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post newArticle)
        {
            await _service.CreateAsync(newArticle);

            return CreatedAtAction(nameof(Retrieve), new { identifier = newArticle.Id }, newArticle);
        }

        [HttpPut("{identifier:length(24)}")]
        public async Task<IActionResult> Modify(string identifier, Post modifiedArticle)
        {
            var existingArticle = await _service.GetAsync(identifier);

            if (existingArticle == null)
            {
                return NotFound();
            }

            modifiedArticle.Id = existingArticle.Id;

            await _service.UpdateAsync(identifier, modifiedArticle);

            return NoContent();
        }

        [HttpDelete("{identifier:length(24)}")]
        public async Task<IActionResult> Remove(string identifier)
        {
            var existingArticle = await _service.GetAsync(identifier);

            if (existingArticle == null)
            {
                return NotFound();
            }

            await _service.RemoveAsync(identifier);

            return NoContent();
        }
    }
}

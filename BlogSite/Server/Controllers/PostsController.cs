using BlogSite.Server.Repository;
using BlogSite.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogSite.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<PostsController>
        [HttpGet]
        public async Task<ActionResult> GetPosts()
        {
            var posts = await _unitOfWork.Posts.GetAll();
            return Ok(posts);
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPost(int id)
        {
            var post = await _unitOfWork.Posts.Get(q => q.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // POST api/<PostsController>
        [HttpPost]
        public async Task<ActionResult<Post>> PostMake(Post post)
        {
            await _unitOfWork.Posts.Insert(post);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Posts.Update(post);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _unitOfWork.Posts.Get(q => q.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            await _unitOfWork.Posts.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> PostExists(int id)
        {
            var post = await _unitOfWork.Posts.Get(q => q.Id == id);
            return post != null;
        }
    }
}

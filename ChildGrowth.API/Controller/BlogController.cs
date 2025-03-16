using AutoMapper;
using ChildGrowth.API.Constants;
using ChildGrowth.API.Payload.Request.Blog;
using ChildGrowth.API.Payload.Response.Blog;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Paginate;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller
{
    [ApiController]
    [Route(ApiEndPointConstant.Blog.BlogEndPoint)]
    public class BlogController : BaseController<BlogController>
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService, IMapper mapper, ILogger<BlogController> logger)
            : base(logger)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IPaginate<BlogResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBlogs([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var blogs = await _blogService.GetAllBlogsAsync(page, size);
            return Ok(blogs);
        }

        [HttpGet(ApiEndPointConstant.Blog.GetById)]
        [ProducesResponseType(typeof(BlogResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBlogById([FromRoute] int blogId)
        {
            var blog = await _blogService.GetBlogByIdAsync(blogId);
            if (blog == null)
                return NotFound();
            return Ok(blog);
        }

        [HttpPost(ApiEndPointConstant.Blog.Create)]
        [ProducesResponseType(typeof(BlogResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogRequest request)
        {
            var newBlog = await _blogService.CreateBlogAsync(request);
            return CreatedAtAction(nameof(GetBlogById), new { blogId = newBlog.BlogId }, newBlog);
        }

        [HttpPut(ApiEndPointConstant.Blog.Update)]
        [ProducesResponseType(typeof(BlogResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBlog([FromRoute] int blogId, [FromBody] UpdateBlogRequest request)
        {
            var updatedBlog = await _blogService.UpdateBlogAsync(blogId, request);
            return Ok(updatedBlog);
        }

        [HttpDelete(ApiEndPointConstant.Blog.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBlog([FromRoute] int blogId)
        {
            var deleted = await _blogService.DeleteBlogAsync(blogId);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        [HttpGet(ApiEndPointConstant.Blog.Search)]
        [ProducesResponseType(typeof(IPaginate<BlogResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchBlogs(
            [FromQuery] string? keyword,
            [FromQuery] string? category,
            [FromQuery] string? tag,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var result = await _blogService.SearchBlogsAsync(keyword, category, tag, page, size);
            return Ok(result);
        }

        [HttpPost(ApiEndPointConstant.Blog.View)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> IncrementViewCount([FromRoute] int blogId)
        {
            var success = await _blogService.IncrementViewCountAsync(blogId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPost(ApiEndPointConstant.Blog.Like)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LikeBlog([FromRoute] int blogId)
        {
            var success = await _blogService.LikeBlogAsync(blogId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPost(ApiEndPointConstant.Blog.Comment)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CommentBlog([FromRoute] int blogId)
        {
            var success = await _blogService.CommentBlogAsync(blogId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

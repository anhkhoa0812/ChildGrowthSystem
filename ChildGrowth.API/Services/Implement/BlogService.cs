using AutoMapper;
using ChildGrowth.API.Payload.Request.Blog;
using ChildGrowth.API.Payload.Response.Blog;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChildGrowth.API.Services.Implement
{
    public class BlogService : BaseService<Blog>, IBlogService
    {
        public BlogService(
            IUnitOfWork<ChildGrowDBContext> unitOfWork,
            ILogger<Blog> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<IPaginate<BlogResponse>> GetAllBlogsAsync(int page, int size)
        {
            var blogs = await _unitOfWork.GetRepository<Blog>().GetPagingListAsync(
                predicate: null,
                orderBy: b => b.OrderByDescending(x => x.PublishDate),
                include: null,
                page: page,
                size: size
            );
            return _mapper.Map<IPaginate<BlogResponse>>(blogs);
        }

        public async Task<BlogResponse> GetBlogByIdAsync(int blogId)
        {
            var blog = await _unitOfWork.GetRepository<Blog>().SingleOrDefaultAsync(
                predicate: x => x.BlogId == blogId,
                orderBy: null,
                include: null
            );
            if (blog == null)
                throw new KeyNotFoundException("Blog not found");

            return _mapper.Map<BlogResponse>(blog);
        }

        public async Task<BlogResponse> CreateBlogAsync(CreateBlogRequest request)
        {
            var blog = _mapper.Map<Blog>(request);
            blog.PublishDate = DateTime.UtcNow;
            blog.UpdatedDate = DateTime.UtcNow;
            blog.Status = blog.Status ?? "Draft";
            blog.ViewCount = 0;
            blog.LikeCount = 0;
            blog.CommentCount = 0;

            await _unitOfWork.GetRepository<Blog>().InsertAsync(blog);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<BlogResponse>(blog);
        }

        public async Task<BlogResponse> UpdateBlogAsync(int blogId, UpdateBlogRequest request)
        {
            var blog = await _unitOfWork.GetRepository<Blog>().SingleOrDefaultAsync(
                predicate: x => x.BlogId == blogId,
                orderBy: null,
                include: null
            );
            if (blog == null)
                throw new KeyNotFoundException("Blog not found");

            _mapper.Map(request, blog);
            blog.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.GetRepository<Blog>().UpdateAsync(blog);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<BlogResponse>(blog);
        }

        public async Task<bool> DeleteBlogAsync(int blogId)
        {
            var blog = await _unitOfWork.GetRepository<Blog>().SingleOrDefaultAsync(
                predicate: x => x.BlogId == blogId,
                orderBy: null,
                include: null
            );
            if (blog == null)
                return false;

            _unitOfWork.GetRepository<Blog>().DeleteAsync(blog);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IPaginate<BlogResponse>> SearchBlogsAsync(string? keyword, string? category, string? tag, int page, int size)
        {
            Expression<Func<Blog, bool>> predicate = b =>
                (string.IsNullOrEmpty(keyword) || b.Title.Contains(keyword) || b.Content.Contains(keyword)) &&
                (string.IsNullOrEmpty(category) || b.Category == category) &&
                (string.IsNullOrEmpty(tag) || (b.Tags != null && b.Tags.Contains(tag)));

            var paginatedBlogs = await _unitOfWork.GetRepository<Blog>().GetPagingListAsync(
                predicate: predicate,
                orderBy: b => b.OrderByDescending(x => x.PublishDate),
                include: null,
                page: page,
                size: size
            );

            return _mapper.Map<IPaginate<BlogResponse>>(paginatedBlogs);
        }

        public async Task<bool> IncrementViewCountAsync(int blogId)
        {
            var blog = await _unitOfWork.GetRepository<Blog>().SingleOrDefaultAsync(
                predicate: x => x.BlogId == blogId,
                orderBy: null,
                include: null
            );
            if (blog == null)
                return false;

            blog.ViewCount = (blog.ViewCount ?? 0) + 1;
            _unitOfWork.GetRepository<Blog>().UpdateAsync(blog);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<bool> LikeBlogAsync(int blogId)
        {
            var blog = await _unitOfWork.GetRepository<Blog>().SingleOrDefaultAsync(
                predicate: x => x.BlogId == blogId,
                orderBy: null,
                include: null
            );
            if (blog == null)
                return false;

            blog.LikeCount = (blog.LikeCount ?? 0) + 1;
            _unitOfWork.GetRepository<Blog>().UpdateAsync(blog);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> CommentBlogAsync(int blogId)
        {
            var blog = await _unitOfWork.GetRepository<Blog>().SingleOrDefaultAsync(
                predicate: x => x.BlogId == blogId,
                orderBy: null,
                include: null
            );
            if (blog == null)
                return false;

            blog.CommentCount = (blog.CommentCount ?? 0) + 1;
            _unitOfWork.GetRepository<Blog>().UpdateAsync(blog);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}

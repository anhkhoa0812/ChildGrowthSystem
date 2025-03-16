using ChildGrowth.API.Payload.Request.Blog;
using ChildGrowth.API.Payload.Response.Blog;
using ChildGrowth.Domain.Paginate;

namespace ChildGrowth.API.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IPaginate<BlogResponse>> GetAllBlogsAsync(int page, int size);
        Task<BlogResponse> GetBlogByIdAsync(int blogId);
        Task<BlogResponse> CreateBlogAsync(CreateBlogRequest request);
        Task<BlogResponse> UpdateBlogAsync(int blogId, UpdateBlogRequest request);
        Task<bool> DeleteBlogAsync(int blogId);

        Task<IPaginate<BlogResponse>> SearchBlogsAsync(string? keyword, string? category, string? tag, int page, int size);
        Task<bool> IncrementViewCountAsync(int blogId);
        Task<bool> LikeBlogAsync(int blogId);
        Task<bool> CommentBlogAsync(int blogId);
    }
}

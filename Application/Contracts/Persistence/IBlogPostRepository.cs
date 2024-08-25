using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IBlogPostRepository
{
    Task<List<BlogPost>> GetBlogPostsAsync();
    Task<BlogPost> GetBlogPostByIdAsync(Guid id);
    Task<BlogPost> CreateBlogPostAsync(BlogPost blogPost);
    Task<BlogPost> UpdateBlogPostAsync(BlogPost blogPost, Guid id);
}

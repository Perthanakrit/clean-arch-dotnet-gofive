using System;
using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;

namespace Persistence.Repositories;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly ApplicationDbContext dbContext;

    public BlogPostRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<BlogPost> CreateBlogPostAsync(BlogPost blogPost)
    {
        await dbContext.BlogPosts.AddAsync(blogPost);
        await dbContext.SaveChangesAsync();
        return blogPost;
    }

    public Task<BlogPost> GetBlogPostByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<BlogPost>> GetBlogPostsAsync()
    {
        return await dbContext.BlogPosts.AsNoTracking()
                    .Include(b => b.Categories)
                    .ToListAsync();
    }

    public async Task<BlogPost> UpdateBlogPostAsync(BlogPost blogPost, Guid id)
    {
        BlogPost blogPostDb = await dbContext.BlogPosts.FirstOrDefaultAsync(b => b.Id == id);

        if (blogPostDb == null)
        {
            return null;
        }

        blogPostDb.Title = blogPost.Title;
        blogPostDb.Content = blogPost.Content;
        blogPostDb.UrlHandle = blogPost.UrlHandle;

        foreach (Category category in blogPost.Categories)
        {
            Category categoryDb = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (categoryDb != null)
            {
                blogPostDb.Categories.Add(categoryDb);
            }
        }

        await dbContext.SaveChangesAsync();
        return blogPostDb;
    }
}

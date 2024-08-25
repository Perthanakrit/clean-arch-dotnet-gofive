using System;
using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.DatabaseContext;

namespace Persistence.Repositories;

public class BlogImageRepository : IBlogImageRepository
{
    private readonly ApplicationDbContext dbContext;

    public BlogImageRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<BlogImage> SaveImage(BlogImage image)
    {
        await dbContext.BlogImages.AddAsync(image);
        await dbContext.SaveChangesAsync();
        return image;
    }
}

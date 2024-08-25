using Application.Contracts.Persistence;
using Application.Features.BlogPost.Commands.CreateBlogPost;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Category.Commands.CreateBlogPost;

public class CreateBlogPostHandler : IRequestHandler<CreateBlogPostCommand, BlogPostDto>
{
    private readonly IBlogPostRepository blogPostRepository;
    private readonly IMapper mapper;
    private readonly ICategoryRepository categoryRepository;

    public CreateBlogPostHandler(IBlogPostRepository blogPostRepository, IMapper mapper, ICategoryRepository categoryRepository)
    {
        this.blogPostRepository = blogPostRepository;
        this.mapper = mapper;
        this.categoryRepository = categoryRepository;
    }

    public async Task<BlogPostDto> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.BlogPost blogPost = mapper.Map<Domain.Entities.BlogPost>(request.Request);
        
        foreach(Guid categoryId in request.Request.CategoryIds)
        {
            Domain.Entities.Category category = await categoryRepository.GetByIdAsync(categoryId);
            if (category is not null)
            {
                blogPost.Categories.Add(category);
            }
        }

        Domain.Entities.BlogPost result = await blogPostRepository.CreateBlogPostAsync(blogPost);

        return mapper.Map<BlogPostDto>(result);
    }
}

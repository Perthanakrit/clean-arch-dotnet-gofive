using System;
using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.BlogPost.Commands.UpdateBlogPost;

public class UpdateBlogPostHandler : IRequestHandler<UpdateBlogPostCommand, BlogPostDto>
{
    private readonly IBlogPostRepository blogPostRepository;
    private readonly IMapper mapper;

    public UpdateBlogPostHandler(IBlogPostRepository blogPostRepository, IMapper mapper)
    {
        this.blogPostRepository = blogPostRepository;
        this.mapper = mapper;
    }

    public async Task<BlogPostDto> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.BlogPost blogPost = mapper.Map<Domain.Entities.BlogPost>(request.Request);
        Domain.Entities.BlogPost result = await blogPostRepository.UpdateBlogPostAsync(blogPost, request.Id);

        if (result == null)
        {
            throw new ArgumentNullException("blog post not found");
        }

        return mapper.Map<BlogPostDto>(result);
    }
}

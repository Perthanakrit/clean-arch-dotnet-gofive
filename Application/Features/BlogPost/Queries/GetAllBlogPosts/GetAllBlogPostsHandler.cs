using System;
using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Category.Queries.GetAllBlogPosts;

public class GetAllBlogPostsHandler : IRequestHandler<GetAllBlogPostsQuery, List<BlogPostDto>>
{
    private readonly IBlogPostRepository blogPostRepository;
    private readonly IMapper mapper;

    public GetAllBlogPostsHandler(IBlogPostRepository blogPostRepository, IMapper mapper)
    {
        this.blogPostRepository = blogPostRepository;
        this.mapper = mapper;
    }

    public async Task<List<BlogPostDto>> Handle(GetAllBlogPostsQuery request, CancellationToken cancellationToken)
    {
        List<Domain.Entities.BlogPost> blogPosts = await blogPostRepository.GetBlogPostsAsync();

        return mapper.Map<List<BlogPostDto>>(blogPosts);
    }
}

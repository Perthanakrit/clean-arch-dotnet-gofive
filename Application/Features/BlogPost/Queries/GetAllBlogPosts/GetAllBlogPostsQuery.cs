using System;
using Application.Models;
using MediatR;

namespace Application.Features.Category.Queries.GetAllBlogPosts;

public class GetAllBlogPostsQuery : IRequest<List<BlogPostDto>>
{
}

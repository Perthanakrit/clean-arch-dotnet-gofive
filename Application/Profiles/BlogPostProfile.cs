using Application.Models;
using Application.Models.BlogPost;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class BlogPostProfile : Profile
{
    public BlogPostProfile()
    {
        CreateMap<CreateBlogPostRequestDto, BlogPost>()
            .ForMember(des => des.Categories, opt=> opt.MapFrom(src => new List<Category>()));
        CreateMap<BlogPost, BlogPostDto>();
        CreateMap<UpdateBlogPostRequestDto, BlogPost>()
            .ForMember(des => des.Categories, opt=> opt.MapFrom(src => new List<Category>()));
    }
}

using System;
using Application.Features.Image.UploadImage;
using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class BlogImageProfile : Profile
{
    public BlogImageProfile()
    {
        CreateMap<BlogImage, BlogImageDto>();

        CreateMap<UploadImageCommand, BlogImage>();
    }
}

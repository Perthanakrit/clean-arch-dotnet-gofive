using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Image.UploadImage;

public class UploadImageHandler : IRequestHandler<UploadImageCommand, BlogImageDto>
{
    private readonly IHostingEnvironment hostingEnvironment;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IBlogImageRepository blogImageRepository;
    private readonly IMapper mapper;

    public UploadImageHandler(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, 
        IBlogImageRepository blogImageRepository, IMapper mapper)
    {
        this.hostingEnvironment = hostingEnvironment;
        this.httpContextAccessor = httpContextAccessor;
        this.blogImageRepository = blogImageRepository;
        this.mapper = mapper;
    }

    public async Task<BlogImageDto> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var file = request.File;
        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        Directory.CreateDirectory(Path.Combine(hostingEnvironment.ContentRootPath, "Images"));

        var localPath = Path.Combine(hostingEnvironment.ContentRootPath, "Images", $"{request.FileName}{fileExtension}");
        using var stream = new FileStream(localPath, FileMode.Create);
        await file.CopyToAsync(stream);

        // Save the image to the database
        HttpRequest httpRequest = httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{request.FileName}{fileExtension}";

        BlogImage blogImage = new BlogImage{
            FileName = request.FileName,
            FileExtension = fileExtension,
            Title = request.Title,
            Url = baseUrl,
            DateCreated = DateTime.UtcNow
        };

        BlogImage result = await blogImageRepository.SaveImage(blogImage);

        return mapper.Map<BlogImageDto>(result);
    }
}

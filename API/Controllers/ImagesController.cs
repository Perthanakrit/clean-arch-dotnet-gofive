using Application.Features.Image.UploadImage;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ImagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {
            BlogImageDto image = await mediator.Send(new UploadImageCommand { File = file, FileName = fileName, Title = title });
            return Ok(image);
        }

        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            return Ok();
        }
    }
}

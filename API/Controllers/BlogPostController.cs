using Application.Features.BlogPost.Commands.CreateBlogPost;
using Application.Features.BlogPost.Commands.UpdateBlogPost;
using Application.Features.Category.Queries.GetAllBlogPosts;
using Application.Models;
using Application.Models.BlogPost;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IMediator mediator;

        public BlogPostController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            try
            {
                List<BlogPostDto> result = await mediator.Send(new GetAllBlogPostsQuery());
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById(Guid id)
        {
            try
            {
                //var result = await mediator.Send(new GetBlogPostByIdQuery() { Id = id });
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotImplementedException ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }

        [HttpGet("{url:alpha}")]
        public async Task<IActionResult> GetBlogPostByUrl(string url)
        {
            try
            {
                //var result = await mediator.Send(new GetBlogPostByUrlQuery() { Url = url });
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotImplementedException ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            try
            {
                CreateBlogPostCommand command = new CreateBlogPostCommand(){ Request = request };

                BlogPostDto result = await mediator.Send(command);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, [FromBody] UpdateBlogPostRequestDto request)
        {
            try
            {
                UpdateBlogPostCommand command = new UpdateBlogPostCommand()
                {
                Request = request,
                Id = id
                };

                BlogPostDto result = await mediator.Send(command);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            try
            {
                //DeleteBlogPostCommand command = new DeleteBlogPostCommand()
                //{
                //    Id = id
                //};

                //await mediator.Send(command);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

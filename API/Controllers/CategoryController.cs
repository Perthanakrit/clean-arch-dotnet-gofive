using Application.Features.Category.Commands.CreateCategory;
using Application.Features.Category.Commands.DeleteCategory;
using Application.Features.Category.Commands.UpdateCategory;
using Application.Features.Category.Queries.GetAllCategories;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator) {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {   
            try
            {
                var result = await mediator.Send(new GetAllCategoriesQuery());
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(NotImplementedException ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
        {
            try
            {
                CreateCategoryCommand command = new CreateCategoryCommand() {
                    Request = request
                };

                CategoryDto result = await mediator.Send(command);
                
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto request)
        {
            try
            {
                UpdateCategoryCommand command = new UpdateCategoryCommand() {
                    Request = request,
                    Id = id
                };

                CategoryDto result = await mediator.Send(command);
                
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            try
            {
                DeleteCategoryCommand command = new DeleteCategoryCommand() {
                    Id = id
                };

                CategoryDto result = await mediator.Send(command);

                if (result == null)
                {
                    return NotFound();
                }
                
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

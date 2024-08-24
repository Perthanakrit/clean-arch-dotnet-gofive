using Application.Features.Category.Queries.GetAllCategories;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
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
                return StatusCode(501, ex.Message);
            }
        }
    }
}

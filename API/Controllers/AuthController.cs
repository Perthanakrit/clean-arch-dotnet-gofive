using Application.Features.Auth.Login.Queries.GetTokenJwt;
using Application.Features.Auth.Register.Commands;
using Application.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerDto)
        {
            try
            {
                bool result = await mediator.Send(new CreateAccountCommand { RegisterModel = registerDto });
                return Ok(result);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginDto)
        {
            try
            {
                LoginResponseDto result = await mediator.Send(new GetTokenJwtQuery {
                    Email = loginDto.Email,
                    Password = loginDto.Password
                });
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
    }
}

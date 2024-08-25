using System;
using Application.Models.Auth;
using MediatR;

namespace Application.Features.Auth.Login.Queries.GetTokenJwt;

public class GetTokenJwtQuery : IRequest<LoginResponseDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

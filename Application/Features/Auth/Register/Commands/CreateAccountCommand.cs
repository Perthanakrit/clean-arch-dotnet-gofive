using System;
using Application.Models.Auth;
using MediatR;

namespace Application.Features.Auth.Register.Commands;

public class CreateAccountCommand : IRequest<bool>
{
    public RegisterRequestDto RegisterModel { get; set; }
}

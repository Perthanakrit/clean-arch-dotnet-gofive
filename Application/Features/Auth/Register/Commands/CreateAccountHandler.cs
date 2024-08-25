using System;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Register.Commands;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, bool>
{
    private readonly UserManager<IdentityUser> userManager;

    public CreateAccountHandler(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        IdentityUser user = new IdentityUser
        {
            UserName = request.RegisterModel.Email.Trim(),
            Email = request.RegisterModel.Email.Trim()
        };

        IdentityResult identityResult = await userManager.CreateAsync(user, request.RegisterModel.Password);

        if (identityResult.Succeeded is false)
        {
            throw new ArgumentException("Failed to create user because: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));
        }

        identityResult = await userManager.AddToRolesAsync(user, ["Reader", "Writer"]);

        if (identityResult.Succeeded is false)
        {
            throw new ArgumentException("Failed to assign roles to user because: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));
        }

        return true;
    }
}

using System;

namespace Application.Models.Auth;

public class LoginResponseDto
{
    public string Email { get; set; }
    public string Token { get; set; }
    public List<string> Roles { get; set; }
}

using CleanArchitecture.Application.Users.Commands.LoginUser;
using CleanArchitecture.Application.Users.Commands.RegisterUser;

using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers;

public class UsersController : ApiControllerBase
{
    [HttpPost("login")]
    public async Task<bool> Login(LoginUserCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPost("register")]
    public async Task<string> Register(RegisterUserCommand command)
    {
        return await Mediator.Send(command);
    }
}
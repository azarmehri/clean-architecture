using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

using MediatR;

namespace CleanArchitecture.Application.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public RegisterUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.CreateUserAsync(request.Email, request.Password);
        return user.Result.Succeeded ? user.UserId : string.Join(",", user.Result.Errors);
    }
}
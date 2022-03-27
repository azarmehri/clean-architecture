using CleanArchitecture.Application.Common.Interfaces;

using MediatR;

namespace CleanArchitecture.Application.Users.Commands.LoginUser;

public class LoginUserCommand : IRequest<bool>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsPersistent { get; set; }
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, bool>
{
    private readonly IIdentityService _identityService;

    public LoginUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.LoginUserAsync(request.Email, request.Password, request.IsPersistent);
    }
}
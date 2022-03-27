using CleanArchitecture.Application.Common.Interfaces;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly IApplicationDbContext _context;

    public RegisterUserCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
        RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
        // .MustAsync(BeUniqueEmail).WithMessage("The specified email already exists.");
    }

    // public async Task<bool> BeUniqueEmail(string title, CancellationToken cancellationToken)
    // {
    //     return await _context.TodoLists
    //         .AllAsync(l => l.Title != title, cancellationToken);
    // }
}
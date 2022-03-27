using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

using MediatR;

namespace CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommand : IRequest<int>
{
    public string? Title { get; set; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        TodoList entity = new();

        entity.Title = request.Title;

        _context.TodoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
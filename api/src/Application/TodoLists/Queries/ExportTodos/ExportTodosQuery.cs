using AutoMapper;
using AutoMapper.QueryableExtensions;

using CleanArchitecture.Application.Common.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoLists.Queries.ExportTodos;

public class ExportTodosQuery : IRequest<ExportTodosVm>
{
    public int ListId { get; set; }
}

public class ExportTodosQueryHandler : IRequestHandler<ExportTodosQuery, ExportTodosVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ExportTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ExportTodosVm> Handle(ExportTodosQuery request, CancellationToken cancellationToken)
    {
        List<TodoItemRecord> records = await _context.TodoItems
            .Where(t => t.ListId == request.ListId)
            .ProjectTo<TodoItemRecord>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        ExportTodosVm vm = new(
            "TodoItems.csv",
            "text/csv",
            Array.Empty<byte>());

        return vm;
    }
}
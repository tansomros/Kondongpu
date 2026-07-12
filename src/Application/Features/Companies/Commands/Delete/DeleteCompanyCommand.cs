using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.Companies.Commands.Delete;

public record DeleteCompanyCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Unit>
{
    private readonly IKondongpuDatabaseContext _context;

    public DeleteCompanyCommandHandler(IKondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.Company
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Companies), request.Id);
        }

        _context.Company.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

    }
}

public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
{
    private readonly IKondongpuDatabaseContext _context;
    public DeleteCompanyCommandValidator(IKondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty().WithMessage("Id ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ Id");
    }
}

using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Companies.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Companies.Queries.Get;

public record GetCompanyByIdQuery : IRequest<CompanyViewModel>
{
    public required int Id { get; set; }
}

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyViewModel>
{
    private readonly IMapper _mapper;
    private readonly IKondongpuDatabaseContext _context;

    public GetCompanyByIdQueryHandler(IKondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CompanyViewModel> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var company = await _context.Company.FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Company), request.Id);

        return _mapper.Map<CompanyViewModel>(company);
    }
}

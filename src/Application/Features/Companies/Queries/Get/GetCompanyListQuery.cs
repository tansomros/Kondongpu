using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Companies.ViewModels;

namespace Kondongpu.Application.Features.Companies.Queries.Get;

public class GetCompanyListQuery : IRequest<CompanyListViewModel>
{

}

public class GetCompanyListQueryHandler : IRequestHandler<GetCompanyListQuery, CompanyListViewModel>
{
    private readonly IKondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetCompanyListQueryHandler(IKondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CompanyListViewModel> Handle(GetCompanyListQuery request, CancellationToken cancellationToken)
    {
        var companies = await _context.Company.AsNoTracking().ToListAsync(cancellationToken);
        var companyList = _mapper.Map<List<CompanyViewModel>>(companies);
        return new CompanyListViewModel()
        {
            Companies = companyList,
        };
    }
}

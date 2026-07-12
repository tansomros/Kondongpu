using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CareProviders.ViewModels;

namespace Kondongpu.Application.Features.CareProviders.Queries.Get
{
    public class GetCareProviderListQuery : IRequest<CareProviderListViewModel>
    {

    }  

    public class GetCareProviderListQueryHandler : IRequestHandler<GetCareProviderListQuery, CareProviderListViewModel>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;
        private readonly IMapper _mapper;

        public GetCareProviderListQueryHandler(KondongpuDatabaseContext checkupDatabaseContext, IMapper mapper)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
            _mapper = mapper;
        }

        public async Task<CareProviderListViewModel> Handle(GetCareProviderListQuery request, CancellationToken cancellationToken)
        {            
            var result = await _checkupDatabaseContext.CareProviders.Where(d => d.IsActive == true)
                .OrderBy(d => d.FullNameThai)
                .ToListAsync(cancellationToken);

            return new CareProviderListViewModel { CareProviders = _mapper.Map<List<CareProviderViewModel>>(result) };
        }
    }
}

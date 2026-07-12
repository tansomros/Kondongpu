using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CareProviders.ViewModels;

namespace Kondongpu.Application.Features.CareProviders.Queries.Get
{
    public class GetCareProviderByTypeQuery : IRequest<CareProviderListViewModel>
    {
        public int CareProviderTypeId { get; set; }
    }  

    public class GetCareProviderByTypeQueryHandler : IRequestHandler<GetCareProviderByTypeQuery, CareProviderListViewModel>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;
        private readonly IMapper _mapper;

        public GetCareProviderByTypeQueryHandler(KondongpuDatabaseContext checkupDatabaseContext, IMapper mapper)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
            _mapper = mapper;
        }

        public async Task<CareProviderListViewModel> Handle(GetCareProviderByTypeQuery request, CancellationToken cancellationToken)
        {
            var careProviderTypeId = 0;
            if (request != null)
            {
                careProviderTypeId = request.CareProviderTypeId;
            }

            var result = await _checkupDatabaseContext.CareProviders.Where(d => d.CareProviderTypeId == careProviderTypeId && d.IsActive == true)
                .OrderBy(d => d.FullNameThai)
                .ToListAsync(cancellationToken);

            return new CareProviderListViewModel { CareProviders = _mapper.Map<List<CareProviderViewModel>>(result) };
        }
    }
}

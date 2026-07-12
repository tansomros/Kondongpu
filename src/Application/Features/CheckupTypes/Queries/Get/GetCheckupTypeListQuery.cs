using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CheckupTypes.ViewModels;

namespace Kondongpu.Application.Features.CheckupTypes.Queries.Get
{ 
    public class GetCheckupTypeListQuery : IRequest<CheckupTypeListViewModel>
    {
       
    }

    public class GetCheckupTypeListQueryHandler : IRequestHandler<GetCheckupTypeListQuery, CheckupTypeListViewModel>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;
        private readonly IMapper _mapper;

        public GetCheckupTypeListQueryHandler(KondongpuDatabaseContext checkupDatabaseContext, IMapper mapper)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
            _mapper = mapper;
        }

        public async Task<CheckupTypeListViewModel> Handle(GetCheckupTypeListQuery request, CancellationToken cancellationToken)
        {
           
            var result = await _checkupDatabaseContext.CheckupTypes
                .OrderBy(d => d.Name)
                .ToListAsync(cancellationToken);

            return new CheckupTypeListViewModel { CheckupTypes = _mapper.Map<List<CheckupTypeViewModel>>(result) };
        }
    }
}

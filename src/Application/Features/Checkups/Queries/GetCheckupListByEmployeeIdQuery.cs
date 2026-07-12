using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Checkups.ViewModels;

namespace Kondongpu.Application.Features.Checkups.Queries;
public class GetCheckupListByEmployeeIdQuery : IRequest<PaginatedList<CheckupViewModel>>
{  
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }  
    public required string EmployeeId { get; set; }
    public required int Page { get; set; }
    public required int Length { get; set; }
}

public class GetCheckupListByEmployeeIdQueryHandler 
    : IRequestHandler<GetCheckupListByEmployeeIdQuery, PaginatedList<CheckupViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetCheckupListByEmployeeIdQueryHandler(IMapper mapper, KondongpuDatabaseContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<CheckupViewModel>> Handle(GetCheckupListByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var query =  _context
            .Checkups
            .Include(c => c.CheckupType)
            .Include(c => c.Patient)
            .Include(c => c.Audiograms)
            .Include(c => c.Company)               
            .Include(c => c.Lungs)
            .Include(c => c.Labs)
            .Include(c => c.Xrays)
                .ThenInclude(xray => xray.CheckupItem).ThenInclude(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Include(c => c.Visions)
            .Include(c => c.PhysicalExams)
            .Include(c => c.SpecialTests)
            .Include(c => c.ConclusionBy!)
            .Include(c => c.PhysicalExaminationBy!)
            .AsQueryable();

        if (request.StartDate != null && request.EndDate != null)
        {
            query = query.Where(c => 
                c.VisitDate >= request.StartDate &&
                c.VisitDate <= request.EndDate);
        }              

        if (!string.IsNullOrEmpty(request.EmployeeId))
        {
            query = query.Where(c => c.Patient !=null && (c.Patient.EmployeeId == request.EmployeeId));
        }

        query = query
            .OrderByDescending(d => d.LastModified)
            .ThenBy(d => d.CreatedOn);

        var count = query.Count();

        var list = await query.ToListAsync(cancellationToken);
        var listViewModel = _mapper.Map<List<CheckupViewModel>>(list);
        //return paginatedList;

        return new PaginatedList<CheckupViewModel>(listViewModel, count, request.Page, request.Length);

        //return await query.ProjectTo<CheckupViewModel>(_mapper.ConfigurationProvider)
        //.PaginatedListAsync(request.Page, request.Length, cancellationToken: cancellationToken);       
    }
}

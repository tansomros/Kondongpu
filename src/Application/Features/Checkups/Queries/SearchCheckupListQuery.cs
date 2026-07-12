using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Checkups.ViewModels;
using Kondongpu.Application.Services;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Checkups.Queries;
public class SearchCheckupListQuery : IRequest<PaginatedList<CheckupViewModel>>
{  
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int? Status { get; set; }
    public int? Company { get; set; }
    public int? Type { get; set; }
    public int? Doctor { get; set; }
    public string? EmployeeId { get; set; }
    public string? SearchTerm { get; set; }
    public required int Page { get; set; }
    public required int Length { get; set; }
}

public class SearchCheckupListQueryHandler 
    : IRequestHandler<SearchCheckupListQuery, PaginatedList<CheckupViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;
    private readonly ICheckupStatusService _checkupStatusService;

    public SearchCheckupListQueryHandler(IMapper mapper, KondongpuDatabaseContext context, ICheckupStatusService checkupStatusService)
    {
        _mapper = mapper;
        _context = context;
        _checkupStatusService = checkupStatusService;
    }

    public async Task<PaginatedList<CheckupViewModel>> Handle(SearchCheckupListQuery request, CancellationToken cancellationToken)
    {
        var query =  _context
            .Checkups
            .Include(c => c.CheckupType)
            .Include(c => c.Patient)
            .Include(c => c.Audiograms)
            .Include(c => c.Company)
                //.ThenInclude(xray => xray.CheckupItem).ThenInclude(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Include(c => c.Lungs)
                //.ThenInclude(xray => xray.CheckupItem).ThenInclude(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Include(c => c.Labs)
                //.ThenInclude(xray => xray.CheckupItem).ThenInclude(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Include(c => c.Xrays)
                .ThenInclude(xray => xray.CheckupItem).ThenInclude(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Include(c => c.Visions)
            .Include(c => c.Dentals)
                //.ThenInclude(xray => xray.CheckupItem).ThenInclude(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Include(c => c.PhysicalExams)
                //.ThenInclude(xray => xray.CheckupItem).ThenInclude(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Include(c => c.SpecialTests)
                //.ThenInclude(xray => xray.CheckupItem).ThenInclude(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Include(c => c.ConclusionBy!)
            .Include(c => c.PhysicalExaminationBy!)
            .AsQueryable();

        if (request.StartDate != null && request.EndDate != null)
        {
            query = query.Where(c => 
                c.VisitDate >= request.StartDate &&
                c.VisitDate <= request.EndDate);
        }

        if (request.Status != null && request.Status != 0)
        {
            query = query.Where(c => c.Status == request.Status);
        }

        if (request.Company != null && request.Company != 0)
        {
            query = query.Where(c => c.CompanyId == request.Company);
        }

        if (request.Type != null && request.Type != 0)
        {
            query = query.Where(c => c.CheckupTypeId == request.Type);
        }
        if (request.Doctor != null && request.Doctor != 0)
        {
            query = query.Where(c => c.ConclusionById == request.Doctor);
        }
        if (!string.IsNullOrEmpty(request.EmployeeId))
        {
            query = query.Where(c => c.Patient != null && (c.Patient.EmployeeId == request.EmployeeId));
        }

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            //query = query.Filter(request.SearchTerm);
            query = query.Where(c => c.Patient !=null && (c.Patient.FirstName.Contains(request.SearchTerm) || c.Patient.LastName.Contains(request.SearchTerm) || c.HospitalNumber.Contains(request.SearchTerm) || (c.Patient.NationId !=null && c.Patient.NationId.Contains(request.SearchTerm))));
        }

        query = query
            .OrderByDescending(d => d.LastModified)
            .ThenBy(d => d.CreatedOn);

        var count = query.Count();

        var list = await query.ToListAsync(cancellationToken);
        var listViewModel = _mapper.Map<List<CheckupViewModel>>(list);
        //return paginatedList;


        await _checkupStatusService.UpdateStatusesAsync(listViewModel);


        return new PaginatedList<CheckupViewModel>(listViewModel, count, request.Page, request.Length);

        //return await query.ProjectTo<CheckupViewModel>(_mapper.ConfigurationProvider)
        //.PaginatedListAsync(request.Page, request.Length, cancellationToken: cancellationToken);       
    }
}

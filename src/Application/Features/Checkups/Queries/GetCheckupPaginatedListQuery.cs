using Kondongpu.Application.Common.Extensions;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Checkups.ViewModels;
using Kondongpu.Application.Features.Patients.ViewModels;

namespace Kondongpu.Application.Features.Checkups.Queries;
public class GetCheckupPaginatedListQuery : IRequest<PaginatedList<CheckupViewModel>>
{
    public string? SearchTerm { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public required int Page { get; set; }
    public required int Length { get; set; }
}

public class GetCheckupPaginatedListQueryHandler 
    : IRequestHandler<GetCheckupPaginatedListQuery, PaginatedList<CheckupViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetCheckupPaginatedListQueryHandler(IMapper mapper, KondongpuDatabaseContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<CheckupViewModel>> Handle(GetCheckupPaginatedListQuery request, CancellationToken cancellationToken)
    {
        var query =  _context
            .Checkups
            .Include(c => c.CheckupType)
            .Include(c => c.Patient)
            .Include(c => c.Audiograms)
                //.ThenInclude(xray => xray.CheckupItem).ThenInclude(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Include(c => c.Lungs)
                //.ThenInclude(xray => xray.CheckupItem).ThenInclude(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Include(c => c.Labs)
                //.ThenInclude(xray => xray.CheckupItem).ThenInclude(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Include(c => c.Xrays)
                .ThenInclude(xray => xray.CheckupItem).ThenInclude(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Include(c => c.Visions)
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

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Filter(request.SearchTerm);
        }

        query = query
            .OrderByDescending(d => d.LastModified)
            .ThenBy(d => d.CreatedOn);

        return await query.ProjectTo<CheckupViewModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.Page, request.Length, cancellationToken: cancellationToken);

        //var count = await query.CountAsync(cancellationToken);
        //var filtered = await query
        //    .AsNoTracking()
        //    .Skip((request.Page - 1) * request.Length)
        //    .Take(request.Length)
        //    .ToListAsync(cancellationToken);

        //var viewModels = _mapper.Map<List<CheckupViewModel>>(filtered);

        //foreach (CheckupViewModel item in viewModels)
        //{
        //    var patient = await _context.Patients.AsNoTracking().FirstOrDefaultAsync(s => s.HospitalNumber == item.HospitalNumber, cancellationToken);

        //    var patientViewModel = _mapper.Map<PatientViewModel>(patient);
        //    item.Patient = patientViewModel;
        //}
        //return new PaginatedList<CheckupViewModel>(viewModels, count, request.Page, request.Length);
    }
}

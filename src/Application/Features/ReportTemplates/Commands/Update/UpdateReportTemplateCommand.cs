
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.ReportTemplates.ViewModels;
using Kondongpu.Domain.Constants;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.ReportTemplates.Commands.Update
{
    public class UpdateReportTemplateCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public required string ReportName { get; set; }
        public required int ReportGroupId { get; set; }
        public string? ReportGroupName { get; set; }
        public bool Active { get; set; }
        public required string Sql { get; set; }
        public int Display { get; set; }
        public bool IsConfidential { get; set; }
        public required string CreateBy { get; set; }         
    }

    internal class UpdateReportTemplateCommandHandler : IRequestHandler<UpdateReportTemplateCommand, Unit>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly KondongpuDatabaseContext _context;
        private readonly IMapper _mapper;

        public UpdateReportTemplateCommandHandler(KondongpuDatabaseContext context, IMapper mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(UpdateReportTemplateCommand request, CancellationToken cancellationToken)
        {
            var report = await _context.ReportTemplate.FirstOrDefaultAsync(b => b.Id.Equals(request.Id), cancellationToken);

            if (report == null)
            {
                throw new NotFoundException(nameof(ReportTemplate), request.Id);
            }
            report.Name = request.ReportName;
            report.ReportGroupId = request.ReportGroupId;
            report.SqlText = request.Sql;
            report.IsActive = request.Active;
            report.IsConfidential = request.IsConfidential;
            report.ModifiedUser = request.CreateBy;             

            _context.ReportTemplate.Update(report);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }
}

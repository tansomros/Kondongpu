using AutoMapper;
using MediatR;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.ReportTemplates.ViewModels;
using Kondongpu.Domain.Entities;


namespace Kondongpu.Application.Features.ReportTemplates.Commands.Create
{
    public class CreateReportTemplateCommand : IRequest<int>
    {
        //public int Id { get; set; }
        public required string ReportName { get; set; }
        public required int ReportGroupId { get; set; }
        public string? ReportGroupName { get; set; }
        public bool IsActive { get; set; }
        public string? SqlText { get; set; }
        public int Sort { get; set; }
        public bool IsConfidential { get; set; }
        public string? CreateBy { get; set; } 


    }
    internal class CreateReportTemplateCommandHandler : IRequestHandler<CreateReportTemplateCommand, int>
    {
        private readonly KondongpuDatabaseContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public class CreateReportTemplateCommandValidation
        {
        }


        public CreateReportTemplateCommandHandler(KondongpuDatabaseContext context,ICurrentUserService currentUser, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<int> Handle(CreateReportTemplateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var report = new ReportTemplate(request.ReportName,request.ReportGroupId)
                {                    
                    SqlText = request.SqlText,
                    IsActive = request.IsActive,
                    IsConfidential = request.IsConfidential,  
                    CreateUser = request.CreateBy,
                    CreatedOn = DateTimeOffset.Now         
                };
                await _context.ReportTemplate.AddAsync(report, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return report.Id;

            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                    throw new NotImplementedException(ex.InnerException.Message);
                else
                    throw new NotImplementedException(ex.Message);
            }
        }
    }
}

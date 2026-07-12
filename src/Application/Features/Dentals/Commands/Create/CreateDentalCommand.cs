using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Dentals.Commands.Create
{
    public class CreateDentalCommand : IRequest<int>
    {
        public int CheckupId { get; set; }
        public required string VisitNumber { get; set; }
        public required int CheckupItemId { get; set; }
        public required string ResultValue { get; set; }
        public string? ResultNote { get; set; }    
        public bool Gingivitis { get; set; }
        public bool Decay { get; set; } 
        public bool Scaling { get; set; }
        public bool Fluoride { get; set; }
        public bool Sealant { get; set; }
        public string? SealantNote { get; set; }
        public bool Filling { get; set; }
        public string? FillingNote { get; set; }
        public bool Extraction { get; set; }
        public string? ExtractionNote { get; set; }
    }

    public class CreateDentalCommandHandler : IRequestHandler<CreateDentalCommand, int>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;

        public CreateDentalCommandHandler(KondongpuDatabaseContext checkupDatabaseContext)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
        }

        public async Task<int> Handle(CreateDentalCommand request, CancellationToken cancellationToken)
        {
            var dental = new Dental(request.CheckupId, request.VisitNumber, request.CheckupItemId, request.ResultValue)
            {
                ResultNote = request.ResultNote,
                Scaling = request.Scaling,
                Gingivitis = request.Gingivitis,
                Decay = request.Decay,
                Fluoride = request.Fluoride,
                Sealant = request.Sealant,
                SealantNote = request.SealantNote,
                Filling = request.Filling,
                FillingNote = request.FillingNote,
                Extraction = request.Extraction,
                ExtractionNote = request.ExtractionNote,
            };

            await _checkupDatabaseContext.Dentals.AddAsync(dental, cancellationToken);
            await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
            return dental.Id;
        }
    }
}

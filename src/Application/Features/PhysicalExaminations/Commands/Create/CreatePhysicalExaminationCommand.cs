using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.PhysicalExaminations.Commands.Create;

public record CreatePhysicalExaminationCommand : IRequest<int>
{
    public required int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
    public required int CheckupItemId { get; set; }
    public string? Ga { get; set; }
    public string? Heent { get; set; }
    public string? Mouth { get; set; }
    public string? Lymph { get; set; }
    public string? Thyroid { get; set; }
    public string? Chest { get; set; }
    public string? Heart { get; set; }
    public string? Abdomen { get; set; }
    public string? Ext { get; set; }
    public string? Skin { get; set; }
    public string? Other { get; set; }
    public string? GaText { get; set; }
    public string? HeentText { get; set; }
    public string? MouthText { get; set; }
    public string? LymphText { get; set; }
    public string? ThyroidText { get; set; }
    public string? ChestText { get; set; }
    public string? HeartText { get; set; }
    public string? AbdomenText { get; set; }
    public string? ExtText { get; set; }
    public string? SkinText { get; set; }
    public string? OtherText { get; set; }
}

public class CreatePhysicalExaminationCommandHandler : IRequestHandler<CreatePhysicalExaminationCommand, int>
{
    private readonly KondongpuDatabaseContext _checkupDatabaseContext;

    public CreatePhysicalExaminationCommandHandler(KondongpuDatabaseContext checkupDatabaseContext)
    {
        _checkupDatabaseContext = checkupDatabaseContext;
    }

    public async Task<int> Handle(CreatePhysicalExaminationCommand request, CancellationToken cancellationToken)
    {
        var physicalExamination = new PhysicalExamination(
            request.VisitNumber,
            request.CheckupId, 
            request.CheckupItemId, 
            request.Ga, 
            request.Heent, 
            request.Mouth, 
            request.Lymph, 
            request.Thyroid, 
            request.Chest, 
            request.Heart, 
            request.Abdomen, 
            request.Ext, 
            request.Skin, 
            request.Other,
            request.GaText, 
            request.HeentText, 
            request.MouthText, 
            request.ThyroidText, 
            request.ChestText, 
            request.HeartText, 
            request.AbdomenText, 
            request.ExtText, 
            request.SkinText, 
            request.OtherText);

        await _checkupDatabaseContext.PhysicalExams.AddAsync(physicalExamination, cancellationToken);
        await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
        return physicalExamination.Id;

    }
}

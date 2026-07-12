using Microsoft.EntityFrameworkCore;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Checkups.Constants;

namespace Kondongpu.Application.Features.PhysicalExaminations.Commands.Update;

public record UpdatePhysicalExaminationCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public required int CheckupId { get; set; }
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

    public bool IsActive { get; set; }
}

public class UpdatePhysicalExaminationCommandValidator : AbstractValidator<UpdatePhysicalExaminationCommand>
{
    private readonly KondongpuDatabaseContext _checkupDatabaseContext;
    public UpdatePhysicalExaminationCommandValidator(KondongpuDatabaseContext checkupDatabaseContext)
    {
        _checkupDatabaseContext = checkupDatabaseContext;

        RuleFor(p => p.CheckupId)
           .NotEmpty().WithMessage("Checkup Id ต้องไม่เป็นค่าว่าง")
           .NotNull().WithMessage("Checkup Id ต้องไม่เป็นค่า NULL")
           .MustAsync(BeExistAsync).WithMessage("ไม่พบข้อมูล")
           .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");

        RuleFor(p => p.CheckupItemId)
                .NotEmpty().WithMessage("Checkup ItemId ไม่สามารถเป็นค่าว่างได้")
                .NotNull().WithMessage("โปรดระบุ Checkup ItemId");

        //RuleFor(p => p.IsAbnormal)
        //      .NotEmpty().WithMessage("Result Abnormal ไม่สามารถเป็นค่าว่างได้")
        //      .NotNull().WithMessage("โปรดระบุ Result Abnormal");
        RuleFor(p => p.IsActive)
                .NotEmpty().WithMessage("สถานะ ไม่สามารถเป็นค่าว่างได้")
                .NotNull().WithMessage("โปรดระบุ สถานะ");
        //RuleFor(p => p.Recommend)
        //.NotEmpty().WithMessage("คำแนะนำ ไม่สามารถเป็นค่าว่างได้")
        //.NotNull().WithMessage("โปรดระบุ คำแนะนำ");
    }
    public async Task<bool> BeExistAsync(int id, CancellationToken cancellationToken)
    {
        var checkup = await _checkupDatabaseContext
            .Checkups
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return checkup != null;
    }
    public async Task<bool> AvailableToUpdateAsync(int id, CancellationToken cancellationToken)
    {
        var checkup = await _checkupDatabaseContext
            .Checkups
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return checkup != null && checkup.IsFinalized != true;
    }
}

public class UpdatePhysicalExaminationCommandHandler : IRequestHandler<UpdatePhysicalExaminationCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdatePhysicalExaminationCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdatePhysicalExaminationCommand request, CancellationToken cancellationToken)
    {
        var physicalExamination = await _context.PhysicalExams.FirstOrDefaultAsync(b => b.Id.Equals(request.Id), cancellationToken);
        if (physicalExamination == null)
        {
            throw new NotFoundException(nameof(physicalExamination), request.Id);
        }
        
        //physicalExamination.IsAbnormal = request.IsAbnormal;
        //physicalExamination.Recommend = request.Recommend;
        //physicalExamination.ResultValue = request.ResultValue;
        physicalExamination.IsActive = request.IsActive;
        physicalExamination.Ga  = request.Ga;
        physicalExamination.GaText = request.GaText;
        physicalExamination.Heent = request.Heent;
        physicalExamination.HeentText = request.HeentText;
        physicalExamination.Abdomen = request.Abdomen;
        physicalExamination.AbdomenText = request.AbdomenText;
        physicalExamination.Mouth = request.Mouth;
        physicalExamination.MouthText = request.MouthText;
        physicalExamination.Lymph = request.Lymph;
        physicalExamination.LymphText = request.LymphText;
        physicalExamination.Skin = request.Skin;
        physicalExamination.SkinText = request.SkinText;
        physicalExamination.Chest = request.Chest;
        physicalExamination.ChestText = request.ChestText;
        physicalExamination.Ext = request.Ext;
        physicalExamination.ExtText = request.ExtText;
        physicalExamination.Heart = request.Heart;
        physicalExamination.HeartText = request.HeartText;
        physicalExamination.Other   = request.Other;
        physicalExamination.OtherText = request.OtherText;
        physicalExamination.Thyroid =   request.Thyroid;
        physicalExamination.ThyroidText = request.ThyroidText; 

        _context.PhysicalExams.Update(physicalExamination);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

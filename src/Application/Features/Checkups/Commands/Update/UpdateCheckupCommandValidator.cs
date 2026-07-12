using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.Constants;

namespace Kondongpu.Application.Features.Checkups.Commands.Update;
public class UpdateCheckupCommandValidator : AbstractValidator<UpdateCheckupCommand>
{
    private readonly KondongpuDatabaseContext _context;
    public UpdateCheckupCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Checkup Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Checkup Id ต้องไม่เป็นค่า NULL")
            .MustAsync(BeExistAsync).WithMessage("ไม่พบข้อมูล")
            .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");

        RuleFor(p => p.Status).NotEmpty().WithMessage("สถานะต้องไม่เป็นค่าว่าง");
        RuleFor(p => p.PhysicalExaminationById)
            .NotNull().WithMessage("ผู้สรุปผลการตรวจร่างกายต้องไม่เป็นค่าว่าง")
            .MustAsync(BeExistDoctorAsync).WithMessage("ไม่พบข้อมูลแพทย์ผู้สรุปผลการตรวจร่างกาย");

        RuleFor(p => p.Status).NotEmpty().WithMessage("Status ต้องไม่เป็นค่าว่าง");
        RuleFor(p => p.PhysicalExaminationById).NotEmpty().WithMessage("PhysicalExaminationById ต้องไม่เป็นค่าว่าง");
        RuleFor(p => p.ConclusionById).NotEmpty().WithMessage("ConclusionById ต้องไม่เป็นค่าว่าง");
        //RuleFor(p => p.IsLabResultReady).NotEmpty().WithMessage("IsLabResultReady ต้องไม่เป็นค่าว่าง");
        //RuleFor(p => p.IsXrayResultReady).NotEmpty().WithMessage("IsXrayResultReady ต้องไม่เป็นค่าว่าง");

        //RuleFor(p => p.Weight)
        //    .Must(BeInBodyWeightRange)
        //    .WithMessage("โปรดตรวจสอบการป้อนข้อมูลน้ำหนัก ให้อยู่ในช่วงที่ควรจะเป็น 1 ถึง 200 กิโลกรัม");

        //RuleFor(p => p.Height)
        //    .Must(BeInBodyHeightRange)
        //    .WithMessage("โปรดตรวจสอบการป้อนข้อมูลส่วนสูง ให้อยู่ในช่วงที่ควรจะเป็น 25 ถึง 250 เซ็นติเมตร");

        //RuleFor(p => p.Temperature)
        //    .Must(BeInBodyTemperatureRange)
        //    .WithMessage("โปรดตรวจสอบการป้อนข้อมูลอุณหภูมิร่างกาย ให้อยู่ในช่วงที่ควรจะเป็น 20 ถึง 50 องศาเซลเซียส");

        //RuleFor(p => p.IsMain).NotEmpty().WithMessage("IsMain ต้องไม่เป็นค่าว่าง");
    }

    public async Task<bool> BeExistAsync(int id, CancellationToken cancellationToken)
    {
        var checkup = await _context
            .Checkups
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return checkup != null;
    }

    public async Task<bool> AvailableToUpdateAsync(int id, CancellationToken cancellationToken)
    {
        var checkup = await _context
            .Checkups
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return checkup != null && checkup.IsFinalized != true;
    }

    public async Task<bool> BeExistDoctorAsync(int id, CancellationToken cancellationToken)
    {
        var careProvider = await _context
            .CareProviders
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return careProvider != null;
    }

    // ตรวจสอบข้อมูล น้ำหนักคนเราควรอยู่ในช่วงที่ควรจะเป็น หน่วย กิโลกรัม
    public static bool BeInBodyWeightRange(double weight)
    {
        return weight >= 1 && weight <= 200;
    }

    // ตรวจสอบข้อมูล ส่วนสูงคนเราควรอยู่ในช่วงที่ควรจะเป็น หน่วย เซ็นติเมตร
    public static bool BeInBodyHeightRange(double height)
    {
        return height >= 25 && height <= 300;
    }

    // ตรวจสอบข้อมูล อุณหภูมิร่างกายคนเราควรอยู่ในช่วงที่ควรจะเป็น หน่วย องศาเซลเซียส
    public static bool BeInBodyTemperatureRange(double temperature)
    {
        return temperature >= 20 && temperature <= 50;
    }
}

namespace Kondongpu.Application.Features.Checkups.Commands.Create
{
    public class CreateCheckupCommandValidator : AbstractValidator<CreateCheckupCommand>
    {
        public CreateCheckupCommandValidator()
        {

            RuleFor(p => p.PatientId).NotNull().WithMessage("PatientId ต้องไม่เป็นค่าว่าง");
            RuleFor(p => p.CheckupVisitId).NotNull().WithMessage("CheckupVisitId ต้องไม่เป็นค่าว่าง");
            RuleFor(p => p.VisitNumber).NotEmpty().WithMessage("VisitNumber ต้องไม่เป็นค่าว่าง");
            RuleFor(p => p.HospitalNumber).NotEmpty().WithMessage("HospitalNumber ต้องไม่เป็นค่าว่าง");
            RuleFor(p => p.VisitDate).NotEmpty().WithMessage("VisitDate ต้องไม่เป็นค่าว่าง");
            RuleFor(p => p.VisitTime).NotEmpty().WithMessage("VisitTime ต้องไม่เป็นค่าว่าง");
            //RuleFor(p => p.CheckupTypeId).NotEmpty().WithMessage("CheckupTypeId ต้องไม่เป็นค่าว่าง");
            //RuleFor(p => p.Status).NotEmpty().WithMessage("Status ต้องไม่เป็นค่าว่าง");
            //RuleFor(p => p.PhysicalExaminationById).NotEmpty().WithMessage("PhysicalExaminationById ต้องไม่เป็นค่าว่าง");
            //RuleFor(p => p.ConclusionById).NotEmpty().WithMessage("ConclusionById ต้องไม่เป็นค่าว่าง");
            RuleFor(p => p.PayorName).NotEmpty().WithMessage("PayorName ต้องไม่เป็นค่าว่าง");
            RuleFor(p => p.PackageId).NotEmpty().WithMessage("PackageId ต้องไม่เป็นค่าว่าง");
            RuleFor(p => p.PackageName).NotEmpty().WithMessage("PackageName ต้องไม่เป็นค่าว่าง");
            //RuleFor(p => p.LabStatus).NotEmpty().WithMessage("LabStatus ต้องไม่เป็นค่าว่าง");
            //RuleFor(p => p.RadiologyStatus).NotEmpty().WithMessage("RadiologyStatus ต้องไม่เป็นค่าว่าง");
            RuleFor(p => p.Weight).NotEmpty().WithMessage("Weight ต้องไม่เป็นค่าว่าง");
            RuleFor(p => p.Height).NotEmpty().WithMessage("Height ต้องไม่เป็นค่าว่าง");
            RuleFor(p => p.Temperature).NotEmpty().WithMessage("Temperature ต้องไม่เป็นค่าว่าง");
            //RuleFor(p => p.IsMain).NotEmpty().WithMessage("IsMain ต้องไม่เป็นค่าว่าง");
        }
    }
}

using FluentValidation;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Patients.Commands.Update;
public class UpdateAdressPatientCommandValidator : AbstractValidator<UpdateAdressPatientCommand>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateAdressPatientCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;
        RuleFor(p => p.ProvinceId).MustAsync(DoesProvinceExistAsync).WithMessage("ไม่พบ Province ที่ระบุ");
        //RuleFor(p => p.DistrictId).MustAsync(DoesDistrictExistAsync).WithMessage("ไม่พบ District ที่ระบุ");
        //RuleFor(p => p.SubDistrictId).MustAsync(DoesSubDistrictExistAsync).WithMessage("ไม่พบ SubDistrict ที่ระบุ");
        RuleFor(p => p.ZipCode).MustAsync(DoesZipCodeExistAsync).WithMessage("ไม่พบ ZipCode ที่ระบุ");
    }

    public async Task<bool> DoesProvinceExistAsync(string? provinceId, CancellationToken cancellationToken)
    {
        return await _context.Provinces.AsNoTracking().AnyAsync(p => p.ProvinceId == provinceId, cancellationToken);
    }

    public async Task<bool> DoesDistrictExistAsync(string? districtId, CancellationToken cancellationToken)
    {
        return await _context.Districts.AnyAsync(d => d.DistrictId == districtId, cancellationToken);
    }

    public async Task<bool> DoesSubDistrictExistAsync(string? subdistrictId, CancellationToken cancellationToken)
    {
        return await _context.SubDistricts.AnyAsync(s => s.SubDistrictId == subdistrictId, cancellationToken);
    }

    public async Task<bool> DoesZipCodeExistAsync(string? zipCode, CancellationToken cancellationToken)
    {
        return await _context.SubDistricts.AnyAsync(s => s.ZipCode == zipCode, cancellationToken);
    }
}
